namespace Database.Implementations.Internal.Instructions
{
    using Interfaces;
    using Interfaces.Internal;

    public class DropSchemaInstruction : IScriptInstruction
    {
        public IDatabase Database { get; set; }

        public void Run()
        {

        }
    }
}
