namespace Database.Implementations.Internal.Instructions
{
    using System;
    using Interfaces;
    using Interfaces.Internal;

    public abstract class BaseInstruction : IScriptInstruction
    {
        private readonly string instructionContent;

        protected BaseInstruction(string instruction)
        {
            if (string.IsNullOrWhiteSpace(instruction))
            {
                throw new ArgumentNullException("instruction",
                    "Must provide instruction body.");
            }

            instructionContent = instruction;
        }

        public string Instruction
        {
            get { return instructionContent; }
        }

        public IDatabase Database { get; set; }
        public abstract void Run();
    }
}