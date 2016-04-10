using NUnit.Framework;
using ProcessManagement.DOs;
using ProcessManagement.Implementations;
using ProcessManagement.Interfaces;

namespace ProcessManagement.UnitTests
{
    [TestFixture]
    public class StudentRepositoryTests
    {
        private IDatabaseModel databaseModel;
        private StudentRepository student;

        [SetUp]
        public void Init()
        {
            databaseModel = new DatabaseModel();
            student = new StudentRepository(databaseModel);
        }

        [Test]
        public void ChecIfStudentIsInDatabaseTest()
        {
            var result = student.Get(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public void CheckIfStudentIsNotInDatabaseTest()
        {
            var result = student.Get(100);

            Assert.IsNull(result);
        }

        [Test]
        public void UpdateStudentDataTest()
        {  
            //TODO: not complete
            StudentDo studentData = new StudentDo()
            {
                Id = 0,
                Age = "22",
                FirstName = "Cornel"
            };

            student.Update(studentData);
        }

        [Test]
        public void InsertStudentDataTest()
        {
            //TODO: not complete
            StudentDo studentData = new StudentDo()
            {
                Id = 3,
                Age = "25",
                FirstName = "Cornel"
            };

            student.Insert(studentData);
        }
    }
}
