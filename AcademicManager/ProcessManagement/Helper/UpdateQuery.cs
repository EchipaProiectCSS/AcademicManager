using System.Linq;

namespace ProcessManagement.Helper
{
    public static class UpdateQuery
    {
        public static string Create(string tableName, object objectData)
        {
            var query = $"update {tableName} set ";

            var last = objectData.GetType().GetProperties().Last();

            var Id = "0";

            foreach (var property in objectData.GetType().GetProperties())
            {
                if (property.Name.ToLower() == "id")
                {
                    Id = property.GetValue(objectData).ToString();
                    continue;
                }

                query += $"{property.Name} = '{property.GetValue(objectData)}'";

                query += property.Equals(last) ? " " : ", ";
            }

            query += $"where Id = '{Id}';";

            return query;
        }
    }
}
