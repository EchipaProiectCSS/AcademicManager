
using NUnit.Framework;
using ProcessManagement.DOs;
using ProcessManagement.Helper;

//Unit testing

namespace ProcessManagement.UnitTests
{
    public class GenericQueryTests
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void InsertQuerySuccesfullyMapped()
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

            var expectedQuery ="insert into students (Id, Age, Gender, FirstName, LastName, EmailAddress, LinkedStatus) values ('1', '22', 'M', 'Iliusa', 'Niculae', 'cornel@yahoo.com', '1');";

            var result = InsertQuery.Create(tableName, student);

            Assert.IsTrue(result.Equals(expectedQuery));
        }
    }
}
