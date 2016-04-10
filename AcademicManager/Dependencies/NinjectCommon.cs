using Ninject.Modules;
using ProcessManagement.Implementations;
using ProcessManagement.Interfaces;

namespace Dependencies
{
    public class NinjectCommon : NinjectModule
    {
        public override void Load()
        {
            Bind<IStudentRepository>().To<StudentRepository>();
            Bind<IStudentStatusRepository>().To<StudentStatusRepository>();
            Bind<IStudentClassRepository>().To<StudentClassRepository>();
            Bind<IDatabaseModel>().To<DatabaseModel>();
        }
    }
}
