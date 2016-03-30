namespace Database.Implementations.Internal
{
    using System.IO;
    using Interfaces.Internal;

    public class FileLoader : ILoader
    {
        public string Load(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}