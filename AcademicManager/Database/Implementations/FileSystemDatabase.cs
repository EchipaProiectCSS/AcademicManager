namespace Database.Implementations
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Interfaces.Internal;
    using Internal;

    public class FileSystemDatabase : Database
    {

        [ContractInvariantMethod]
        protected void ClassInvariant()
        {
            Contract.Invariant(databaseEngine != null);
            Contract.Invariant(fileLoader != null);
            Contract.Invariant(queryParser != null);
            Contract.Invariant(instructionParser != null);
        }

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
            //todo: assertion
            Debug.Assert(loader != null, "An instance of loader must be provided.");
            Debug.Assert(instructionParser != null, "An instance of instructions parse must be provided.");
            Debug.Assert(queryParser != null, "An instance of query parser must be provided.");
            Debug.Assert(databaseEngine != null, "An instance of database engine must be provided.");

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
            //todo: assertion - pre
            Debug.Assert(string.IsNullOrEmpty(scriptBody), "The script should not be empty in order to be execute on Database");

            var query = queryParser.Parse(scriptBody).Single();

            return databaseEngine.Query(query);
        }
    }
}