namespace Database.Implementations.Internal.Instructions
{
    using System;
    using System.IO;
    using Utility;

    public class DropTableInstruction : BaseInstruction
    {
        public DropTableInstruction(string instruction) : base(instruction)
        {
        }

        public override void Run()
        {
            var tableName = ExtractTableName();

            if (!TableExists(tableName))
            {
                throw new TableAlreadyExistsException(
                    string.Format("The database {0} does not contain a table with the name {1}", Database.Name,
                        tableName));
            }

            DropTable(tableName);
        }

        private void DropTable(string tableName)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            File.Delete(tableFilePath);
        }

        private bool TableExists(string tableName)
        {
            var tableFilePath = Path.Combine(Database.ConnectionString, Database.Name, tableName + ".txt");

            return File.Exists(tableFilePath);
        }

        private string ExtractTableName()
        {
            var queryCopy = string.Copy(Instruction);

            var startIndex = queryCopy.IndexOf(Instructions.DropTable, StringComparison.OrdinalIgnoreCase) +
                             Instructions.DropTable.Length;

            var length = queryCopy.IndexOf(Instructions.StatementTerminator) - startIndex;

            var tableName = queryCopy.Substring(startIndex, length).Trim();

            return tableName;
        }
    }
}