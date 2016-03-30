namespace Database.Interfaces.Internal
{
    using System.Collections.Generic;

    public interface IDatabaseEngine
    {
        void Execute(ICollection<IScriptInstruction> instructions);
        IQueryResult Query(IScriptInstruction query);
    }
}