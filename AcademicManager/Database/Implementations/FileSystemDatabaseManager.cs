namespace Database.Implementations
{
    using System.IO;
    using Interfaces;
    using Internal;

    public class FileSystemDatabaseManager : IDatabaseManager<FileSystemDatabase>
    {
        public FileSystemDatabase Open(string filePath)
        {
            var directoryName = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryName ?? string.Empty))
            {
                throw new FileNotFoundException("The database does not exist at the provided location.");
            }

            var db = new FileSystemDatabase(new FileLoader(), new ScriptParser(), new DatabaseEngine())
            {
                ConnectionString = ExtractConnectionString(filePath),
                Name = ExtractDatabaseName(filePath)
            };

            return db;
        }

        public FileSystemDatabase Create(string connectionString, string name)
        {
            var dbFilePath = Path.Combine(connectionString, name);

            if (!Directory.Exists(dbFilePath))
            {
                Directory.CreateDirectory(dbFilePath);
            }
            else
            {
                throw new IOException("A database with the provided name already exists.");
            }

            var db = new FileSystemDatabase(new FileLoader(), new ScriptParser(), new DatabaseEngine())
            {
                ConnectionString = connectionString,
                Name = name
            };

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