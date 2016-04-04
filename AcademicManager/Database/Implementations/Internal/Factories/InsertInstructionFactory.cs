namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class InsertInstructionFactory : InstructionFactory<InsertInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.InsertInto),
            RegexOptions.IgnoreCase);

        public override InsertInstruction Create(string input)
        {
            return new InsertInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}