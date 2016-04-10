using System.Collections.Generic;

namespace ProcessManagement.DOs
{
    public class StudentStatusDo
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int Credits { get; set; }

        public int ECTS { get; set; }

        public int Year { get; set; }
    }
}
