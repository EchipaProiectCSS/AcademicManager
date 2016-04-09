namespace Database.Implementations.Internal.Instructions
{
    using System;
    using System.IO;
    using Utility;

    public class DropDatabaseInstruction : BaseInstruction
    {
        public DropDatabaseInstruction(string instruction) : base(instruction)
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

            DropDatabase(databaseName);
        }

        private void DropDatabase(string databaseName)
        {
            string databaseFilePath = databaseName;
            if (Directory.Exists(databaseName))
            {
                Directory.Delete(databaseFilePath, true);
                return;
            }

            databaseFilePath = Path.Combine(Database.ConnectionString, databaseName);
            Directory.Delete(databaseFilePath, true);
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

            var startIndex = queryCopy.IndexOf(Instructions.DropDatabase, StringComparison.OrdinalIgnoreCase) +
                             Instructions.DropDatabase.Length;

            var length = queryCopy.IndexOf(Instructions.StatementTerminator) - startIndex;

            var tableName = queryCopy.Substring(startIndex, length).Trim();

            return tableName;
        }
    }
}