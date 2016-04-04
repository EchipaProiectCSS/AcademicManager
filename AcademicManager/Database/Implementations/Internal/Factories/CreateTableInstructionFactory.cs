namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class CreateTableInstructionFactory : InstructionFactory<CreateTableInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.CreateTable),
            RegexOptions.IgnoreCase);

        public override CreateTableInstruction Create(string input)
        {
            return new CreateTableInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}