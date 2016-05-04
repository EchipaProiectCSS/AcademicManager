namespace DataBase.UnitTests
{
    using System;
    using System.Linq;

    using Database.Implementations.Internal.Parsers;
    using Database.Implementations.Internal.Queries;

    using NUnit.Framework;

    [TestFixture]
    public class QueryParserUnitTests
    {
        private QueryParser scriptParser;

        [SetUp]
        public void Initialize()
        {
            scriptParser = new QueryParser();
        }

        [Test]
        [TestCase(@"select * from students where id = '0';", typeof(SelectQuery))]
        [TestCase(@"select firstName, lastName from students where id = '0' and firstName = 'John';", 
            typeof(SelectQuery))]
        [TestCase(@"select * from admins where id = '1' or username = 'Jane Doe';", typeof(SelectQuery))]
        public void ShouldCreateTheCorrectQueryTypes(string queryBody, Type expectedQueryType)
        {
            var queryList = scriptParser.Parse(queryBody);

            Assert.IsNotNull(queryList);
            Assert.IsTrue(queryList.Count == 1);
            Assert.IsTrue(queryList.First().GetType() == expectedQueryType);
        }

        [Test]
        public void ShouldParseMultipleQueries()
        {
            var queriesString =
                @"select * from students where id = '0';select firstName, lastName from students where id = '0' and firstName = 'John';";
            var queriesList = scriptParser.Parse(queriesString);

            Assert.IsNotNull(queriesList);
            Assert.IsTrue(queriesList.Count == 2);
        }

        [Test]
        public void EmptyQueryDoesNotThrow()
        {
            var queryString = string.Empty;
            var queriesList = scriptParser.Parse(queryString);
            Assert.IsNotNull(queriesList);
            Assert.IsFalse(queriesList.Any());
        }

        [Test]
        public void CommentAndEmptyLinesAreIgnored()
        {
            // BUG: missing logic from parser that removed comment and empty lines; the bug has been fixed
            var instructionsList = scriptParser.Parse(@"select * from students where id = '0';
                                       --comment line followed by an empty line
                                       ");
            Assert.IsNotNull(instructionsList);
            Assert.IsTrue(instructionsList.Count == 1);
        }

        [Test]
        public void UnknownQueriesAreIgnored()
        {
            var queriesList = scriptParser.Parse(@"create index StudentIdsIndex;");
            Assert.IsNotNull(queriesList);
            Assert.IsFalse(queriesList.Any());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowIfQueryDoesNotEndWithSemicolon()
        {
            // BUG: original implementation did not have this validation; bug fixed
            var instructionsString = @"select * from students where id = '0'";
            scriptParser.Parse(instructionsString);
        }
    }
}