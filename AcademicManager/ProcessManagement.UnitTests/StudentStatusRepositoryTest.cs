using NUnit.Framework;
using ProcessManagement.DOs;
using ProcessManagement.Implementations;
using ProcessManagement.Interfaces;

namespace ProcessManagement.UnitTests
{
    [TestFixture]
    public class StudentStatusRepositoryTest
    {
        private IDatabaseModel databaseModel;
        private StudentStatusRepository studentStatus;

        [SetUp]
        public void Init()
        {
            databaseModel = new DatabaseModel();
            studentStatus = new StudentStatusRepository(databaseModel);
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

        [Test]
        public void UpdateStudentDataTest()
        {
            //TODO: not complete
            StudentStatusDo studentData = new StudentStatusDo()
            {
                Id = 0,
                Credits = 40,
                StudentId = 1,
                ECTS = 8
            };

            studentStatus.Update(studentData);
        }

        [Test]
        public void InsertStudentDataTest()
        {
            //TODO: not complete
            StudentStatusDo studentData = new StudentStatusDo()
            {
                Id = 2,
                Credits = 33,
                StudentId = 1,
                ECTS = 8,
                Year = 2016
            };

            studentStatus.Insert(studentData);
        }

    }
}
