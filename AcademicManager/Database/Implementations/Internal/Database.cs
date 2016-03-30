namespace Database.Implementations.Internal
{
    using Interfaces;
    using Interfaces.Internal;

    public abstract class Database : IDatabase
    {
        public string ConnectionString { get; set; }

        public string Name { get; set; }

        public abstract void RunScriptFile(string scriptFilePath);

        public abstract void Execute(string script);

        public abstract IQueryResult Query(string script);
    }
}