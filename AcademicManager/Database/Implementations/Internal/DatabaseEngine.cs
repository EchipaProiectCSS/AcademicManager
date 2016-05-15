namespace Database.Implementations.Internal
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Interfaces;
    using Interfaces.Internal;

    public class DatabaseEngine : IDatabaseEngine
    {
        private static IDatabase database;

        public IDatabase Database
        {
            get { return database; }
            set { database = value; }
        }

        public void Execute(ICollection<IScriptInstruction> instructions)
        {
            //todo: assertion
            Debug.Assert(Database != null, "Instructions are always executed on an instance of database.");
            foreach (var instruction in instructions)
            {
                instruction.Database = Database;
                instruction.Run();
            }
        }

        public IQueryResult Query(IQueryInstruction query)
        {
            //todo: assertion
            Debug.Assert(Database != null, "Queries are always executed on an instance of database.");
            query.Database = Database;
            return query.Execute();
        }
    }
}