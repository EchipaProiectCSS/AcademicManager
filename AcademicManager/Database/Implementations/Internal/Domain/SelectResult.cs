namespace Database.Implementations.Internal.Domain
{
    using Interfaces.Internal;

    public class SelectResult : IQueryResult
    {
        public SelectResult()
        {
            Result = new Table();
        }

        public Table Result { get; set; }
    }
}