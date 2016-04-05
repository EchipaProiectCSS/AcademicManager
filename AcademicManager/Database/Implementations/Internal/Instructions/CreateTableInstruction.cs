namespace Database.Implementations.Internal.Instructions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Utility;

    public class CreateTableInstruction : BaseInstruction
    {
        public CreateTableInstruction(string instruction) : base(instruction)
        {
            if (!instruction.Contains('(') || !instruction.Contains(')'))
            {
                throw new ArgumentException("The list of columns must be enclosed within '(' and ')'");
            }
        }

        public override void Run()
        {
            if (Database == null)
            {
                throw new ArgumentNullException(string.Empty, "Must set a database where the table is to be created.");
            }

            var tableName = ExtractTableName();
            var columnNames = ExtractColumnNames();

            if (!TableExists(tableName))
            {
                CreateTable(tableName, columnNames);
            }
            else
            {
                throw new TableAlreadyExistsException(
                    string.Format("The database {0} already has a table with the name {1}", Database.Name, tableName));
            }
        }

        private void CreateTable(string tableName, IEnumerable<string> columnNames)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            using (var file = File.CreateText(tableFilePath))
            {
                file.Write(string.Join(", ", columnNames));
            }
        }

        private bool TableExists(string tableName)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, tableName + ".txt");

            return File.Exists(tableFilePath);
        }

        private IEnumerable<string> ExtractColumnNames()
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

            instructionCopy = instructionCopy.Replace(Instructions.CreateTable, string.Empty);
            var tableName = instructionCopy.Substring(0, instructionCopy.IndexOf('(')).Trim(' ');

            return tableName;
        }
    }
}