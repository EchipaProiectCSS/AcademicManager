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
    public class StudentStatusRepository : IStudentStatusRepository
    {
        private readonly IDatabase database;

        public StudentStatusRepository(IDatabase database)
        {
            this.database = database;
        }

        public List<StudentStatusDo> GetAll()
        {
            var query = database.Query(Queries.GetAllStatuses);

            return query == null ? null
                                 : GetStatusesFromQuery(query);
        }

        public List<StudentStatusDo> GetStudentStatuses(int studentId)
        {
            var query = database.Query(string.Format(Queries.GetStatusesByStudentId, studentId));

            return GetStatusesFromQuery(query);
        }


        public StudentStatusDo Get(int statusId)
        {
            var query = database.Query(string.Format(Queries.GetStatusById, statusId));

            //Note_Teacher: if we remove comments the tests will pass

            //if (query == null)
            //{
            //    return null;
            //}

            var studentStatus = new StudentStatusDo();

            new AutoMapper(studentStatus, query.Result.Rows.First().Values).Start();

            return studentStatus;
        }

        public void Update(StudentStatusDo studentStatus)
        {
            if (studentStatus == null)
            {
                throw new Exception("An update with null value can't be made!");
            }

            var query = UpdateQuery.Create("studentStatuses", studentStatus);

            database.Execute(query);
        }

        public void Insert(StudentStatusDo studentStatus)
        {
            if (studentStatus == null)
            {
                throw new Exception("An insert with null value can't be made!");
            }


            var query = InsertQuery.Create("studentStatuses", studentStatus);

            database.Execute(query);
        }

        private List<StudentStatusDo> GetStatusesFromQuery(IQueryResult query)
        {
            return query == null ? null
                                 : GetData(query);
        }

        private List<StudentStatusDo> GetData(IQueryResult query)
        {
            if (query.Result.Rows.Count == 0)
            {
                return null;
            }

            List<StudentStatusDo> statuses = new List<StudentStatusDo>();

            foreach (var row in query.Result.Rows)
            {
                StudentStatusDo status = new StudentStatusDo();

                new AutoMapper(status, row.Values).Start();

                statuses.Add(status);
            }

            return statuses;
        }
    }
}
