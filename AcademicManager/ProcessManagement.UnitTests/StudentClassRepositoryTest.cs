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
    public class StudentClassRepositoryTest
    {
        private Mock<IDatabase> databaseMock;

        [SetUp]
        public void Init()
        {
            databaseMock = new Mock<IDatabase>();
        }

        //GetAll() -- not null result
        [Test]
        public void GetAllClasses_NotNullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetClassesMock);

            var result = new StudentClassRepository(databaseMock.Object).GetAll();

            Assert.IsNotNull(result);
        }

        //GetAll() -- null result
        [Test]
        public void GetAllClasses_NullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var result = new StudentClassRepository(databaseMock.Object).GetAll();

            Assert.IsNull(result);
        }

        //GetStudentClasses() -- not null result
        [Test]
        public void GetStudentClasses_NotNullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetClassesMock);

            var result = new StudentClassRepository(databaseMock.Object).GetStudentClasses(2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        //GetStudentClasses() -- null result
        [Test]
        public void GetStudentClasses_NullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var result = new StudentClassRepository(databaseMock.Object).GetStudentClasses(1);

            Assert.Null(result);
        }

        //Get() -- not null result
        [Test]
        public void GetClass_NotNullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(GetClassesMock);

            var result = new StudentClassRepository(databaseMock.Object).Get(1);

            Assert.NotNull(result);
            Assert.True(result.Id == 1);
        }

        //Get() -- null result
        [Test]
        public void GetClass_NullResultExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var result = new StudentClassRepository(databaseMock.Object).Get(1);

            Assert.Null(result);
        }

        //Insert() -- Exception Raised
        [Test]
        public void InsertClass_WithNullData_ExceptionRaisedExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var ex = Assert.Throws<Exception>(() => new StudentClassRepository(databaseMock.Object).Insert(null));

            Assert.That(ex.Message, Is.EqualTo("An insert with null value can't be made!"));
        }

        //Update() -- Exception Raised
        [Test]
        public void UpdateClass_WithNullData_ExceptionRaisedExpected()
        {
            databaseMock.Setup(t => t.Query(It.IsAny<string>())).Returns(() => null);

            var ex = Assert.Throws<Exception>(() => new StudentClassRepository(databaseMock.Object).Update(null));

            Assert.That(ex.Message, Is.EqualTo("An update with null value can't be made!"));
        }

        private IQueryResult GetClassesMock()
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
                        {"Name", "Math"},
                        {"Promoted", "true"}
                    }
                }
            };
            return data;
        }

    }
}