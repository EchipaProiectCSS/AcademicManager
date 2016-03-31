namespace Database.Implementations
{
    using System;
    using System.Linq;
    using Interfaces.Internal;
    using Internal;

    public class FileSystemDatabase : Database
    {
        private readonly IDatabaseEngine databaseEngine;
        private readonly ILoader fileLoader;
        private readonly IScriptParser scriptParser;

        public FileSystemDatabase(ILoader loader, IScriptParser scriptParser, IDatabaseEngine databaseEngine)
        {
            if (loader == null)
            {
                throw new ArgumentNullException("loader", "Must provide an instance of ILoader.");
            }

            if (scriptParser == null)
            {
                throw new ArgumentNullException("scriptParser", "Must provide an instance of IScriptParser.");
            }

            if (databaseEngine == null)
            {
                throw new ArgumentNullException("databaseEngine", "Must provide an instance of IDatabaseEngine.");
            }

            fileLoader = loader;
            this.scriptParser = scriptParser;
            this.databaseEngine = databaseEngine;
        }

        public override void RunScriptFile(string scriptFilePath)
        {
            var scriptBody = fileLoader.Load(scriptFilePath);

            if (string.IsNullOrWhiteSpace(scriptBody))
            {
                return;
            }

            Execute(scriptBody);
        }

        public override void Execute(string scriptBody)
        {
            var instructions = scriptParser.Parse(scriptBody);

            if (instructions == null || instructions.Count <= 0)
            {
                return;
            }

            databaseEngine.Execute(instructions);
        }

        public override IQueryResult Query(string scriptBody)
        {
            var query = scriptParser.Parse(scriptBody).Single();

            return databaseEngine.Query(query);
        }
    }
}