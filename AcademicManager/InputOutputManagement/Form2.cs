using System.Windows.Forms;
using System.Data.SqlClient;
using ProcessManagement.DOs;
using ProcessManagement.Helper;
using ProcessManagement.Implementations;
using ProcessManagement.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;

namespace InputOutputManagement
{
    public partial class InsertStudent : Form
    {

        private readonly IStudentRepository studentRepository;
        private readonly IStudentClassRepository studentClassRepository;
        private readonly IStudentStatusRepository studentStatusRepository;
        private IDatabaseModel databaseModel;

        public InsertStudent()
        {
            databaseModel = new DatabaseModel();
            this.studentRepository = new StudentRepository(databaseModel);
            this.studentClassRepository = new StudentClassRepository(databaseModel);
            this.studentStatusRepository = new StudentStatusRepository(databaseModel);
            InitializeComponent();
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

        private void Insert_Click(object sender, EventArgs e)
        {

            List<StudentDo> listStudents = studentRepository.GetAll();
            StudentDo student = listStudents.Last();
            int studentId = student.Id;
            //TODO: not complete
            StudentDo studentData = new StudentDo();
            studentData.Id = ++studentId;
            studentData.Age = textYear.Text;
            studentData.FirstName = textFirstName.Text;
            studentData.LastName = textLastName.Text;
            studentData.Gender = textGender.Text;
            studentData.EmailAddress = textEmail.Text;


            studentRepository.Insert(studentData);

            List<StudentClassDo> listClassStudents = studentClassRepository.GetAll();
            StudentClassDo studentClass = listClassStudents.Last();
            int studentClassId = student.Id;

            StudentClassDo studentClassData = new StudentClassDo()
            {
                Id = ++studentClassId,
                StudentId = studentId,

            };

           // studentClassRepository.Insert(studentClassData);
        }
    }
}



