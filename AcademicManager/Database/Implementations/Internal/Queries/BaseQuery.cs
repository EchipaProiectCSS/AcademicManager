namespace Database.Implementations.Internal.Queries
{
    using System;
    using Interfaces;
    using Interfaces.Internal;

    public abstract class BaseQuery : IQueryInstruction
    {
        private readonly string queryContent;

        protected BaseQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException("query",
                    "Must provide query body.");
            }

            queryContent = query;
        }

        public string Query
        {
            get { return queryContent; }
        }

        public IDatabase Database { get; set; }
        public abstract IQueryResult Execute();
    }
}