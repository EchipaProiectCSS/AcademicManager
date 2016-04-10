using NUnit.Framework;
using ProcessManagement.DOs;
using ProcessManagement.Implementations;
using ProcessManagement.Interfaces;

namespace ProcessManagement.UnitTests
{
    [TestFixture]
    public class StudentClassRepositoryTest
    {
        private IDatabaseModel databaseModel;
        private StudentClassRepository studentClass;

        [SetUp]
        public void Init()
        {
            databaseModel = new DatabaseModel();
            studentClass = new StudentClassRepository(databaseModel);
        }

        [Test]
        public void GetAllClassesTest()
        {
            var result = studentClass.GetAll();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllClassesByStudentIdTest()
        {
            var result = studentClass.GetStudentClasses(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetClassTest()
        {
            var result = studentClass.Get(0);

            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateStudentDataTest()
        {
            //TODO: not complete
            var studentData = new StudentClassDo()
            {
                Id = 2,
                Name = "Math",
                StudentId = 0,
                Promoted = "Yes"
            };

            studentClass.Update(studentData);
        }

        [Test]
        public void InsertStudentDataTest()
        {
            //TODO: not complete
            var studentData = new StudentClassDo()
            {
                Id = 2,
                Name = "Math",
                StudentId = 1,
                Promoted = "No"
            };
            ;

            studentClass.Insert(studentData);
        }

    }
}
