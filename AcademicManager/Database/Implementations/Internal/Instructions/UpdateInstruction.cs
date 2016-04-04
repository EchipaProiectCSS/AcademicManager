namespace Database.Implementations.Internal.Instructions
{
    using Interfaces;
    using Interfaces.Internal;

    public class UpdateInstruction : BaseInstruction, IScriptInstruction
    {
        public UpdateInstruction(string instruction) : base(instruction)
        {
        }

        public IDatabase Database { get; set; }

        public void Run()
        {
        }
    }
}