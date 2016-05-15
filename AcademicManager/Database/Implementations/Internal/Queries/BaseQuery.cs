namespace Database.Implementations.Internal.Queries
{
    using System;
    using System.Diagnostics;
    using Interfaces;
    using Interfaces.Internal;
    using Utility;

    public abstract class BaseQuery : IQueryInstruction
    {
        protected BaseQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException("query", "Must provide query body.");
            }

            //todo: assertion
            Debug.Assert(query.EndsWith(Instructions.StatementTerminator.ToString(), StringComparison.Ordinal),
                string.Format("An query must end with statement terminator: {0}",
                    Instructions.StatementTerminator));

            if (!query.EndsWith(Instructions.StatementTerminator.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("query", "Query must end with ';'.");
            }

            Query = query;
        }

        public string Query { get; }

        public IDatabase Database { get; set; }

        public abstract IQueryResult Execute();
    }
}