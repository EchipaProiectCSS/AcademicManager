using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProcessManagement.Helper;

namespace ProcessManagement.UnitTests
{
    [TestFixture]
    public class AutoMapperTests
    {
        private StudentMock studentMock;
        private Dictionary<string, string> rowMock;

        [SetUp]
        public void Init()
        {
            studentMock = new StudentMock();
            rowMock = new Dictionary<string, string>()
            {
                {"Id","1"},
                {"Age","12"}
            };
        }

        [Test]
        public void ShouldRaiseException_If_NullObjectForMapping()
        {
            var exception = Assert.Throws<Exception>(() => new AutoMapper(null, rowMock).Start());

            Assert.That(exception.Message, Is.EqualTo("The mapping can't be made with null or empty values."));
        }

        public void ShouldRaiseException_If_NullDataFromDatabaseForMapping()
        {
            var exception = Assert.Throws<Exception>(() => new AutoMapper(studentMock, null).Start());

            Assert.That(exception.Message, Is.EqualTo("The mapping can't be made with null or empty values."));
        }

        [Test]
        public void ShouldMappingSuccesfull()
        {
            new AutoMapper(studentMock, rowMock).Start();

            Assert.IsTrue(studentMock.Id == 1);
            Assert.IsTrue(studentMock.Age.Equals(12));
        }

        [Test]
        public void ShouldRaiseException_If_ObjectIsNotAccordingWithDBRow()
        {
            rowMock = new Dictionary<string, string>()
            {
                {"Id","1"},
                {"Student","LALA"}
            };

            var exception = Assert.Throws<Exception>(() => new AutoMapper(studentMock, rowMock).Start());

            Assert.That(exception.Message, Is.EqualTo("The object StudentMock should respect the name convention for Age"));
        }
    }

    public class StudentMock
    {
        public int Id { get; set; }
        public int Age { get; set; }
    }
}