namespace DataBase.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Database.Implementations.Internal;
    using Database.Interfaces;
    using Database.Interfaces.Internal;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseEngineUnitTests
    {
        [SetUp]
        public void Initialize()
        {
            databaseEngine = new DatabaseEngine
            {
                Database = Substitute.For<IDatabase>()
            };
        }

        private IDatabaseEngine databaseEngine;

        [Test]
        public void InstructionDatabaseShouldBeSameAsEngineDatabase()
        {
            var instructions = new List<IScriptInstruction> {Substitute.For<IScriptInstruction>()};

            databaseEngine.Execute(instructions);

            Assert.AreSame(instructions.First().Database, databaseEngine.Database);
        }

        [Test]
        public void QueryDatabaseShouldBeSameAsEngineDatabase()
        {
            var query = Substitute.For<IQueryInstruction>();

            databaseEngine.Query(query);

            Assert.AreSame(query.Database, databaseEngine.Database);
        }

        [Test]
        public void ShouldExecuteSingleQuery()
        {
            var query = Substitute.For<IQueryInstruction>();

            databaseEngine.Query(query);

            query.Received(1).Execute();
        }

        [Test]
        public void ShouldRunMultipleInstructions()
        {
            var instructions = new List<IScriptInstruction>
            {
                Substitute.For<IScriptInstruction>(),
                Substitute.For<IScriptInstruction>()
            };

            databaseEngine.Execute(instructions);

            foreach (var instruction in instructions)
            {
                instruction.Received(1).Run();
            }
        }

        [Test]
        public void ShouldRunSingleInstruction()
        {
            var instructions = new List<IScriptInstruction> {Substitute.For<IScriptInstruction>()};

            databaseEngine.Execute(instructions);

            instructions.First().Received(1).Run();
        }
    }
}