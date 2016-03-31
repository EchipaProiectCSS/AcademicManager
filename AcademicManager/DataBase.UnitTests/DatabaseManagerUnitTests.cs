namespace DataBase.UnitTests
{
    using System.IO;
    using Database.Implementations;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseManagerUnitTests
    {
        [SetUp]
        public void Initialize()
        {
            if (Directory.Exists(DatabaseFilePath))
            {
                Directory.Delete(DatabaseFilePath, true);
            }

            databaseManager = new FileSystemDatabaseManager();
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(DatabaseFilePath, true);
        }

        private const string DatabaseFilePath = @"C:\Databases";
        private const string DatabaseName = "SampleDatabase";
        private readonly string databaseConnectionString = Path.Combine(DatabaseFilePath, DatabaseName);
        private IDatabaseManager<FileSystemDatabase> databaseManager;

        [Test]
        public void ShouldSuccesfullyOpenACreatedDatabase()
        {
            var expectedDatabase = databaseManager.Create(DatabaseFilePath, DatabaseName);

            Assert.IsNotNull(expectedDatabase);

            var actualDatabase = databaseManager.Open(databaseConnectionString);

            Assert.AreEqual(expectedDatabase.ConnectionString, actualDatabase.ConnectionString);
            Assert.AreEqual(expectedDatabase.Name, actualDatabase.Name);
        }
    }
}