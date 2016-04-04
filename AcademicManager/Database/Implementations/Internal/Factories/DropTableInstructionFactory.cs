namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class DropTableInstructionFactory : InstructionFactory<DropTableInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.DropTable),
            RegexOptions.IgnoreCase);

        public override DropTableInstruction Create(string input)
        {
            return new DropTableInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}