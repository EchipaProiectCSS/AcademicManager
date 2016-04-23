using System;
using NUnit.Framework;
using ProcessManagement.Implementations;

//Integration testing

namespace ProcessManagement.UnitTests
{
    [TestFixture]
    public class StudentRepositoryTests
    {
        private StudentRepository student;

        [SetUp]
        public void Init()
        {
            student = (StudentRepository) new DatabaseContext().Student;
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
        public void UpdateStudentNullDataTest()
        {
            var ex = Assert.Throws<Exception>(() => student.Update(null));

            Assert.That(ex.Message, Is.EqualTo("An update with null value can't be made!"));
        }

        [Test]
        public void InsertStudentNullDataTest()
        {
            var ex = Assert.Throws<Exception>(() => student.Insert(null));

            Assert.That(ex.Message, Is.EqualTo("An insert with null value can't be made!"));
        }
    }
}
