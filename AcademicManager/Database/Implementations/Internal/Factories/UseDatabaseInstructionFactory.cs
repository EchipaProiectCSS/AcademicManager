namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class UseDatabaseInstructionFactory : InstructionFactory<UseDatabaseInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.Use),
            RegexOptions.IgnoreCase);

        public override UseDatabaseInstruction Create(string input)
        {
            return new UseDatabaseInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}