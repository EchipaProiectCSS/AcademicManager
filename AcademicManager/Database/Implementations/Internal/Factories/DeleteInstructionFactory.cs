namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class DeleteInstructionFactory : InstructionFactory<DeleteInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.Delete),
            RegexOptions.IgnoreCase);

        public override DeleteInstruction Create(string input)
        {
            return new DeleteInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}