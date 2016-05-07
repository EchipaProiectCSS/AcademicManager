namespace DataBase.UnitTests
{
    using System.IO;
    using Database.Implementations;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class SelectFromTableUnitTests
    {
        [SetUp]
        public void BeforeEachTest()
        {
            var script = @"drop table admins;";
            database.Execute(script);
            script = @"create table admins (id, username, password);";
            database.Execute(script);
        }

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
        public void ShouldSelectAllFromTable()
        {
            var script =
                @"insert into admins (id, username, password) values ('0', 'John Doe', '1234'); insert into admins (id, username, password) values ('1', 'James Doe', '4321'); insert into admins (id, username) values ('2', 'Jane Doe');";
            database.Execute(script);

            script = @"select * from admins;";
            var result = database.Query(script);
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Result.Rows.Count);
        }

        [Test]
        public void ShouldSelectSingleRowFromTable()
        {
            var script =
                @"insert into admins (id, username, password) values ('0', 'John Doe', '1234'); insert into admins (id, username, password) values ('1', 'John Doe', '4321'); insert into admins (id, username) values ('2', 'Jane Doe');";
            database.Execute(script);

            script = @"select id, username, password from admins where id = '0' and username = 'John Doe';";
            var result = database.Query(script);
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Result.Rows.Count);
        }

        [Test]
        public void ShouldSelectTwoRowsFromTable()
        {
            //BUG: several bug fixes were need for the selection algorithm
            var script =
                @"insert into admins (id, username, password) values ('0', 'John Doe', '1234'); insert into admins (id, username, password) values ('1', 'John Doe', '4321'); insert into admins (id, username) values ('2', 'Jane Doe');";
            database.Execute(script);

            script = @"select id, username, password from admins where id = '0' or username = 'John Doe';";
            var result = database.Query(script);
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Result.Rows.Count);
        }

        [Test]
        public void ShouldSelectOnlyTwoRowsFromTable()
        {
            //BUG: the first boolean operator was skipped; bug fixed
            var script =
                @"insert into admins (id, username, password) values ('0', 'John Doe', '1234'); insert into admins (id, username, password) values ('1', 'John Doe', '4321'); insert into admins (id, username, password) values ('2', 'John Doe', '4321');";
            database.Execute(script);

            script = @"select id, username, password from admins where id = '0' and username = 'John Doe' or id = '1' and username = 'John Doe';";
            var result = database.Query(script);
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Result.Rows.Count);
        }
    }
}