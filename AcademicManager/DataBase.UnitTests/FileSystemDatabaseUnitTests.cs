namespace DataBase.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Database.Implementations;
    using Database.Implementations.Internal.Instructions;
    using Database.Implementations.Internal.Queries;
    using Database.Interfaces;
    using Database.Interfaces.Internal;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class FileSystemDatabaseUnitTests
    {
        [SetUp]
        public void Initialize()
        {
            fileLoader = Substitute.For<ILoader>();
            instructionParser = Substitute.For<IInstructionParser>();
            queryParser = Substitute.For<IQueryParser>();
            databaseEngine = Substitute.For<IDatabaseEngine>();
            database = new FileSystemDatabase(fileLoader, instructionParser, queryParser, databaseEngine);
        }

        private IDatabase database;
        private ILoader fileLoader;
        private IInstructionParser instructionParser;
        private IQueryParser queryParser;
        private IDatabaseEngine databaseEngine;

        [Test]
        public void ShouldCallDatabaseEngineExecuteIfInstructionsListIsNotEmpty()
        {
            instructionParser.Parse(Arg.Any<string>())
                .Returns(new List<IScriptInstruction>
                {
                    new CreateDatabaseInstruction("create database AcademicManagerDatabase;")
                });

            database.Execute(string.Empty);

            instructionParser.Received(1).Parse(Arg.Any<string>());

            databaseEngine.Received(1).Execute(Arg.Any<ICollection<IScriptInstruction>>());
        }

        [Test]
        public void ShouldCallDatabaseExecuteIfLoaderReturnsNonEmpty()
        {
            fileLoader.Load(Arg.Any<string>()).Returns("create database AcademicManagerDatabase;");

            database.RunScriptFile(string.Empty);

            fileLoader.Received(1).Load(Arg.Any<string>());

            instructionParser.Received(1).Parse(Arg.Any<string>());
        }

        [Test]
        public void ShouldCallQueryRunIfParserReturnsAQuery()
        {
            queryParser.Parse(Arg.Any<string>()).Returns(new List<IQueryInstruction>
            {
                new SelectQuery(@"select * from students where id = '0';")
            });

            database.Query(string.Empty);

            queryParser.Received(1).Parse(Arg.Any<string>());

            databaseEngine.Received(1).Query(Arg.Any<IQueryInstruction>());
        }

        [Test]
        public void ShouldNotCallDatabaseEngineExecuteIfInstructionsListIsEmpty()
        {
            instructionParser.Parse(Arg.Any<string>()).Returns(new List<IScriptInstruction>());

            database.Execute(string.Empty);

            instructionParser.Received(1).Parse(Arg.Any<string>());

            databaseEngine.DidNotReceiveWithAnyArgs().Execute(Arg.Any<ICollection<IScriptInstruction>>());
        }

        [Test]
        public void ShouldNotCallDatabaseExecuteIfLoaderReturnsEmpty()
        {
            fileLoader.Load(Arg.Any<string>()).Returns(string.Empty);

            database.RunScriptFile(string.Empty);

            fileLoader.Received(1).Load(Arg.Any<string>());

            instructionParser.DidNotReceive().Parse(Arg.Any<string>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowIfDatabaseEngineIsNull()
        {
            database = new FileSystemDatabase(null, instructionParser, queryParser, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowIfFileLoaderIsNull()
        {
            database = new FileSystemDatabase(null, instructionParser, queryParser, databaseEngine);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowIfInstructionParserIsNull()
        {
            database = new FileSystemDatabase(fileLoader, null, queryParser, databaseEngine);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowIfQueryParserIsNull()
        {
            database = new FileSystemDatabase(fileLoader, instructionParser, null, databaseEngine);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowWhenCallingMultipleQueries()
        {
            queryParser.Parse(Arg.Any<string>()).Returns(new List<IQueryInstruction>
            {
                new SelectQuery(@"select * from students where id = '0';"),
                new SelectQuery(@"select * from students where id = '0';")
            });

            database.Query(string.Empty);

            queryParser.Received(1).Parse(Arg.Any<string>());
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowWhenCallingWithEmptyQueriesList()
        {
            queryParser.Parse(Arg.Any<string>()).Returns(new List<IQueryInstruction>());

            database.Query(string.Empty);

            queryParser.Received(1).Parse(Arg.Any<string>());
        }
    }
}