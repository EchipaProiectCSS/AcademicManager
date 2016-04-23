using System.Configuration;
using Database.Implementations;
using ProcessManagement.Interfaces;

namespace ProcessManagement.Implementations
{
    public class DatabaseContext: IDatabaseContext
    {
        public DatabaseContext()
        {
            if (Helper.Cache.FileDatabase != null)
            {
                return;
            }

            Helper.Cache.FileDatabase = new FileSystemDatabaseManager().Open(ConfigurationSettings.AppSettings["connectionString"]);
        }

        public IStudentRepository Student => new StudentRepository(Helper.Cache.FileDatabase);

        public IStudentStatusRepository StudentStatus => new StudentStatusRepository(Helper.Cache.FileDatabase);

        public IStudentClassRepository StudentClass => new StudentClassRepository(Helper.Cache.FileDatabase);
    }
}