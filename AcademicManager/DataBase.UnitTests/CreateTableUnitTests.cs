namespace DataBase.UnitTests
{
    using System;
    using System.IO;
    using Database.Implementations;
    using Database.Implementations.Internal.Utility;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class CreateTableUnitTests
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
        public void ShouldCreateTable()
        {
            var script = @"create table admins (id, username, password);";
            database.Execute(script);

            var expectedTableFileLocation = Path.Combine(database.ConnectionString, database.Name, "admins.txt");

            Assert.IsTrue(File.Exists(expectedTableFileLocation));

            var expectedTableFileContents = "id, username, password\r\n";
            var actualTableFileContents = File.ReadAllText(expectedTableFileLocation);

            Assert.AreEqual(expectedTableFileContents, actualTableFileContents);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowIfListOfColumnsNotEnclosedWithParantheses()
        {
            var script = @"create table admins id, username, password;";
            database.Execute(script);
        }

        [Test]
        [ExpectedException(typeof(TableAlreadyExistsException))]
        public void ShouldThrowIfTableAlreadyExists()
        {
            var script = @"create table admins (id, username, password);create table admins (id, username, password);";
            database.Execute(script);
        }

        [Test]
        [ExpectedException(typeof(DuplicateColumnNameException))]
        public void ShouldThrowIfTableDeclaresDuplicateColumns()
        {
            var script = @"create table admins (id, id);";
            database.Execute(script);
        }
    }
}