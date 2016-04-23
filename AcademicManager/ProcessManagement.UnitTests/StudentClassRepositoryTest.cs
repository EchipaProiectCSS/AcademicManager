using System;
using Database.Implementations.Internal.Domain;
using Moq;
using NUnit.Framework;
using ProcessManagement.Implementations;
using Database.Interfaces;
using Database.Interfaces.Internal;

//Unit testing

namespace ProcessManagement.UnitTests
{
    [TestFixture]
    public class StudentClassRepositoryTest
    {
        private Mock<IDatabase> databaseMock;

        [SetUp]
        public void Init()
        {
            databaseMock = new Mock<IDatabase>();
        }

        [Test]
        public void GetAllClassesTest()
        {            
            databaseMock.Setup(t => t.Query(string.Empty)).Returns(() => null);

            var result = new StudentClassRepository(databaseMock.Object).GetAll();

            Assert.IsNull(result);
        }

        [Test]
        public void GetAllClassesByStudentIdTest()
        {
            //TODO:Check why query method is always returning null

            databaseMock.Setup(t => t.Query("test")).Returns((IQueryResult)new SelectResult());

            var result = new StudentClassRepository(databaseMock.Object).GetStudentClasses(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetClassTest()
        {
            //TODO:Check why query method is always returning null

            databaseMock.Setup(r => r.Query(string.Empty)).Returns(() => null);

            var result = new StudentClassRepository(databaseMock.Object).GetStudentClasses(0);

            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateStudentNullDataTest()
        {
            databaseMock.Setup(t => t.Query("test")).Returns(() => null);

            var ex = Assert.Throws<Exception>(() => new StudentClassRepository(databaseMock.Object).Update(null));

            Assert.That(ex.Message, Is.EqualTo("An update with null value can't be made!"));
        }

        [Test]
        public void InsertStudentNullDataTest()
        {
            databaseMock.Setup(t => t.Query(string.Empty)).Returns(() => null);

            var ex = Assert.Throws<Exception>(() => new StudentClassRepository(databaseMock.Object).Insert(null));

            Assert.That(ex.Message, Is.EqualTo("An insert with null value can't be made!"));
        }

    }
}
