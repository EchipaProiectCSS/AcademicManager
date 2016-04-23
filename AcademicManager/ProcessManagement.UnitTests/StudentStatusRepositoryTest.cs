using System;
using NUnit.Framework;
using ProcessManagement.Implementations;

//Integration testing

namespace ProcessManagement.UnitTests
{
    [TestFixture]
    public class StudentStatusRepositoryTest
    {
        private StudentStatusRepository studentStatus;

        [SetUp]
        public void Init()
        {
            studentStatus = (StudentStatusRepository)new DatabaseContext().StudentStatus;
        }

        [Test]
        public void GetAllStatusesTest()
        {
            var result = studentStatus.GetAll();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllStatusesByStudentIdTest()
        {
            var result = studentStatus.GetStudentStatuses(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetStatusbyStatusIdTest()
        {
            var result = studentStatus.Get(1);

            Assert.IsNotNull(result);
        }

        public void UpdateStudentNullDataTest()
        {
            var ex = Assert.Throws<Exception>(() => studentStatus.Update(null));

            Assert.That(ex.Message, Is.EqualTo("An update with null value can't be made!"));
        }

        public void InsertStudentNullDataTest()
        {
            var ex = Assert.Throws<Exception>(() => studentStatus.Insert(null));

            Assert.That(ex.Message, Is.EqualTo("An insert with null value can't be made!"));
        }

    }
}
