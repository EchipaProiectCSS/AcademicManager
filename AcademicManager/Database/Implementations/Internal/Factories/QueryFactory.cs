namespace Database.Implementations.Internal.Factories
{
    using Interfaces.Internal;

    public abstract class QueryFactory<TOut, TIn> : IFactory<TOut, TIn>
        where TOut : IQueryInstruction
    {
        public abstract TOut Create(TIn input);

        public abstract bool IsMatch(string input);
    }
}