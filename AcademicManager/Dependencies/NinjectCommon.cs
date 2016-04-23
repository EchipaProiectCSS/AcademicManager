using Database.Implementations;
using Database.Implementations.Internal;
using Database.Implementations.Internal.Parsers;
using Database.Interfaces;
using Database.Interfaces.Internal;
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
            Bind<IDatabase>().To<FileSystemDatabase>();
            Bind<ILoader>().To<FileLoader>();
            Bind<IDatabaseEngine>().To<DatabaseEngine>();
            Bind<IInstructionParser>().To<InstructionParser>();
            Bind<IQueryParser>().To<QueryParser>();
            Bind<IDatabaseContext>().To<DatabaseContext>();
        }
    }
}
