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
    public class StudentStatusRepositoryTest
    {
        private Mock<IDatabase> databaseMock;

        [SetUp]
        public void Init()
        {
            databaseMock = new Mock<IDatabase>();
        }

        //Get(id) -- not null result
        [Test]
        public void GetStatus_NotNullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetStatusMock);

            var result = new StudentStatusRepository(databaseMock.Object).Get(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == 1);
        }

        //Get(id) -- null result
        [Test]
        public void GetStatus_NullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var result = new StudentStatusRepository(databaseMock.Object).Get(1);

            Assert.IsNull(result);
        }

        //GetAll() -- not null result
        [Test]
        public void GetAllStatuses_NotNullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetStatusMock);

            var result = new StudentStatusRepository(databaseMock.Object).GetAll();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        //GetAll() -- null result
        [Test]
        public void GetAllStatuses_NullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var result = new StudentStatusRepository(databaseMock.Object).GetAll();

            Assert.Null(result);
        }

        [Test]
        public void GetStudentStatuses_NotNullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetStatusMock);

            var result = new StudentStatusRepository(databaseMock.Object).GetStudentStatuses(2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetStudentStatuses_NullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var result = new StudentStatusRepository(databaseMock.Object).GetStudentStatuses(2);

            Assert.Null(result);
        }

        //Update() -- null result
        [Test]
        public void UpdateStatus_WithNullData_ExceptionRaisedExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var ex = Assert.Throws<Exception>(() => new StudentStatusRepository(databaseMock.Object).Update(null));

            Assert.That(ex.Message, Is.EqualTo("An update with null value can't be made!"));
        }

        //Insert() -- null result
        [Test]
        public void InsertStatus_WithNullData_ExceptionRaisedExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var ex = Assert.Throws<Exception>(() => new StudentStatusRepository(databaseMock.Object).Insert(null));

            Assert.That(ex.Message, Is.EqualTo("An insert with null value can't be made!"));
        }

        private IQueryResult GetStatusMock()
        {
            IQueryResult data = new SelectResult();

            data.Result.Rows = new List<Row>
            {
                new Row
                {
                    Values = new Dictionary<string, string>
                    {
                        {"Id", "1"},
                        {"StudentId", "2"},
                        {"Credits", "123"},
                        {"ECTS", "9"},
                        {"Year", "2013"}
                    }
                }
            };

            return data;
        }

    }
}
