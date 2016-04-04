namespace Database.Implementations.Internal.Instructions
{
    using Interfaces;
    using Interfaces.Internal;

    public class CreateSchemaInstruction : BaseInstruction, IScriptInstruction
    {
        public CreateSchemaInstruction(string instruction) : base(instruction)
        {
        }

        public IDatabase Database { get; set; }

        public void Run()
        {
        }
    }
}