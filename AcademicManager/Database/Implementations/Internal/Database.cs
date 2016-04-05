namespace Database.Implementations.Internal
{
    using Interfaces;
    using Interfaces.Internal;

    public abstract class Database : IDatabase
    {
        private string connectionString = string.Empty;

        public string ConnectionString
        {
            get
            {
                return string.IsNullOrWhiteSpace(connectionString)
                    ? connectionString = @"C:\Databases"
                    : connectionString;
            }
            set { connectionString = value; }
        }

        public string Name { get; set; }

        public abstract void RunScriptFile(string scriptFilePath);

        public abstract void Execute(string script);

        public abstract IQueryResult Query(string script);
    }
}