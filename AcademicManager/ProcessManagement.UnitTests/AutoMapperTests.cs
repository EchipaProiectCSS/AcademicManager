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
        public void CheckIfMappingSuccesfullTest()
        {
            new AutoMapper(studentMock, rowMock).Start();

            Assert.IsTrue(studentMock.Id == 1);
            Assert.IsTrue(studentMock.Age.Equals(12));
        }
    }

    public class StudentMock
    {
        public int Id { get; set; }
        public int Age { get; set; }
    }
}