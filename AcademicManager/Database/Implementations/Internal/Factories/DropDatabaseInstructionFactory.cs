namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class DropDatabaseInstructionFactory : InstructionFactory<DropDatabaseInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.DropDatabase),
            RegexOptions.IgnoreCase);

        public override DropDatabaseInstruction Create(string input)
        {
            return new DropDatabaseInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}