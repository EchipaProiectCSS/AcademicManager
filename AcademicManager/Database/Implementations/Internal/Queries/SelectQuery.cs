namespace Database.Implementations.Internal.Queries
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Diagnostics;

    using global::Database.Implementations.Internal.Domain;
    using global::Database.Implementations.Internal.Utility;
    using global::Database.Interfaces.Internal;

    public class SelectQuery : BaseQuery
    {
        public SelectQuery(string query)
            : base(query)
        {

            //ADDED ASSERTION
            Debug.Assert(query == null, "Query must not be NULL");
            if (!query.Contains(Instructions.From))
            {
                throw new ArgumentException("Must specify the table from where to select.");
            }
        }

        public override IQueryResult Execute()
        {
            var result = new SelectResult();

            //todo: assertion - pre
            Debug.Assert(Database == null, "A Database should be selected in order to execute any operation.");

            if (Database == null)
            {
                throw new ArgumentNullException(string.Empty, "Must set a database where the select is to be executed.");
            }

            var tableName = ExtractTableName();

            if (!TableExists(tableName))
            {
                throw new TableAlreadyExistsException(
                    string.Format(
                        "The database {0} does not contain a table with the name {1}",
                        Database.Name,
                        tableName));
            }

            var columnNamesFromSelect = ExtractColumnNamesFromQuery();

            var columns = string.Join("\t", columnNamesFromSelect.Cast<string>().ToArray());

            //todo: assertion - post
            Debug.Assert(columnNamesFromSelect.Count > 0, $"The columns used in select operation are {columns}");

            var conditionsFromQuery = ExtractConditionsFromQuery();

            var table = LoadTable(tableName);

            if (conditionsFromQuery == null)
            {
                result.Result = table;
                return result;
            }

            foreach (var columnName in columnNamesFromSelect)
            {
                if (columnName.Equals("*"))
                {
                    break;
                }

                if (!table.Header.Any(c => c.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new InvalidOperationException(
                        string.Format("Table {0} does not have the {1} column.", tableName, columnName));
                }
            }

            var resultTable = new Table { Header = table.Header };

            foreach (var row in table.Rows)
            {
                foreach (var conditionRow in conditionsFromQuery.Rows)
                {
                    var isMatch = true;
                    foreach (var pair in conditionRow.Values)
                    {
                        if (!string.IsNullOrEmpty(pair.Value) && row.Values[pair.Key] != pair.Value)
                        {
                            isMatch &= false;
                        }
                        else
                        {
                            isMatch &= true;
                        }

                        if (isMatch)
                        { break; }
                    }

                    if (isMatch)
                    {
                        resultTable.Rows.Add(row);
                        break;
                    }
                }
            }

            result.Result = resultTable;
            return result;
        }

        private Table LoadTable(string tableName)
        {
            var result = new Table();

            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            //todo: assertion - post
            Debug.Assert(string.IsNullOrEmpty(tableFilePath), string.Format("The table {0} is not found in database", tableName));

            var tableLines = File.ReadAllLines(tableFilePath).ToList();
            var headerLine = tableLines.First();

            tableLines = tableLines.Skip(1).ToList();

            var columns = headerLine.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToList();

            result.Header =
                columns.Select(
                    c =>
                    new Column
                    {
                        IsPrimaryKey = c.ToLower().Contains(Instructions.PrimaryKey),
                        Name = c.Replace(Instructions.PrimaryKey, string.Empty).Trim()
                    }).ToList();

            foreach (var line in tableLines)
            {
                var values = line.Split(',').Select(s => s.Trim()).ToList();
                var row = new Row();
                for (var i = 0; i < columns.Count; i++)
                {
                    if (values.Count < i)
                    {
                        row.Values[columns[i]] = string.Empty;
                    }
                    else
                    {
                        row.Values[columns[i]] = values[i];
                    }
                }

                result.Rows.Add(row);
            }

            return result;
        }

        private Table ExtractConditionsFromQuery()
        {
            var queryCopy = string.Copy(Query);

            if (!queryCopy.Contains(Instructions.Where))
            {
                return null;
            }

            var startIndex = queryCopy.IndexOf(Instructions.Where, StringComparison.OrdinalIgnoreCase)
                             + Instructions.Where.Length;

            var length = queryCopy.IndexOf(Instructions.StatementTerminator) - startIndex;

            queryCopy = queryCopy.Substring(startIndex, length);

            var equalityConditionsRegex = new Regex(@"(\w+)( *)=( *)'([\w ]+)'", RegexOptions.IgnoreCase);

            var res = equalityConditionsRegex.Match(queryCopy);
            var equalityConditions = new List<string>();
            while (res.Success)
            {
                equalityConditions.Add(res.Value.Trim());
                res = res.NextMatch();
            }

            var quantifiersRegex = new Regex(@"and|or", RegexOptions.IgnoreCase);

            var quantifiers = new List<string>();
            res = quantifiersRegex.Match(queryCopy);
            while (res.Success)
            {
                quantifiers.Add(res.Value.Trim());
                res = res.NextMatch();
            }

            var result = new Table();
            var cond = equalityConditions.First()
                .Split(new[] { Operators.Equal }, StringSplitOptions.RemoveEmptyEntries);

            var row = new Row();
            row.Values.Add(cond[0].Trim(), cond[1].Trim().Replace("'", string.Empty));

            result.Rows.Add(row);

            for (var i = 1; i < equalityConditions.Count - 1; i++)
            {
                cond = equalityConditions[i].Split(new[] { Operators.Equal }, StringSplitOptions.RemoveEmptyEntries);

                if (quantifiers[i - 1].Equals(Operators.Or))
                {
                    row = new Row();
                }

                row.Values.Add(cond[0].Trim(), cond[1].Trim().Replace("'", string.Empty));

                result.Rows.Add(row);
            }

            if (equalityConditions.Count >= 2)
            {
                cond = equalityConditions.Last().Split(new[] { Operators.Equal }, StringSplitOptions.RemoveEmptyEntries);

                if (quantifiers.Last().Equals(Operators.Or))
                {
                    row = new Row();
                }

                row.Values.Add(cond[0].Trim(), cond[1].Trim().Replace("'", string.Empty));

                result.Rows.Add(row);
            }

            return result;
        }

        private bool TableExists(string tableName)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            return File.Exists(tableFilePath);
        }

        private List<string> ExtractColumnNamesFromQuery()
        {
            var queryCopy = string.Copy(Query);

            var startIndex = queryCopy.IndexOf(Instructions.Select, StringComparison.OrdinalIgnoreCase)
                             + Instructions.Select.Length;

            var length = queryCopy.IndexOf(Instructions.From, StringComparison.OrdinalIgnoreCase) - startIndex;

            queryCopy = queryCopy.Substring(startIndex, length).Trim();

            var rawColumns = queryCopy.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            return rawColumns.Select(c => c.Trim()).ToList();
        }

        private string ExtractTableName()
        {
            var queryCopy = string.Copy(Query);

            var startIndex = queryCopy.IndexOf(Instructions.From, StringComparison.OrdinalIgnoreCase)
                             + Instructions.From.Length;

            int length;

            if (queryCopy.Contains(Instructions.Where))
            {
                length = queryCopy.IndexOf(Instructions.Where, StringComparison.OrdinalIgnoreCase) - startIndex;
            }
            else
            {
                length = queryCopy.IndexOf(Instructions.StatementTerminator) - startIndex;
            }

            var tableName = queryCopy.Substring(startIndex, length).Trim(' ');

            return tableName;
        }
    }
}