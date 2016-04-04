namespace Database.Implementations.Internal.Instructions
{
    using Interfaces;
    using Interfaces.Internal;

    public class SelectInstruction : BaseInstruction, IScriptInstruction
    {
        public SelectInstruction(string instruction) : base(instruction)
        {
        }

        public IDatabase Database { get; set; }

        public void Run()
        {
        }
    }
}