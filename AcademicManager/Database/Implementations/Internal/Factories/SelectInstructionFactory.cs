namespace Database.Implementations.Internal.Factories
{
    using System.Text.RegularExpressions;
    using Queries;
    using Utility;

    public class SelectInstructionFactory : QueryFactory<SelectQuery, string>
    {
        private static readonly Regex Pattern = new Regex(string.Format("{0}.*", Instructions.Select),
            RegexOptions.IgnoreCase);

        public override SelectQuery Create(string input)
        {
            return new SelectQuery(input);
        }

        public override bool IsMatch(string input)
        {
            return Pattern.Match(input).Success;
        }
    }
}