namespace DataBase.UnitTests
{
    using System.IO;
    using Database.Implementations;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseScriptsUnitTests
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
            
            script = @"insert into admins (id, username, password) values ('0', 'John Doe', '1234');";
            database.Execute(script);

            script = @"insert into admins (id, username) values ('1', 'John Doe');";
            database.Execute(script);

            script = @"insert into admins (id, username) values ('1', 'Jane Doe');";
            database.Execute(script);

            script = @"select * from admins;";
            var result = database.Query(script);
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Result.Rows.Count);

            script = @"delete from admins where id = '0';";
            database.Execute(script);

            script = @"update admins set password = 'new password' where username = 'John Doe';";
            database.Execute(script);


            script = @"drop table admins;";
            database.Execute(script);

            script = @"drop database SampleDatabase;";
            database.Execute(script);

            script = @"create database SampleDatabase2;";
            database.Execute(script);

            script = @"use SampleDatabase2;";
            database.Execute(script);

            script = @"create table admins (id, username, password);";
            database.Execute(script);
        }
    }
}