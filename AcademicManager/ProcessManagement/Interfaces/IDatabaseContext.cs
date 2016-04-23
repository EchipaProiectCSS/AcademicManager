using ProcessManagement.Implementations;

namespace ProcessManagement.Interfaces
{
    public interface IDatabaseContext
    {
        IStudentRepository Student { get; }

        IStudentStatusRepository StudentStatus { get; }

        IStudentClassRepository StudentClass { get; }
    }
}
