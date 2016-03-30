namespace Database.Implementations.Internal
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Internal;

    public class DatabaseEngine : IDatabaseEngine
    {
        public void Execute(ICollection<IScriptInstruction> instructions)
        {
            throw new NotImplementedException();
        }

        public IQueryResult Query(IScriptInstruction query)
        {
            throw new NotImplementedException();
        }
    }
}