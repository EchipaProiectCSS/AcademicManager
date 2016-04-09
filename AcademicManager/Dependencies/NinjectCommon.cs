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
            Bind<IDatabaseModel>().To<DatabaseModel>();
        }
    }
}
