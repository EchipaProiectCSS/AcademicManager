namespace Database.Interfaces.Internal
{
    public interface IQueryInstruction
    {
        IDatabase Database { get; set; }
        IQueryResult Execute();
    }
}