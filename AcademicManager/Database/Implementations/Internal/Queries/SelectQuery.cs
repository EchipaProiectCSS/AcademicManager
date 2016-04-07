namespace Database.Implementations.Internal.Queries
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Domain;
    using Interfaces.Internal;
    using Utility;

    public class SelectQuery : BaseQuery
    {
        public SelectQuery(string query) : base(query)
        {
            if (!query.Contains(Instructions.From))
            {
                throw new ArgumentException("Must specify the table from which to select.");
            }
        }

        public override IQueryResult Execute()
        {
            var result = new SelectResult();

            if (Database == null)
            {
                throw new ArgumentNullException(string.Empty, "Must set a database where the select is to be executed.");
            }

            var tableName = ExtractTableName();

            if (!TableExists(tableName))
            {
                throw new TableAlreadyExistsException(
                    string.Format("The database {0} does not contain a table with the name {1}", Database.Name,
                        tableName));
            }

            var columnNamesFromSelect = ExtractColumnNamesFromQuery();
            var conditionsFromQuery = ExtractConditionsFromQuery();

            var tableHeader = LoadTableHeader(tableName);

            foreach (var columnName in columnNamesFromSelect)
            {
                if (!tableHeader.Contains(columnName))
                {
                    throw new InvalidOperationException(string.Format("Table {0} does not have the {1} column.",
                        tableName, columnName));
                }
            }

            var columnToValue = new Dictionary<string, string>();

            for (var i = 0; i < columnNamesFromSelect.Count(); i++)
            {
                //columnToValue[columnNamesFromSelect[i]] = conditionsFromQuery[i];
            }

            var values = new List<string>();

            foreach (var header in tableHeader)
            {
                if (columnToValue.ContainsKey(header))
                {
                    values.Add(columnToValue[header]);
                }
                else
                {
                    values.Add(string.Empty);
                }
            }

            WriteValuesToTable(tableName, values);

            return result;
        }

        private Table ExtractConditionsFromQuery()
        {
            var queryCopy = string.Copy(Query);

            if (!queryCopy.Contains(Instructions.Where))
            {
                return null;
            }

            var startIndex = queryCopy.IndexOf(Instructions.Where, StringComparison.OrdinalIgnoreCase) +
                             Instructions.Where.Length;

            var length = queryCopy.IndexOf(Instructions.StatementTerminator) - startIndex;

            queryCopy = queryCopy.Substring(startIndex, length);

            var equalityConditionsRegex = new Regex(@"(\w+)( *)=( *)(\w+)", RegexOptions.IgnoreCase);

            var res = equalityConditionsRegex.Match(queryCopy);
            var equalityConditions = new List<string>();
            while (res.Success)
            {
                equalityConditions.Add(res.Value.Replace(" ", string.Empty));
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
                .Split(new[] {Operators.Equal}, StringSplitOptions.RemoveEmptyEntries);

            var row = new Row();
            row.Values.Add(cond[0], cond[1].Replace("'", string.Empty));

            result.Rows.Add(row);

            for (var i = 1; i < equalityConditions.Count - 1; i++)
            {
                cond = equalityConditions[i]
                    .Split(new[] {Operators.Equal}, StringSplitOptions.RemoveEmptyEntries);

                if (quantifiers[i].Equals(Operators.Or))
                {
                    row = new Row();
                }

                row.Values.Add(cond[0], cond[1].Replace("'", string.Empty));

                result.Rows.Add(row);
            }

            cond = equalityConditions.Last()
                .Split(new[] {Operators.Equal}, StringSplitOptions.RemoveEmptyEntries);

            row = new Row();
            row.Values.Add(cond[0], cond[1].Replace("'", string.Empty));

            result.Rows.Add(row);

            return result;
        }

        private void WriteValuesToTable(string tableName, List<string> values)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            if (!File.Exists(tableFilePath))
            {
                throw new FileNotFoundException(string.Format("Could not find file for table {0}, in database {1}",
                    tableName, Database.Name));
            }

            for (var i = 0; i < values.Count; i++)
            {
                values[i] = values[i].Replace("'", string.Empty);
            }

            File.AppendAllLines(tableFilePath, new List<string> {string.Join(", ", values)});
        }

        private List<string> LoadTableHeader(string tableName)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");
            var headerLine = File.ReadLines(tableFilePath).First();

            return headerLine.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToList();
        }

        private bool TableExists(string tableName)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            return File.Exists(tableFilePath);
        }

        private List<string> ExtractColumnNamesFromQuery()
        {
            var queryCopy = string.Copy(Query);

            var startIndex = queryCopy.IndexOf(Instructions.Select, StringComparison.OrdinalIgnoreCase) +
                             Instructions.Select.Length;

            var length = queryCopy.IndexOf(Instructions.From, StringComparison.OrdinalIgnoreCase) - startIndex;

            queryCopy = queryCopy.Substring(startIndex, length).Trim();

            var rawColumns = queryCopy.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            return rawColumns.Select(c => c.Trim()).ToList();
        }

        private string ExtractTableName()
        {
            var queryCopy = string.Copy(Query);

            var startIndex = queryCopy.IndexOf(Instructions.From, StringComparison.OrdinalIgnoreCase) +
                             Instructions.From.Length;

            int length;

            if (queryCopy.Contains(Instructions.Where))
            {
                length = queryCopy.IndexOf(Instructions.Where, StringComparison.OrdinalIgnoreCase) - startIndex;
            }
            else
            {
                length = queryCopy.IndexOf(Instructions.StatementTerminator);
            }

            var tableName = queryCopy.Substring(startIndex, length).Trim(' ');

            return tableName;
        }
    }
}