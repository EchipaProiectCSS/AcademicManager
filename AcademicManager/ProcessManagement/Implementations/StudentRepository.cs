using System;
using System.Collections.Generic;
using System.Linq;
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

        public StudentRepository(IDatabase database)
        {
            this.database = database;
        }

        public List<StudentDo> GetAll()
        {
            var query = database.Query(Queries.GetAllStudents);

            var students = GetStudentsFromQueryResult(query);

            return students;
        }

        public StudentDo Get(int studentId)
        {
            var query = database.Query(string.Format(Queries.GetStudentById, studentId));

            if (query.Result.Rows.Count == 0)
            {
                return null;
            }

            var studentDo = new StudentDo();

            new AutoMapper(studentDo, query.Result.Rows.First().Values).Start();

            return studentDo;
        }

        public void Update(StudentDo student)
        {
            if (student == null)
            {
                throw new Exception("An update with null value can't be made!");
            }

            var query = UpdateQuery.Create("students", student);

            database.Execute(query);
        }
        public void Insert(StudentDo student)
        {
            if (student == null)
            {
                throw new Exception("An insert with null value can't be made!");
            }

            var query = InsertQuery.Create("students", student);

            database.Execute(query);
        }

        private List<StudentDo> GetStudentsFromQueryResult(IQueryResult query)
        {
            List<StudentDo> students = new List<StudentDo>();

            foreach (var row in query.Result.Rows)
            {
                StudentDo studentDo = new StudentDo();

                new AutoMapper(studentDo, row.Values).Start();

                students.Add(studentDo);
            }

            return students;
        }
    }
}
