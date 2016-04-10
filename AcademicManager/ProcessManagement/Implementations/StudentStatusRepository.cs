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

        public StudentStatusRepository(IDatabaseModel model)
        {
            database = model.GetInstance();
        }

        public List<StudentStatusDo> GetAll()
        {
            var query = database.Query(Queries.GetAllStatuses);

            return GetStatusesFromQuery(query);
        }

        public List<StudentStatusDo> GetStudentStatuses(int studentId)
        {
            var query = database.Query(string.Format(Queries.GetStatusesByStudentId, studentId));

            return GetStatusesFromQuery(query);
        }


        public StudentStatusDo Get(int statusId)
        {

            var query = database.Query(string.Format(Queries.GetStatusById, statusId));

            if (query.Result.Rows.Count == 0)
            {
                return null;
            }

            var studentStatus = new StudentStatusDo();

            new AutoMapper(studentStatus, query.Result.Rows.First().Values).Start();

            return studentStatus;
        }

        private List<StudentStatusDo> GetStatusesFromQuery(IQueryResult query)
        {
            if (query.Result.Rows.Count == 0)
            {
                return null;
            }

            var statuses = GetStatusesFromQueryResult(query);

            return statuses;
        }

        public void Update(StudentStatusDo status)
        {
            var query = UpdateQuery.Create("studentStatuses", status);

            database.Execute(query);
        }

        public void Insert(StudentStatusDo status)
        {
            var query = InsertQuery.Create("studentStatuses", status);

            database.Execute(query);
        }


        private List<StudentStatusDo> GetStatusesFromQueryResult(IQueryResult query)
        {
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
