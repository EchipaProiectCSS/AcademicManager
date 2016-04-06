namespace Database.Implementations.Internal.Instructions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Utility;

    public class InsertInstruction : BaseInstruction
    {
        public InsertInstruction(string instruction) : base(instruction)
        {
            if (!instruction.Contains('(') || !instruction.Contains(')'))
            {
                throw new ArgumentException("The list of columns must be enclosed within '(' and ')'");
            }

            if (!instruction.Contains(Instructions.Values))
            {
                throw new ArgumentException("Must provide the list of values.");
            }
        }

        public override void Run()
        {
            if (Database == null)
            {
                throw new ArgumentNullException(string.Empty, "Must set a database where the table is to be created.");
            }

            var tableName = ExtractTableName();

            if (!TableExists(tableName))
            {
                throw new TableAlreadyExistsException(
                    string.Format("The database {0} does not contain a table with the name {1}", Database.Name,
                        tableName));
            }

            var columnNamesFromInsert = ExtractColumnNamesFromScript();
            var valuesFromInsert = ExtractValuesFromScript();

            if (columnNamesFromInsert.Count != valuesFromInsert.Count)
            {
                throw new InvalidOperationException(
                    "The number of values must be the same with the number of columns specified.");
            }

            var tableHeader = LoadTableHeader(tableName);

            foreach (var columnName in columnNamesFromInsert)
            {
                if (!tableHeader.Contains(columnName))
                {
                    throw new InvalidOperationException(string.Format("Table {0} does not have the {1} column.",
                        tableName, columnName));
                }
            }

            var columnToValue = new Dictionary<string, string>();

            for (var i = 0; i < columnNamesFromInsert.Count(); i++)
            {
                columnToValue[columnNamesFromInsert[i]] = valuesFromInsert[i];
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
        }

        private void WriteValuesToTable(string tableName, IEnumerable<string> values)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            if (!File.Exists(tableFilePath))
            {
                throw new FileNotFoundException(string.Format("Could not find file for table {0}, in database {1}",
                    tableName, Database.Name));
            }

            File.AppendAllLines(tableFilePath, new List<string> {string.Join(", ", values)});
        }

        private List<string> LoadTableHeader(string tableName)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");
            var headerLine = File.ReadLines(tableFilePath).First();

            return headerLine.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToList();
        }

        private List<string> ExtractValuesFromScript()
        {
            var instructionCopy = string.Copy(Instruction);

            var valuesSectionStartIndex =
                instructionCopy.IndexOf(Instructions.Values, StringComparison.OrdinalIgnoreCase) +
                Instructions.Values.Length;

            instructionCopy = instructionCopy.Substring(valuesSectionStartIndex);

            var startIndex = instructionCopy.IndexOf('(') + 1;
            var length = instructionCopy.IndexOf(')') - startIndex;

            instructionCopy = instructionCopy.Substring(startIndex, length);

            var rawValues = instructionCopy.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            return new List<string>(rawValues);
        }

        private bool TableExists(string tableName)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            return File.Exists(tableFilePath);
        }

        private List<string> ExtractColumnNamesFromScript()
        {
            var instructionCopy = string.Copy(Instruction);

            var startIndex = instructionCopy.IndexOf('(') + 1;
            var length = instructionCopy.IndexOf(')') - startIndex;

            instructionCopy = instructionCopy.Substring(startIndex, length);

            var rawColumns = instructionCopy.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            var columnNames = new List<string>();

            foreach (var column in rawColumns.Select(c => c.Trim(' ')))
            {
                if (!columnNames.Contains(column))
                {
                    columnNames.Add(column);
                }
                else
                {
                    throw new DuplicateColumnNameException("Columns must have unique names within the same table.");
                }
            }

            return columnNames;
        }

        private string ExtractTableName()
        {
            var instructionCopy = string.Copy(Instruction);

            instructionCopy = instructionCopy.Replace(Instructions.InsertInto, string.Empty);
            var tableName = instructionCopy.Substring(0, instructionCopy.IndexOf('(')).Trim(' ');

            return tableName;
        }
    }
}