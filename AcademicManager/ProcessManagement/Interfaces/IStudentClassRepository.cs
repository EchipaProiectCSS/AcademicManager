using System.Collections.Generic;
using ProcessManagement.DOs;

namespace ProcessManagement.Interfaces
{
    public interface IStudentClassRepository
    {
        List<StudentClassDo> GetAll();

        StudentClassDo Get(int classId);

        List<StudentClassDo> GetStudentClasses(int studentId);

        void Update(StudentClassDo studentClass);

        void Insert(StudentClassDo studentClass);
    }
}
