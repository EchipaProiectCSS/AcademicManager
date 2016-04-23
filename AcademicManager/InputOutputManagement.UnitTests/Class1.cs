using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProcessManagement.DOs;
using ProcessManagement.Interfaces;

namespace InputOutputManagement.UnitTests
{
    [TestFixture]
    public class Class1
    {
        private Mock<IDatabaseContext> databaseContextMock;

        [SetUp]
        public void Init()
        {
            databaseContextMock = new Mock<IDatabaseContext>();
        }

        [Test]
        public void Test()
        {
            databaseContextMock.Setup(t => t.Student.GetAll()).Returns(GetData);

            var result = new Form1(databaseContextMock.Object).GetStudentsId();

            Assert.IsTrue(result.Count == 2);
        }

        public List<StudentDo> GetData()
        {
            return new List<StudentDo>()
            {
                new StudentDo() { Id = 1},
                new StudentDo() { Id = 2}
            };
        }
    }
}