namespace DataBase.UnitTests
{
    using Database.Implementations.Internal.Parsers;
    using NUnit.Framework;

    [TestFixture]
    public class ScriptParserUnitTests
    {
        [SetUp]
        public void Initialize()
        {
            scriptParser = new InstructionParser();
        }

        private InstructionParser scriptParser;

        [Test]
        public void ShouldParseSelectInstruction()
        {
            var instruction = @"select * from students where id = '0';";
            var result = scriptParser.Parse(instruction);

            Assert.IsNotNull(result);
        }
    }
}