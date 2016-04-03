namespace Database.Interfaces.Internal
{
    public interface IScriptInstruction
    {
        IDatabase Database { get; set; }
        void Run();
    }
}