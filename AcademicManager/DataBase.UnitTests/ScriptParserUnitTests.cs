namespace DataBase.UnitTests
{
    using Database.Implementations.Internal;
    using NUnit.Framework;

    [TestFixture]
    public class ScriptParserUnitTests
    {
        private ScriptParser scriptParser;

        [SetUp]
        public void Initialize()
        {
            scriptParser = new ScriptParser();
        }

        [Test]
        public void ShouldParseSelectInstruction()
        {
            var instruction = @"select * from students where id = '0';";
            var result = scriptParser.Parse(instruction);

            Assert.IsNotNull(result);
        }
    }
}
