namespace Database.Interfaces.Internal
{
    using System.Collections.Generic;

    public interface IDatabaseEngine
    {
        IDatabase Database { get; set; }
        void Execute(ICollection<IScriptInstruction> instructions);
        IQueryResult Query(IQueryInstruction query);
    }
}