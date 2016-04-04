namespace DataBase.UnitTests
{
    using Database.Implementations.Internal;
    using NUnit.Framework;

    [TestFixture]
    public class ScriptParserUnitTests
    {
        [SetUp]
        public void Initialize()
        {
            scriptParser = new ScriptParser();
        }

        private ScriptParser scriptParser;

        [Test]
        public void ShouldParseSelectInstruction()
        {
            var instruction = @"select * from students where id = '0';";
            var result = scriptParser.Parse(instruction);

            Assert.IsNotNull(result);
        }
    }
}