using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProcessManagement.DOs;
using ProcessManagement.Interfaces;

namespace InputOutputManagement.UnitTests
{
    [TestFixture]
    public class Form1Tests
    {
        private Mock<IDatabaseContext> databaseContextMock;
        private List<int> studentsToAdd = null;
        private List<int> studentsStatusToAdd = null;
        private List<int> studentsClassToAdd = null;

        [SetUp]
        public void Init()
        {
            databaseContextMock = new Mock<IDatabaseContext>();
        }

        [Test]
        public void test_GetStudentata()
        {
            databaseContextMock.Setup(t => t.Student.GetAll()).Returns(GetStudentData);

            Form1 form1 = new Form1(databaseContextMock.Object);
            form1.firstNameText = "Cristian";
            studentsToAdd = form1.GetStudentsToAdd();
          
            Assert.IsTrue(studentsToAdd.Count == 1);
        }

        [Test]
        public void test_GetStudentStatusToAdd()
        {
            databaseContextMock.Setup(t => t.StudentStatus.GetAll()).Returns(GetStudentStatusData);
            Form1 form1 = new Form1(databaseContextMock.Object);
            form1.combStatusText = "Promoted";
            studentsStatusToAdd = form1.GetStudentsStatusToAdd();

            Assert.IsTrue(studentsStatusToAdd.Count == 1);
        }

        [Test]
        public void test_GetStudentClassToAdd()
        {
            databaseContextMock.Setup(t => t.StudentClass.GetAll()).Returns(GetStudentClassData);
            Form1 form1 = new Form1(databaseContextMock.Object);
            form1.combPromotedText = "Yes";
            studentsClassToAdd = form1.GetStudentClassToAdd();

            Assert.IsTrue(studentsClassToAdd.Count == 1);
        }

        [Test]
        public void test_SearchAfterName()
        {
            databaseContextMock.Setup(t => t.Student.GetAll()).Returns(GetStudentData);
            databaseContextMock.Setup(t => t.StudentStatus.GetAll()).Returns(GetStudentStatusData);
            databaseContextMock.Setup(t => t.StudentClass.GetAll()).Returns(GetStudentClassData);

            Form1 form1 = new Form1(databaseContextMock.Object);       
            form1.firstNameText = "Cristian";
            var result = form1.GetLastSearchResult();

            Assert.IsTrue(form1.resultStudents.Count == 1);
        }

        [Test]
        public void test_SearchAfterNameThosePromoted()
        {
            databaseContextMock.Setup(t => t.Student.GetAll()).Returns(GetStudentData);
            databaseContextMock.Setup(t => t.StudentStatus.GetAll()).Returns(GetStudentStatusData);
            databaseContextMock.Setup(t => t.StudentClass.GetAll()).Returns(GetStudentClassData);

            Form1 form1 = new Form1(databaseContextMock.Object);
            form1.firstNameText = "Cristian";
            form1.combPromotedText = "Yes";
            var result = form1.GetLastSearchResult();

            Assert.IsTrue(form1.resultStudents.Count == 2);
        }

        [Test]
        public void test_SearchShouldReturnNoValue()
        {
            databaseContextMock.Setup(t => t.Student.GetAll()).Returns(GetStudentData);
            databaseContextMock.Setup(t => t.StudentStatus.GetAll()).Returns(GetStudentStatusData);
            databaseContextMock.Setup(t => t.StudentClass.GetAll()).Returns(GetStudentClassData);


            Form1 form1 = new Form1(databaseContextMock.Object); ;
            form1.firstNameText = "Cristian";
            form1.combPromotedText = "No";
            var result = form1.GetLastSearchResult();
            Assert.IsTrue(form1.resultStudents.Count == 0);
        }

        public List<StudentDo> GetStudentData()
        {
            return new List<StudentDo>()
            {
                new StudentDo() { Id = 1, FirstName="Cristian", LastName="Merticaru"},
                new StudentDo() { Id = 2, FirstName="Radu"}
            };
        }

        public List<StudentStatusDo> GetStudentStatusData()
        {
            return new List<StudentStatusDo>()
            {
                new StudentStatusDo() { Id = 1, Credits=33, StudentId=2, ECTS=8, Year=2016 },
                new StudentStatusDo() { Id = 2, Credits=33, StudentId=4, ECTS=8, Year=2016}
            };
        }


        public List<StudentClassDo> GetStudentClassData()
        {
            return new List<StudentClassDo>()
            {
                new StudentClassDo() { Id = 1, Name="Math", StudentId=1, Promoted="Yes" },
                new StudentClassDo() { Id = 2, Name="Math", StudentId=2, Promoted="No"}
            };
        }
    }
}