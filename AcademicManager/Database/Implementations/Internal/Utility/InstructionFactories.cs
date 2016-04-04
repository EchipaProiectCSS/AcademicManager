namespace Database.Implementations.Internal.Utility
{
    using System.Collections.Generic;
    using Factories;
    using Interfaces.Internal;

    public static class InstructionFactories
    {
        private static readonly List<IFactory<IScriptInstruction, string>> factories = new List
            <IFactory<IScriptInstruction, string>>
        {
            new CreateDatabaseInstructionFactory(),
            new CreateSchemaInstructionFactory(),
            new CreateTableInstructionFactory(),
            new DeleteInstructionFactory(),
            new DropDatabaseInstructionFactory(),
            new DropSchemaInstructionFactory(),
            new DropTableInstructionFactory(),
            new GoInstructionFactory(),
            new InsertInstructionFactory(),
            new SelectInstructionFactory(),
            new UpdateInstructionFactory(),
            new UseDatabaseInstructionFactory()
        };

        public static List<IFactory<IScriptInstruction, string>> Factories
        {
            get { return factories; }
        }
    }
}