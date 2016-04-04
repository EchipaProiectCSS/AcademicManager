namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Instructions;
    using Utility;

    public class UpdateInstructionFactory : InstructionFactory<UpdateInstruction, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.Update),
            RegexOptions.IgnoreCase);

        public override UpdateInstruction Create(string input)
        {
            return new UpdateInstruction(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}