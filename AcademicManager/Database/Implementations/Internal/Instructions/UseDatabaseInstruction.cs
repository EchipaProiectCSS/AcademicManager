namespace Database.Implementations.Internal.Instructions
{
    using System;
    using System.IO;
    using Utility;

    public class UseDatabaseInstruction : BaseInstruction
    {
        public UseDatabaseInstruction(string instruction) : base(instruction)
        {
        }

        public override void Run()
        {
            var databaseName = ExtractDatabaseName();

            if (!DatabaseExists(databaseName))
            {
                throw new InvalidOperationException(
                    string.Format("The database {0} does not exist", Database.Name));
            }

            UpdateCurrentDatabase(databaseName);
        }

        private void UpdateCurrentDatabase(string input)
        {
            string connectionString;
            string dbName;

            if (Directory.Exists(input))
            {
                input = input.Trim('\\');

                connectionString = input.Substring(0, input.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase));

                var startIndex = input.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase) + 1;
                var length = input.IndexOf(Instructions.StatementTerminator) - startIndex;

                dbName = input.Substring(startIndex, length);
            }
            else
            {
                connectionString = Database.ConnectionString;
                dbName = input;
            }

            Database.ConnectionString = connectionString;
            Database.Name = dbName;
        }

        private bool DatabaseExists(string databaseName)
        {
            if (Directory.Exists(databaseName))
            {
                return true;
            }

            var databaseFilePath = Path.Combine(Database.ConnectionString, databaseName);

            return Directory.Exists(databaseFilePath);
        }

        private string ExtractDatabaseName()
        {
            var queryCopy = string.Copy(Instruction);

            var startIndex = queryCopy.IndexOf(Instructions.Use, StringComparison.OrdinalIgnoreCase) +
                             Instructions.Use.Length;

            var length = queryCopy.IndexOf(Instructions.StatementTerminator) - startIndex;

            var tableName = queryCopy.Substring(startIndex, length).Trim();

            return tableName;
        }
    }
}