namespace DataBase.UnitTests
{
    using System;
    using System.Linq;

    using Database.Implementations.Internal.Instructions;
    using Database.Implementations.Internal.Parsers;

    using NUnit.Framework;

    [TestFixture]
    public class InstructionParserUnitTests
    {
        private InstructionParser scriptParser;

        [SetUp]
        public void Initialize()
        {
            scriptParser = new InstructionParser();
        }

        [Test]
        [TestCase(@"create database AcademicManagerDatabase;", typeof(CreateDatabaseInstruction))]
        [TestCase(@"create table students (id, firstName, lastName);", typeof(CreateTableInstruction))]
        [TestCase(@"delete from students where id = '0';", typeof(DeleteInstruction))]
        [TestCase(@"drop database AcademicManagerDatabase;", typeof(DropDatabaseInstruction))]
        [TestCase(@"drop table students;", typeof(DropTableInstruction))]
        [TestCase(@"insert into students (id, firstName, lastName) values ('0', 'John', 'Doe');", 
            typeof(InsertInstruction))]
        [TestCase(@"update students set firstName = 'Jane' where firstName = 'John';", typeof(UpdateInstruction))]
        [TestCase(@"use AcademicManagerDatabase;", typeof(UseDatabaseInstruction))]
        public void ShouldCreateTheCorrectInstructionTypes(string instructionBody, Type expectedInstructionType)
        {
            var instructionsList = scriptParser.Parse(instructionBody);

            Assert.IsNotNull(instructionsList);
            Assert.IsTrue(instructionsList.Count == 1);
            Assert.IsTrue(instructionsList.First().GetType() == expectedInstructionType);
        }

        [Test]
        public void ShouldParseMultipleInstructions()
        {
            var instructionsString =
                @"create database AcademicManagerDatabase;create table students (id, firstName, lastName);";
            var instructionsList = scriptParser.Parse(instructionsString);

            Assert.IsNotNull(instructionsList);
            Assert.IsTrue(instructionsList.Count == 2);
        }

        [Test]
        public void EmptyInstructionDoesNotThrow()
        {
            var instructionsString = string.Empty;
            var instructionsList = scriptParser.Parse(instructionsString);
            Assert.IsNotNull(instructionsList);
            Assert.IsFalse(instructionsList.Any());
        }

        [Test]
        public void CommentAndEmptyLinesAreIgnored()
        {
            // BUG: missing logic from parser that removed comment and empty lines; the bug has been fixed
            var instructionsList = scriptParser.Parse(@"create database AcademicManagerDatabase;
                                       --comment line followed by an empty line
                                       ");
            Assert.IsNotNull(instructionsList);
            Assert.IsTrue(instructionsList.Count == 1);
        }

        [Test]
        public void UnknownInstructionsAreIgnored()
        {
            var instructionsList = scriptParser.Parse(@"create index StudentIdsIndex;");
            Assert.IsNotNull(instructionsList);
            Assert.IsFalse(instructionsList.Any());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowIfInstructionDoesNotEndWithSemicolon()
        {
            // BUG: original implementation did not reach the base instruction validation
            // BUG: due to a bug in the parsing logic. the bug has been fixed so this test scenario passes
            var instructionsString = @"create database AcademicManagerDatabase";
            scriptParser.Parse(instructionsString);
        }
    }
}