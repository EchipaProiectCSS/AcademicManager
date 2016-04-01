using Database.Implementations;
using Database.Interfaces;
using Ninject.Modules;
using ProcessManagement.Implementations;
using ProcessManagement.Interfaces;

namespace Dependencies
{
    public class NinjectCommon : NinjectModule
    {
        public override void Load()
        {
            Bind<IStudent>().To<Student>();
            Bind<IDatabase>().To<FileSystemDatabase>();
            Bind<IDatabaseManager<IDatabase>>().To<FileSystemDatabaseManager>();
        }
    }
}
