namespace Database.Implementations.Internal
{
    using System.Collections.Generic;
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
            foreach (var instruction in instructions)
            {
                instruction.Database = Database;
                instruction.Run();
            }
        }

        public IQueryResult Query(IQueryInstruction query)
        {
            query.Database = Database;
            return query.Execute();
        }
    }
}