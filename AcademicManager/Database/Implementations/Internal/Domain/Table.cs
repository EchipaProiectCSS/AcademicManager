namespace Database.Implementations.Internal.Domain
{
    using System.Collections.Generic;

    public class Table
    {
        public Table()
        {
            Header = new List<Column>();
            Rows = new List<Row>();
        }

        public string Name { get; set; }
        public List<Column> Header { get; set; }
        public List<Row> Rows { get; set; }
    }
}