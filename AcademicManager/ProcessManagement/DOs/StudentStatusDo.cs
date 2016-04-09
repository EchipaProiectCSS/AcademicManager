using System.Collections.Generic;

namespace ProcessManagement.DOs
{
    public class StudentStatusDo
    {
        public List<ClassDo> Classes { get; set; }

        public int Credits { get; set; }

        public int ECTS { get; set; }
    }
}
