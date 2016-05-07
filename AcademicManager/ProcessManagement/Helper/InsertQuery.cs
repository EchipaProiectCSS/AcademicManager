using System;
using System.Linq;

namespace ProcessManagement.Helper
{
    public class InsertQuery
    {
        public static string Create(string tableName, object objectData)
        {
            //BUG: the comment line should be added in order to fix the bug
            //It should not be allowed to create a query with null values or null name for table

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
