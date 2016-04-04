namespace Database.Interfaces.Internal
{
    using Implementations.Internal.Domain;

    /// <summary>
    ///     Defines the result of a query.
    /// </summary>
    public interface IQueryResult
    {
        Table Result { get; set; }
    }
}