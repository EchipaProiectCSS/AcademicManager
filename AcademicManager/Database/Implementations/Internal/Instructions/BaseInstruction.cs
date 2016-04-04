namespace Database.Implementations.Internal.Instructions
{
    using System;

    public abstract class BaseInstruction
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
    }
}