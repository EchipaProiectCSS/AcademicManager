namespace Database.Implementations.Internal.Instructions
{
    using Interfaces;
    using Interfaces.Internal;

    public class DropSchemaInstruction : BaseInstruction, IScriptInstruction
    {
        public DropSchemaInstruction(string instruction) : base(instruction)
        {
        }

        public IDatabase Database { get; set; }

        public void Run()
        {
        }
    }
}