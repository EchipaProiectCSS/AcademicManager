namespace Database.Implementations.Internal.Instructions
{
    using Interfaces;
    using Interfaces.Internal;

    public class InsertInstruction : BaseInstruction, IScriptInstruction
    {
        public InsertInstruction(string instruction) : base(instruction)
        {
        }

        public IDatabase Database { get; set; }

        public void Run()
        {
        }
    }
}