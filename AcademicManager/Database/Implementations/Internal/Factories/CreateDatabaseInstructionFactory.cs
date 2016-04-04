namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class CreateDatabaseInstructionFactory : InstructionFactory<CreateDatabaseInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.CreateDatabase),
            RegexOptions.IgnoreCase);

        public override CreateDatabaseInstruction Create(string input)
        {
            return new CreateDatabaseInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}