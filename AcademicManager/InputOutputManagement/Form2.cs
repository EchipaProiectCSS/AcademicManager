using System.Windows.Forms;
using ProcessManagement.DOs;
using ProcessManagement.Implementations;
using ProcessManagement.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace InputOutputManagement
{
    public partial class InsertStudent : Form
    {

        private readonly IStudentRepository studentRepository;
        private readonly IStudentClassRepository studentClassRepository;
        private readonly IStudentStatusRepository studentStatusRepository;

        public InsertStudent()
        {
            DatabaseContext context = new DatabaseContext();

            this.studentRepository = context.Student;
            this.studentClassRepository = context.StudentClass;
            this.studentStatusRepository = context.StudentStatus;
            InitializeComponent();
        }

        public InsertStudent(IDatabaseContext databaseContext)
        {
            InitializeComponent();

            studentRepository = databaseContext.Student;
            studentClassRepository = databaseContext.StudentClass;
            studentStatusRepository = databaseContext.StudentStatus;         
        }


        Form1 form1 = null;

        long studentno = 0;


        public InsertStudent(Form1 frm, long ID)
        {
            InitializeComponent();
            this.form1 = frm;
            this.studentno = ID;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            studentClassRepository.GetAll();

        }

        public int getLastStudentId()
        {
            List<StudentDo> listStudents = studentRepository.GetAll();
            StudentDo student = listStudents.Last();
            int studentId = student.Id;
            return ++studentId;
        }

        public bool insertStudent(StudentDo studentData)
        {
            bool isInserted = false;
            studentRepository.Insert(studentData);
            isInserted = true;

            return isInserted;
        }

        private void Insert_Click(object sender, EventArgs e)
        {

            int nextStudentId = getLastStudentId();
            //TODO: not complete
            StudentDo studentData = new StudentDo();
            studentData.Id = nextStudentId;
            studentData.Age = textAge.Text;
            studentData.FirstName = textFirstName.Text;
            studentData.LastName = textLastName.Text;
            studentData.Gender = textGender.Text;
            studentData.EmailAddress = textEmail.Text;

            insertStudent(studentData);

            List<StudentClassDo> listClassStudents = studentClassRepository.GetAll();
            StudentClassDo studentClass = listClassStudents.Last();
 
            //studentClassRepository.Insert(studentClassData);

            MessageBox.Show("Done !");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            if (result != DialogResult.OK) return;
            var itemList = File.ReadLines(file).ToList();
            foreach (var line in itemList)
            {
                var splitter = line.Split('\t');
                var listStudents = studentRepository.GetAll();
                var student = listStudents.Last();
                var studentId = student.Id;
                StudentDo studentData = new StudentDo
                {
                    Id = ++studentId,
                    Age = splitter[0],
                    FirstName = splitter[1],
                    LastName = splitter[2],
                    Gender = splitter[3],
                    EmailAddress = splitter[4]
                };
                studentRepository.Insert(studentData);
                var listClassStudents = studentClassRepository.GetAll();
                var studentClass = listClassStudents.Last();
                var studentClassId = student.Id;

                var studentClassData = new StudentClassDo()
                {
                    Id = ++studentClassId,
                    StudentId = studentId,

                };
            }
        }
    }
}




