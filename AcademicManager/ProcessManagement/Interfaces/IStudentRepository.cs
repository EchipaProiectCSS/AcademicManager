using System.Collections.Generic;
using ProcessManagement.DOs;

namespace ProcessManagement.Interfaces
{
    public interface IStudentRepository
    {
        List<StudentDO> GetAll();
    }
}
