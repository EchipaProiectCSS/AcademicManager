namespace DataBase.UnitTests
{
    using Database.Implementations;
    using Database.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseManagerUnitTests
    {
        [SetUp]
        public void Initialize()
        {
            databaseManager = new FileSystemDatabaseManager();
        }

        private const string ScriptFilePath = @"Sample\Script1.sql";
        private const string DatabaseFilePath = @"D:\Databases";
        private const string DatabaseName = "SampleDatabase";
        private IDatabaseManager<FileSystemDatabase> databaseManager;

        [Test]
        public void ShouldTestSomething()
        {
            var database = databaseManager.Create(DatabaseFilePath, DatabaseName);
            database.RunScriptFile(ScriptFilePath);
        }
    }
}