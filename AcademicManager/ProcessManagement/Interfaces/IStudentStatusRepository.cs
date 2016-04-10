using System.Collections.Generic;
using ProcessManagement.DOs;

namespace ProcessManagement.Interfaces
{
    public interface IStudentStatusRepository
    {
        List<StudentStatusDo> GetAll();

        StudentStatusDo Get(int statusId);

        List<StudentStatusDo> GetStudentStatuses(int studentId);

        void Update(StudentStatusDo studentStatus);

        void Insert(StudentStatusDo studentStatus);
    }
}
