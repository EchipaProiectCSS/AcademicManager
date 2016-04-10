using System.Collections.Generic;

namespace ProcessManagement.Helper
{
    public class Queries
    {
        public static string GetAllStudents = "select * from students;";
        public static string GetStudentById = "select * from students where Id = '{0}';";

        public static string GetAllStatuses = "select * from studentStatuses;";
        public static string GetStatusesByStudentId = "select * from studentStatuses where StudentId = '{0}';";
        public static string GetStatusById = "select * from studentStatuses where Id = '{0}';";

        public static string GetAllClasses = "select * from studentClasses;";
        public static string GetClassesByStudentId = "select * from studentClasses where StudentId = '{0}';";
        public static string GetClassById = "select * from studentClasses where Id = '{0}';";
    }
}
