namespace Database.Implementations.Internal
{
    using System.Diagnostics;
    using System.IO;
    using Interfaces.Internal;

    public class FileLoader : ILoader
    {
        public string Load(string filePath)
        {
            Debug.Assert(!File.Exists(filePath), "The scripts file must exist.");
            return File.ReadAllText(filePath);
        }
    }
}