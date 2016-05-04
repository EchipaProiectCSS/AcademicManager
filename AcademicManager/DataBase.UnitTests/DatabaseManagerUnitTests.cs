namespace DataBase.UnitTests
{
    using System;
    using System.IO;

    using Database.Implementations;
    using Database.Interfaces;

    using NUnit.Framework;

    [TestFixture]
    public class DatabaseManagerUnitTests
    {
        private const string DatabaseFilePath = @"C:\Databases";

        private const string DatabaseName = "SampleDatabase";

        private readonly string databaseConnectionString = Path.Combine(DatabaseFilePath, DatabaseName);

        private IDatabaseManager<FileSystemDatabase> databaseManager;

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
            if (Directory.Exists(DatabaseFilePath))
            {
                Directory.Delete(DatabaseFilePath, true);
            }
        }

        [Test]
        public void ShouldNotThrowWhenCallingCreateWithEmptyParams()
        {
            var actualDatabase = databaseManager.Create(string.Empty, string.Empty);
            Assert.AreEqual(@"C:\Databases", actualDatabase.ConnectionString);
            Assert.AreEqual(string.Empty, actualDatabase.Name);
        }

        [Test]
        public void ShouldOpenedDatabaseNameAndConnectionStringShoudNotEndWithBackSlash()
        {
            databaseManager.Create(DatabaseFilePath, DatabaseName);
            var actualDatabase = databaseManager.Open(databaseConnectionString);

            Assert.IsNotNull(actualDatabase);
            Assert.IsFalse(actualDatabase.ConnectionString.EndsWith("\\", StringComparison.Ordinal));
            Assert.IsFalse(actualDatabase.Name.EndsWith("\\", StringComparison.Ordinal));
        }

        [Test]
        public void ShouldSuccesfullyCreateANewDatabase()
        {
            var expectedDatabase = databaseManager.Create(DatabaseFilePath, DatabaseName);

            Assert.IsNotNull(expectedDatabase);
            Assert.AreEqual(DatabaseFilePath, expectedDatabase.ConnectionString);
            Assert.AreEqual(DatabaseName, expectedDatabase.Name);
        }

        [Test]
        public void ShouldSuccesfullyOpenAnExistingDatabase()
        {
            databaseManager.Create(DatabaseFilePath, DatabaseName);
            var actualDatabase = databaseManager.Open(databaseConnectionString);

            Assert.IsNotNull(actualDatabase);
            Assert.AreEqual(DatabaseFilePath, actualDatabase.ConnectionString);
            Assert.AreEqual(DatabaseName, actualDatabase.Name);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldThrowWhenCallingOpenWithEmpty()
        {
            // BUG: does not throw expected error
            databaseManager.Open(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(IOException))]
        public void ShouldThrowWhenCreatingADatabaseWithTheSameConnectionString()
        {
            databaseManager.Create(DatabaseFilePath, DatabaseName);
            databaseManager.Create(DatabaseFilePath, DatabaseName);
        }

        [Test]
        [ExpectedException(typeof(IOException))]
        public void ShouldThrowWhenCreatingADatabaseWithMalformedFilePath()
        {
            databaseManager.Create("not a filepath", DatabaseName);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldThrowWhenOpeningANonExistingDatabase()
        {
            // BUG: this does not fail with the expected error because the implementation does not properly verify
            // BUG: that the actual database folder exists, it checks that its parent folder exists instead
            databaseManager.Create(DatabaseFilePath, DatabaseName);
            var nonExistingDatabase = Path.Combine(DatabaseFilePath, DatabaseName) + "NotExists";
            databaseManager.Open(nonExistingDatabase);
        }
    }
}