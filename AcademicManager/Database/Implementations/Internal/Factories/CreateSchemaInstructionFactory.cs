namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class CreateSchemaInstructionFactory : InstructionFactory<CreateSchemaInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.CreateSchema),
            RegexOptions.IgnoreCase);

        public override CreateSchemaInstruction Create(string input)
        {
            return new CreateSchemaInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}