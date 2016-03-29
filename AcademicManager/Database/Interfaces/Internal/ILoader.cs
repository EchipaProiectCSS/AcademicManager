namespace Database.Interfaces.Internal
{
    public interface ILoader
    {
        /// <summary>
        /// Load the entire file contents in memory.
        /// </summary>
        /// <param name="filePath">The file location on disk.</param>
        /// <returns>The entire file content.</returns>
        string Load(string filePath);
    }
}