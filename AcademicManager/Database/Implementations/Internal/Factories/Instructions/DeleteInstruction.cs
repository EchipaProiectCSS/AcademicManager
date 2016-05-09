namespace Database.Implementations.Internal.Instructions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Domain;
    using Utility;

    public class DeleteInstruction : BaseInstruction
    {
        public DeleteInstruction(string instruction) : base(instruction)
        {
            if (!instruction.Contains(Instructions.From))
            {
                throw new ArgumentException("Must specify the table from where to delete.");
            }
        }

        public override void Run()
        {
            if (Database == null)
            {
                throw new ArgumentNullException(string.Empty, "Must set a database where the delete is to be executed.");
            }

            var tableName = ExtractTableName();

            if (!TableExists(tableName))
            {
                throw new TableAlreadyExistsException(
                    string.Format("The database {0} does not contain a table with the name {1}", Database.Name,
                        tableName));
            }

            var conditionsFromQuery = ExtractConditionsFromQuery();

            var table = LoadTable(tableName);

            if (conditionsFromQuery == null)
            {
                return;
            }

            var matchesTable = new Table {Header = table.Header};

            foreach (var row in table.Rows)
            {
                var isMatch = true;
                foreach (var conditionRow in conditionsFromQuery.Rows)
                {
                    foreach (var pair in conditionRow.Values)
                    {
                        if (!string.IsNullOrEmpty(pair.Value) && row.Values[pair.Key] != pair.Value)
                        {
                            isMatch = false;
                            break;
                        }
                    }

                    if (isMatch)
                    {
                        matchesTable.Rows.Add(row);
                        break;
                    }
                }
            }

            foreach (var rowToRemove in matchesTable.Rows)
            {
                table.Rows.Remove(rowToRemove);
            }

            PersistTable(table);
        }

        private void PersistTable(Table table)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, table.Name + ".txt");

            if (File.Exists(tableFilePath))
            {
                File.Delete(tableFilePath);
                CreateTable(table.Name, table.Header.Select(c => c.Name.Trim()));
            }

            var lines =
                table.Rows.Select(
                    row => string.Join(", ", row.Values.Select(v => v.Value.Replace("'", string.Empty).Trim())))
                    .ToList();

            File.AppendAllLines(tableFilePath, lines);
        }

        private void CreateTable(string tableName, IEnumerable<string> columnNames)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            using (var file = File.CreateText(tableFilePath))
            {
                file.WriteLine(string.Join(", ", columnNames));
            }
        }

        private Table LoadTable(string tableName)
        {
            var result = new Table
            {
                Name = tableName
            };

            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");
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
            var queryCopy = string.Copy(Instruction);

            if (!queryCopy.Contains(Instructions.Where))
            {
                return null;
            }

            var startIndex = queryCopy.IndexOf(Instructions.Where, StringComparison.OrdinalIgnoreCase) +
                             Instructions.Where.Length;

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
                .Split(new[] {Operators.Equal}, StringSplitOptions.RemoveEmptyEntries);

            var row = new Row();
            row.Values.Add(cond[0].Trim(), cond[1].Trim().Replace("'", string.Empty));

            result.Rows.Add(row);

            for (var i = 1; i < equalityConditions.Count - 1; i++)
            {
                cond = equalityConditions[i]
                    .Split(new[] {Operators.Equal}, StringSplitOptions.RemoveEmptyEntries);

                if (quantifiers[i].Equals(Operators.Or))
                {
                    row = new Row();
                }

                row.Values.Add(cond[0].Trim(), cond[1].Trim().Replace("'", string.Empty));

                result.Rows.Add(row);
            }
            if (equalityConditions.Count >= 2)
            {
                cond = equalityConditions.Last()
                    .Split(new[] {Operators.Equal}, StringSplitOptions.RemoveEmptyEntries);

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

        private string ExtractTableName()
        {
            var queryCopy = string.Copy(Instruction);

            var startIndex = queryCopy.IndexOf(Instructions.From, StringComparison.OrdinalIgnoreCase) +
                             Instructions.From.Length;

            int length;

            if (queryCopy.Contains(Instructions.Where))
            {
                length = queryCopy.IndexOf(Instructions.Where, StringComparison.OrdinalIgnoreCase) - startIndex;
            }
            else
            {
                length = queryCopy.IndexOf(Instructions.StatementTerminator) - startIndex;
            }

            var tableName = queryCopy.Substring(startIndex, length).Trim();

            return tableName;
        }
    }
}