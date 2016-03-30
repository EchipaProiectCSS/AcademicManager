namespace Database.Interfaces.Internal
{
    using System.Collections.Generic;

    public interface IScriptParser
    {
        /// <summary>
        /// Parses a script and createas appropriate script instructions for the database engine.
        /// </summary>
        /// <param name="scriptBody">The script body.</param>
        /// <returns>Script Instructions for database engine.</returns>
        ICollection<IScriptInstruction> Parse(string scriptBody);
    }
}