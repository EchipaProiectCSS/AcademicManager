namespace Database.Implementations.Internal.Factories
{
    using Interfaces.Internal;

    public abstract class InstructionFactory<TOut, TIn> : IFactory<TOut, TIn>
        where TOut : IScriptInstruction
    {
        public abstract TOut Create(TIn input);

        public abstract bool IsMatch(string input);
    }
}