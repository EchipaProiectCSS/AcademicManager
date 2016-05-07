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
    public class StudentClassRepository : IStudentClassRepository
    {
        private readonly IDatabase database;

        public StudentClassRepository(IDatabase database)
        {
            this.database = database;
        }

        public List<StudentClassDo> GetAll()
        {
            var query = database.Query(Queries.GetAllClasses);

            return query == null ? null
                                 : GetClassesFromQuery(query);
        }

        public StudentClassDo Get(int classId)
        {
            var query = database.Query(string.Format(Queries.GetClassById, classId));

            if (query == null)
            {
                return null;
            }

            var studentClass = new StudentClassDo();

            new AutoMapper(studentClass, query.Result.Rows.First().Values).Start();

            return studentClass;
        }

        public List<StudentClassDo> GetStudentClasses(int studentId)
        {
            var query = database.Query(string.Format(Queries.GetClassesByStudentId, studentId));

            return GetClassesFromQuery(query);
        }

        public void Update(StudentClassDo studentClass)
        {
            if (studentClass == null)
            {
                throw new Exception("An update with null value can't be made!");
            }

            var query = UpdateQuery.Create("studentClasses", studentClass);

            database.Execute(query);
        }

        public void Insert(StudentClassDo studentClass)
        {
            if (studentClass == null)
            {
                throw new Exception("An insert with null value can't be made!");
            }

            var query = InsertQuery.Create("studentClasses", studentClass);

            database.Execute(query);
        }

        private List<StudentClassDo> GetClassesFromQuery(IQueryResult query)
        {
            return query == null ? null
                                 : GetData(query);
        }

        private List<StudentClassDo> GetData(IQueryResult query)
        {
            List<StudentClassDo> statuses = new List<StudentClassDo>();

            foreach (var row in query.Result.Rows)
            {
                StudentClassDo status = new StudentClassDo();

                new AutoMapper(status, row.Values).Start();

                statuses.Add(status);
            }

            return statuses;
        }
    }
}
