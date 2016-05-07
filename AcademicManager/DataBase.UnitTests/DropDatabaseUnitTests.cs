namespace DataBase.UnitTests
{
    using System.IO;
    using Database.Implementations;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class DropDatabaseUnitTests
    {
        [TestFixtureSetUp]
        public void Initialize()
        {
            if (Directory.Exists(DatabaseFilePath))
            {
                Directory.Delete(DatabaseFilePath, true);
            }

            databaseManager = new FileSystemDatabaseManager();
            database = databaseManager.Create(DatabaseFilePath, DatabaseName);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Directory.Delete(DatabaseFilePath, true);
        }

        private const string DatabaseFilePath = @"C:\Databases";
        private const string DatabaseName = "SampleDatabase";
        private IDatabaseManager<FileSystemDatabase> databaseManager;
        private IDatabase database;

        [Test]
        public void ShouldDropDatabase()
        {
            database.Execute(@"drop database SampleDatabase;");

            var expectedTableFileLocation = Path.Combine(database.ConnectionString, database.Name);

            Assert.IsFalse(Directory.Exists(expectedTableFileLocation));
        }
    }
}