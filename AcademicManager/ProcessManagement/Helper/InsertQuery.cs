using System;
using System.Linq;

namespace ProcessManagement.Helper
{
    public class InsertQuery
    {
        public static string Create(string tableName, object objectData)
        {
            //Note_Teacher: if we remove comments the tests will pass

            //if (tableName == null || objectData == null)
            //{
            //    throw new Exception("Please make sure that table name or values used for creating the query aren't null!");
            //}

            var query = $"insert into {tableName} ";
            var last = objectData.GetType().GetProperties().Last();
            var columns = "(";
            var values = "(";

            foreach (var property in objectData.GetType().GetProperties())
            {
                var concate = property.Equals(last) ? ")" : ", ";

                columns += $"{property.Name}{concate}";
                values += $"'{property.GetValue(objectData)}'{concate}";
            }

            query += $"{columns} values {values};";

            return query;
        }
    }
}
