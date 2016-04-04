namespace Database.Interfaces.Internal
{
    public interface IFactory<out TOut, in TIn> : IMatch<string>
    {
        TOut Create(TIn input);
    }
}