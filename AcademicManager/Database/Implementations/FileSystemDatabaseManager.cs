namespace Database.Implementations
{
    using System.Diagnostics;
    using System.IO;
    using Interfaces;
    using Internal;
    using Internal.Parsers;

    public class FileSystemDatabaseManager : IDatabaseManager<FileSystemDatabase>
    {
        public FileSystemDatabase Open(string filePath)
        {
            var directoryName = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryName ?? string.Empty))
            {
                throw new FileNotFoundException("The database does not exist at the provided location.");
            }

            var db = new FileSystemDatabase(
                new FileLoader(),
                new InstructionParser(),
                new QueryParser(),
                new DatabaseEngine())
            {
                ConnectionString = ExtractConnectionString(filePath),
                Name = ExtractDatabaseName(filePath)
            };
            //todo: assertion
            Debug.Assert(!string.IsNullOrWhiteSpace(db.ConnectionString),
                "An instance of database must always have a connection string.");
            return db;
        }

        public FileSystemDatabase Create(string connectionString, string name)
        {
            var db = new FileSystemDatabase(
                new FileLoader(),
                new InstructionParser(),
                new QueryParser(),
                new DatabaseEngine())
            {
                ConnectionString = connectionString,
                Name = name
            };

            var dbFilePath = Path.Combine(db.ConnectionString, db.Name);

            if (!Directory.Exists(dbFilePath))
            {
                Directory.CreateDirectory(dbFilePath);
            }
            else
            {
                throw new IOException(
                    string.Format("The {0} database already exists at {1}.", db.Name, db.ConnectionString));
            }

            //todo: assertion
            Debug.Assert(!string.IsNullOrWhiteSpace(db.ConnectionString),
                "An instance of database must always have a connection string.");

            return db;
        }

        private static string ExtractDatabaseName(string connectionString)
        {
            return connectionString.TrimEnd('\\').Substring(connectionString.LastIndexOf('\\') + 1);
        }

        private static string ExtractConnectionString(string connectionString)
        {
            return connectionString.TrimEnd('\\').Substring(0, connectionString.LastIndexOf('\\'));
        }
    }
}