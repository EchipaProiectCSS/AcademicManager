namespace InputOutputManagement.UnitTests
{
    using System.Collections.Generic;

    using Moq;

    using NUnit.Framework;

    using ProcessManagement.DOs;
    using ProcessManagement.Interfaces;

    [TestFixture]
    class Form2Tests
    {
        private Mock<IDatabaseContext> databaseContextMock;

        [SetUp]
        public void Init()
        {
            databaseContextMock = new Mock<IDatabaseContext>();
        }

        [Test]
        public void test_GetLastStudentId()
        {
            databaseContextMock.Setup(t => t.Student.GetAll()).Returns(GetStudentData);
            var formInsertStudent = new InsertStudent(databaseContextMock.Object);
            var nextStudentId = formInsertStudent.getLastStudentId();

            Assert.IsTrue(nextStudentId == 3);
        }

        public void test_InsertStudent()
        {
            databaseContextMock.Setup(t => t.Student.GetAll()).Returns(GetStudentData);
            var formInsertStudent = new InsertStudent(databaseContextMock.Object);
            var nextStudentId = formInsertStudent.getLastStudentId();

            var newStudent = new StudentDo()
                                 {
                                     Id = nextStudentId, 
                                     FirstName = "Mihai", 
                                     LastName = "Popa", 
                                     Age = "25", 
                                     EmailAddress = "mpopa@yahoo.com", 
                                     Gender = "M"
                                 };

            Assert.IsTrue(formInsertStudent.insertStudent(newStudent));
        }

        public List<StudentDo> GetStudentData()
        {
            return new List<StudentDo>()
                       {
                           new StudentDo() { Id = 1, FirstName = "Cristian", LastName = "Merticaru" }, 
                           new StudentDo() { Id = 2, FirstName = "Radu" }
                       };
        }
    }
}