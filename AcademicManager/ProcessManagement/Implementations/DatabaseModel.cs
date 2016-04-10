using System.Configuration;
using Database.Implementations;
using Database.Interfaces;
using ProcessManagement.Interfaces;

namespace ProcessManagement.Implementations
{
    public class DatabaseModel : IDatabaseModel
    {
        private readonly FileSystemDatabaseManager databaseManager;
        private FileSystemDatabase Context;

        public DatabaseModel()
        {
            databaseManager = new FileSystemDatabaseManager();

            SetInstance();
        }

        public void SetInstance()
        {
            if (Context != null)
            {
                return;
            }

            Context = this.databaseManager.Open(ConfigurationSettings.AppSettings["connectionString"]);
        }

        public IDatabase GetInstance()
        {
            return Context;
        }
    }
}
