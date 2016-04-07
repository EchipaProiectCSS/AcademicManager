namespace Database.Interfaces.Internal
{
    using System.Collections.Generic;

    public interface IQueryParser
    {
        /// <summary>
        ///     Parses a script and createas appropriate script queries for the database engine.
        /// </summary>
        /// <param name="scriptBody">The script body.</param>
        /// <returns>Script queries for database engine.</returns>
        ICollection<IQueryInstruction> Parse(string scriptBody);
    }
}