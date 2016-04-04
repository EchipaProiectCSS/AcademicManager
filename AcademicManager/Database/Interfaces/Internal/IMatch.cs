namespace Database.Interfaces.Internal
{
    public interface IMatch<in T>
    {
        bool IsMatch(T input);
    }
}