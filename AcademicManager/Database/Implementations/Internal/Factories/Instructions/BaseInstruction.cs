namespace Database.Implementations.Internal.Instructions
{
    using System;
    using System.Diagnostics;
    using Interfaces;
    using Interfaces.Internal;
    using Utility;

    public abstract class BaseInstruction : IScriptInstruction
    {
        protected BaseInstruction(string instruction)
        {
            if (string.IsNullOrWhiteSpace(instruction))
            {
                throw new ArgumentNullException("instruction",
                    "Must provide instruction body.");
            }

            //todo: assertion
            Debug.Assert(instruction.EndsWith(Instructions.StatementTerminator.ToString(), StringComparison.Ordinal),
                string.Format("An instruction must end with statement terminator: {0}",
                    Instructions.StatementTerminator));

            if (!instruction.EndsWith(Instructions.StatementTerminator.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Instruction must end with ';'.");
            }

            Instruction = instruction;
        }

        public string Instruction { get; }

        public IDatabase Database { get; set; }
        public abstract void Run();
    }
}