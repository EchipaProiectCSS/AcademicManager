namespace Database.Implementations
{
    using System;
    using System.Linq;

    using Database.Implementations.Internal;
    using Database.Interfaces.Internal;

    public class FileSystemDatabase : Database
    {
        private readonly IDatabaseEngine databaseEngine;

        private readonly ILoader fileLoader;

        private readonly IInstructionParser instructionParser;

        private readonly IQueryParser queryParser;

        public FileSystemDatabase(
            ILoader loader, 
            IInstructionParser instructionParser, 
            IQueryParser queryParser, 
            IDatabaseEngine databaseEngine)
        {
            if (loader == null)
            {
                throw new ArgumentNullException("loader", "Must provide an instance of ILoader.");
            }

            if (instructionParser == null)
            {
                throw new ArgumentNullException("instructionParser", "Must provide an instance of IInstructionParser.");
            }

            if (queryParser == null)
            {
                throw new ArgumentNullException("queryParser", "Must provide an instance of IQueryParser.");
            }

            if (databaseEngine == null)
            {
                throw new ArgumentNullException("databaseEngine", "Must provide an instance of IDatabaseEngine.");
            }

            fileLoader = loader;
            this.instructionParser = instructionParser;
            this.queryParser = queryParser;
            this.databaseEngine = databaseEngine;
            this.databaseEngine.Database = this;
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
            var instructions = instructionParser.Parse(scriptBody);

            if ((instructions == null) || (instructions.Count <= 0))
            {
                return;
            }

            databaseEngine.Execute(instructions);
        }

        public override IQueryResult Query(string scriptBody)
        {
            var query = queryParser.Parse(scriptBody).Single();

            return databaseEngine.Query(query);
        }
    }
}