using System.Collections.Generic;
using ProcessManagement.DOs;

namespace ProcessManagement.Interfaces
{
    public interface IStudentRepository
    {
        List<StudentDo> GetAll();

        StudentDo Get(int studentId);

        void Update(StudentDo student);

        void Insert(StudentDo student);
    }
}
