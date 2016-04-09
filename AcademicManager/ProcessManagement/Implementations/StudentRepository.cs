using System.Collections.Generic;
using Database.Interfaces;
using Database.Interfaces.Internal;
using ProcessManagement.DOs;
using ProcessManagement.Helper;
using ProcessManagement.Interfaces;

namespace ProcessManagement.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDatabase database;

        public StudentRepository(IDatabaseModel model)
        {
            database = model.GetInstance();
        }

        public List<StudentDO> GetAll()
        {
            var query = database.Query(Queries.GetAllStudents);

            var students = GetStudentsFromQueryResult(query);

            return students;
        }

        private List<StudentDO> GetStudentsFromQueryResult(IQueryResult query)
        {
            List<StudentDO> students = new List<StudentDO>();

            foreach (var row in query.Result.Rows)
            {
                StudentDO studentDo = new StudentDO();

                new AutoMapper(studentDo, row.Values).Start();

                students.Add(studentDo);
            }

            return students;
        }
    }
}
