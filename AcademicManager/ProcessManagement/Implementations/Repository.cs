using System.Configuration;
using Database.Interfaces;
using ProcessManagement.Interfaces;

namespace ProcessManagement.Implementations
{
    public class Repository:IRepository
    {
        private readonly IDatabaseManager<IDatabase> databaseManager;
        public readonly IDatabase database;

        public Repository(IDatabaseManager<IDatabase> databaseManager)
        {
            this.databaseManager = databaseManager;

            database = this.databaseManager.Open(ConfigurationSettings.AppSettings["connectionString"]);
        }
    }
}
