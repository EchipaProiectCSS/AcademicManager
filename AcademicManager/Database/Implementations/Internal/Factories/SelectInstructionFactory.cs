namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class SelectInstructionFactory : InstructionFactory<SelectInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.Select),
            RegexOptions.IgnoreCase);

        public override SelectInstruction Create(string input)
        {
            return new SelectInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}