using System.Linq;

namespace ProcessManagement.Helper
{
    public class InsertQuery
    {
        public static string Create(string tableName, object objectData)
        {
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
