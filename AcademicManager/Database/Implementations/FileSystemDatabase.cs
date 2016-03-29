namespace Database.Implementations
{
    using System;
    using Interfaces.Internal;

    public class FileSystemDatabase : Database
    {
        private readonly ILoader fileLoader;

        public FileSystemDatabase(ILoader loader)
        {
            fileLoader = loader;
        }

        public override void RunScriptFile(string scriptFilePath)
        {
            var scriptContent = fileLoader.Load(scriptFilePath);

            if (string.IsNullOrWhiteSpace(scriptContent))
            {
                return;
            }

            Execute(scriptContent);
        }

        public override void Execute(string script)
        {
            throw new NotImplementedException();
        }

        public override IQueryResult Query(string script)
        {
            throw new NotImplementedException();
        }
    }
}