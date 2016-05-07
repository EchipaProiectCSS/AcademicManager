using System;
using System.Collections.Generic;
using Database.Implementations.Internal.Domain;
using Database.Interfaces;
using Database.Interfaces.Internal;
using Moq;
using NUnit.Framework;
using ProcessManagement.Implementations;

namespace ProcessManagement.UnitTests
{
    [TestFixture]
    public class StudentRepositoryTests
    {
        private Mock<IDatabase> databaseMock;

        [SetUp]
        public void Init()
        {
            databaseMock = new Mock<IDatabase>();
        }

        //Get(id) -- not null result
        [Test]
        public void GetStudent_NotNullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetStudentMock);

            var result = new StudentRepository(databaseMock.Object).Get(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == 1);
        }

        //Get(id) -- null result
        [Test]
        public void GetStudent_NullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var result = new StudentRepository(databaseMock.Object).Get(1);

            Assert.IsNull(result);
        }

        //GetAll() -- not null result
        [Test]
        public void GetAllStudents_NotNullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetStudentMock);

            var result = new StudentRepository(databaseMock.Object).GetAll();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        //GetAll() -- null result
        [Test]
        public void GetAllStudents_NullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetStudentMockWithNoData);

            var ex = Assert.Throws<Exception>(() => new StudentRepository(databaseMock.Object).GetAll());

            Assert.That(ex.Message, Is.EqualTo("The mapping can't be made with null or empty values."));
        }

        //Update() -- null result
        [Test]
        public void UpdateStudent_WithNullData_ExceptionRaisedExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var ex = Assert.Throws<Exception>(() => new StudentRepository(databaseMock.Object).Update(null));

            Assert.That(ex.Message, Is.EqualTo("An update with null value can't be made!"));
        }

        //Insert() -- null result
        [Test]
        public void InsertStudent_WithNullData_ExceptionRaisedExpected()
        {

            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var ex = Assert.Throws<Exception>(() => new StudentRepository(databaseMock.Object).Insert(null));

            Assert.That(ex.Message, Is.EqualTo("An insert with null value can't be made!"));
        }

        private IQueryResult GetStudentMock()
        {
            IQueryResult data = new SelectResult();

            data.Result.Rows = new List<Row>
            {
                new Row
                {
                    Values = new Dictionary<string, string>
                    {
                        {"Id", "1"},
                        {"Age", "2"},
                        {"Gender", "Male"},
                        {"FirstName", "Andrei"},
                        {"LastName", "Gorun"},
                        {"EmailAddress", "andrei@info.uaic.com"},
                        {"LinkedStatus", "1"}
                    }
                }
            };

            return data;
        }

        private IQueryResult GetStudentMockWithNoData()
        {
            IQueryResult data = new SelectResult();

            data.Result.Rows = new List<Row>
            {
                new Row
                {
                    Values = new Dictionary<string, string>()
                }
            };

            return data;
        }
    }
}
