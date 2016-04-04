namespace Database.Implementations.Internal.Domain
{
    using System.Collections.Generic;

    public class Table
    {
        public List<Column> Header { get; set; }
        public ICollection<Row> Rows { get; set; }
    }
}