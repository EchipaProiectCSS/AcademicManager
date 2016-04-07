namespace Database.Implementations.Internal.Utility
{
    using System.Collections.Generic;
    using Factories;
    using Interfaces.Internal;

    public static class QueryFactories
    {
        private static readonly List<IFactory<IQueryInstruction, string>> factories = new List
            <IFactory<IQueryInstruction, string>>
        {
            new SelectInstructionFactory()
        };

        public static List<IFactory<IQueryInstruction, string>> Factories
        {
            get { return factories; }
        }
    }
}