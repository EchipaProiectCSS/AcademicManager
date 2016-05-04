namespace Database.Implementations.Internal.Queries
{
    using System;

    using global::Database.Implementations.Internal.Utility;
    using global::Database.Interfaces;
    using global::Database.Interfaces.Internal;

    public abstract class BaseQuery : IQueryInstruction
    {
        private readonly string queryContent;

        protected BaseQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException("query", "Must provide query body.");
            }

            if (!query.EndsWith(Instructions.StatementTerminator.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("query", "Query must end with ';'.");
            }

            queryContent = query;
        }

        public string Query
        {
            get
            {
                return queryContent;
            }
        }

        public IDatabase Database { get; set; }

        public abstract IQueryResult Execute();
    }
}