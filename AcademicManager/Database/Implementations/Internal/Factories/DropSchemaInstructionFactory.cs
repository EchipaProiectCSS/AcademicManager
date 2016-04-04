namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class DropSchemaInstructionFactory : InstructionFactory<DropSchemaInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.DropSchema),
            RegexOptions.IgnoreCase);

        public override DropSchemaInstruction Create(string input)
        {
            return new DropSchemaInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}