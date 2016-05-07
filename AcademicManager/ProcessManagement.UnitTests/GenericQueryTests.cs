using System;
using NUnit.Framework;
using ProcessManagement.DOs;
using ProcessManagement.Helper;

namespace ProcessManagement.UnitTests
{
    public class GenericQueryTests
    {
        [Test]
        public void InsertQuery_SuccesfullyMappedExpected()
        {
            var tableName = "students";

            StudentDo student = new StudentDo()
            {
                Id = 1,
                Age = "22",
                EmailAddress = "cornel@yahoo.com",
                FirstName = "Iliusa",
                Gender = "M",
                LastName = "Niculae",
                LinkedStatus = "1"
            };

            var expectedQuery = "insert into students (Id, Age, Gender, FirstName, LastName, EmailAddress, LinkedStatus) values ('1', '22', 'M', 'Iliusa', 'Niculae', 'cornel@yahoo.com', '1');";

            var result = InsertQuery.Create(tableName, student);

            Assert.IsTrue(result.Equals(expectedQuery));
        }

        [Test]
        public void UpdateQuery_SuccesfullyMappedExpected()
        {
            var tableName = "students";

            StudentDo student = new StudentDo()
            {
                Id = 1,
                Age = "22",
                EmailAddress = "cornel@yahoo.com",
                FirstName = "Iliusa",
                Gender = "M",
                LastName = "Niculae",
                LinkedStatus = "1"
            };

            var expectedQuery = "update students set Age = '22', Gender = 'M', FirstName = 'Iliusa', LastName = 'Niculae', EmailAddress = 'cornel@yahoo.com', LinkedStatus = '1' where Id = '1';";

            var result = UpdateQuery.Create(tableName, student);

            Assert.IsTrue(result.Equals(expectedQuery));
        }

        [Test]
        public void InsertQuery_WithTableNameNull_RaiseExceptionExpected()
        {
            var ex = Assert.Throws<Exception>(() => InsertQuery.Create(null, new StudentDo()));

            Assert.That(ex.Message, Is.EqualTo("Please make sure that table name or values used for creating the query aren't null!"));
        }
    }
}
