namespace DataBase.UnitTests
{
    using System.IO;
    using Database.Implementations;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class DropTableUnitTests
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
            database.Execute(@"create table admins (id, username, password);");
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
        public void ShouldDropTable()
        {
            database.Execute(@"drop table admins;");

            var expectedTableFileLocation = Path.Combine(database.ConnectionString, database.Name, "admins.txt");

            Assert.IsFalse(File.Exists(expectedTableFileLocation));
        }
    }
}