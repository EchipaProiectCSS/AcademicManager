using System.Diagnostics;
using Database.Interfaces;
using ProcessManagement.Interfaces;

namespace ProcessManagement.Implementations
{
    public class Student : Repository, IStudent
    {
        private readonly IDatabaseManager<IDatabase> databaseManager;

        public Student(IDatabaseManager<IDatabase> databaseManager) : base(databaseManager)
        {
            this.databaseManager = databaseManager;
        }

        public void GetAcademicStatus()
        {
            Debug.WriteLine(database.Name);

            //TODO
        }
    }
}
