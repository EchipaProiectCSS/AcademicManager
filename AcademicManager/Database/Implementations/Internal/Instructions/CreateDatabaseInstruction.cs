namespace Database.Implementations.Internal.Instructions
{
    using System;
    using System.IO;
    using Utility;

    public class CreateDatabaseInstruction : BaseInstruction
    {
        public CreateDatabaseInstruction(string instruction) : base(instruction)
        {
        }

        public override void Run()
        {
            var databaseName = ExtractDatabaseName();

            if (DatabaseExists(databaseName))
            {
                throw new InvalidOperationException(
                    string.Format("The database {0} already exists.", Database.Name));
            }

            CreateDatabase(databaseName);
        }

        private void CreateDatabase(string input)
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

            var databaseFilePath = Path.Combine(connectionString, dbName);

            Directory.CreateDirectory(databaseFilePath);
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

            var startIndex = queryCopy.IndexOf(Instructions.CreateDatabase, StringComparison.OrdinalIgnoreCase) +
                             Instructions.CreateDatabase.Length;

            var length = queryCopy.IndexOf(Instructions.StatementTerminator) - startIndex;

            var tableName = queryCopy.Substring(startIndex, length).Trim();

            return tableName.Trim().Trim('\\');
        }
    }
}