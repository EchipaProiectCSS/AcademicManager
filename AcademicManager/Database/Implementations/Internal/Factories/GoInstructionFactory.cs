namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class GoInstructionFactory : InstructionFactory<GoInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.Go),
            RegexOptions.IgnoreCase);

        public override GoInstruction Create(string input)
        {
            return new GoInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}