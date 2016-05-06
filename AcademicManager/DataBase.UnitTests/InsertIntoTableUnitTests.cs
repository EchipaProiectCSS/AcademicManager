namespace DataBase.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Database.Implementations;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class InsertIntoTableUnitTests
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
            var script = @"create table admins (id, username, password);";
            database.Execute(script);
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
        public void ShouldInsertIntoTable()
        {
            //BUG: the write values function added an extra space between the values; bug fixed

            var script =
                @"insert into admins (id, username, password) values ('0', 'John Doe', '1234'); insert into admins (id, username, password) values ('1', 'John Doe', '1234');";
            database.Execute(script);

            var expectedTableFileLocation = Path.Combine(database.ConnectionString, database.Name, "admins.txt");

            var expectedTableFileContents = new List<string>
            {
                "id, username, password",
                "0, John Doe, 1234",
                "1, John Doe, 1234"
            };
            var actualTableFileContents = File.ReadAllLines(expectedTableFileLocation);

            for (var i = 0; i < actualTableFileContents.Length; i++)
            {
                Assert.AreEqual(expectedTableFileContents[i], actualTableFileContents[i]);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowIfColumnDoesNotExist()
        {
            var script = @"insert into admins (iddd, username, password) values ('John Doe', '1234');";
            database.Execute(script);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowIfTableDoesNotExist()
        {
            var script = @"insert into nonExistingTable (id, username, password) values ('0', 'John Doe', '1234');";
            database.Execute(script);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowIfValuesCountDifferFromColumnsCount()
        {
            var script = @"insert into admins (id, username, password) values ('John Doe', '1234');";
            database.Execute(script);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowIfValuesKeywordMissing()
        {
            var script = @"insert into admins (id, username, password) ('0', 'John Doe', '1234');";
            database.Execute(script);
        }
    }
}