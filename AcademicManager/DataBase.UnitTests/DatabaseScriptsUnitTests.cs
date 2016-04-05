namespace DataBase.UnitTests
{
    using System.IO;
    using Database.Implementations;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseScriptsUnitTests
    {
        [SetUp]
        public void Initialize()
        {
            if (Directory.Exists(DatabaseFilePath))
            {
                Directory.Delete(DatabaseFilePath, true);
            }

            databaseManager = new FileSystemDatabaseManager();
            database = databaseManager.Create(DatabaseFilePath, DatabaseName);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(DatabaseFilePath, true);
        }

        private const string DatabaseFilePath = @"C:\Databases";
        private const string DatabaseName = "SampleDatabase";
        private IDatabaseManager<FileSystemDatabase> databaseManager;
        private IDatabase database;

        [Test]
        public void ShouldCreateTable()
        {
            var script = @"create table admins (id PK, username, password);";
            database.Execute(script);

            var expectedTableFileLocation = Path.Combine(database.ConnectionString, database.Name, "admins.txt");

            Assert.IsTrue(File.Exists(expectedTableFileLocation));

            var expectedTableFileContents = "id PK, username, password";
            var actualTableFileContents = File.ReadAllText(expectedTableFileLocation);

            Assert.AreEqual(expectedTableFileContents, actualTableFileContents);
        }
    }
}