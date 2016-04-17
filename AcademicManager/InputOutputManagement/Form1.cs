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
    public partial class Form1 : Form
    {
        private readonly IStudentRepository studentRepository;
        private readonly IStudentClassRepository studentClassRepository;
        private readonly IStudentStatusRepository studentStatusRepository;
        private IDatabaseModel databaseModel;

        public Form1()
        {
            databaseModel = new DatabaseModel();
            this.studentRepository = new StudentRepository(databaseModel);
            this.studentClassRepository = new StudentClassRepository(databaseModel);
            this.studentStatusRepository = new StudentStatusRepository(databaseModel);

            InitializeComponent();
            combStatus.Items.Add("Budget");
            combStatus.Items.Add("Fee");

            combPromoted.Items.Add("Yes");
            combPromoted.Items.Add("No");


        }

        public Form1(IStudentRepository studentRepository, IStudentClassRepository studentClassRepository, IStudentStatusRepository studentStatusRepository)
        {
            databaseModel = new DatabaseModel();
            this.studentRepository = new StudentRepository(databaseModel);
            this.studentClassRepository = studentClassRepository;
            this.studentStatusRepository = studentStatusRepository;

            InitializeComponent();
      

        }

        private void Form1_Load(object sender, System.EventArgs e)
        {


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 4)
                {


                }

            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertStudent insertStudent = new InsertStudent();
            insertStudent.Show();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<StudentDo> students = studentRepository.GetAll();
            List<int> studentsToAdd = new List<int>();
            List<int> studentsClassToAdd = new List<int>();
            List<int> studentsStatusToAdd = new List<int>();
            List<StudentView> resultStudents = new List<StudentView>();

            foreach (var student in students){
                var toAdd = true;
                if (txtFirstname.Text != "" && !txtFirstname.Text.Equals(student.FirstName))
                    {
                        toAdd = false;
                    }
                    if (txtLastname.Text != "" && !txtLastname.Text.Equals(student.LastName))
                    {
                        toAdd = false;
                    }
                    if (toAdd)
                    {
                        studentsToAdd.Add(student.Id);
                    }

                }
            if (combPromoted.Text != "")
            {
                List<StudentClassDo> studentsClass = studentClassRepository.GetAll();
                foreach(var studClass in studentsClass)
                {
                    if (combPromoted.Text.Equals(studClass.Promoted))
                    {
                        studentsClassToAdd.Add(studClass.StudentId);
                    }
                }
            }

            if(combStatus.Text != "")
            {
                List<StudentStatusDo> studentsStatus = studentStatusRepository.GetAll();
                foreach (var studStatus in studentsStatus)
                {
                    if (combStatus.Text.Equals("Budget"))
                    {
                        if (studStatus.Credits > 100)
                        {
                            studentsStatusToAdd.Add(studStatus.StudentId);
                        }

                    }else
                    {
                        if (studStatus.Credits < 100)
                        {
                            studentsStatusToAdd.Add(studStatus.StudentId);
                        }
                    }
                }

            }

            IEnumerable<int> firstresult = null;
            IEnumerable<int> lastresult = null;
            if (studentsClassToAdd.Count > 0)
           {
                if (studentsToAdd.Count > 0)
                {
                   firstresult = studentsToAdd.Intersect(studentsClassToAdd);
                }
                else
                {
                    firstresult = studentsClassToAdd;
                }
            }
            else
            {
                if (studentsToAdd.Count > 0)
                 {
                    firstresult = studentsToAdd;
                 }

            }

            if (firstresult != null && firstresult.ToList().Count > 0)
            {

                if (studentsStatusToAdd.Count > 0)
                {
                    lastresult = studentsStatusToAdd.Intersect(firstresult);
                }
                else
                {
                    lastresult = firstresult;
                }

            }
            else
            {
                if (studentsStatusToAdd.Count > 0)
                {
                    lastresult = studentsStatusToAdd;
                }
            }

           if (lastresult != null && lastresult.ToList().Count > 0)
           {
                List<int> result = lastresult.ToList();
                foreach (var studId in result)
                {
                    List<StudentStatusDo> studentsStatuses = studentStatusRepository.GetStudentStatuses(studId);
                    List<StudentClassDo> studentsClass = studentClassRepository.GetStudentClasses(studId);
                    StudentDo student = studentRepository.Get(studId);

                    StudentView studView = new StudentView();
                   
                    if (studentsClass != null)
                    {
                        if (studentsClass.First().Promoted != null)
                        {
                            studView.Is_Promoted = studentsClass.First().Promoted;
                        }
                    }
                   
                    if (studentsStatuses != null)
                    {
                        studView.Student_Year = studentsStatuses.First().Year.ToString();
                        studView.ECTS = studentsStatuses.First().ECTS.ToString();
                    }
                    if (student != null)
                    {
                        studView.First_Name = student.FirstName;
                        studView.Last_Name = student.LastName;
                        studView.Email_Address = student.EmailAddress;
                        studView.Age = student.Age;
                        studView.Gender = student.Gender;
                    }
                   
                   
                    resultStudents.Add(studView);
                }
           }

               
            var bindingList = new BindingList<StudentView>(resultStudents);
            var source = new BindingSource(bindingList, null);
            dataGridView2.DataSource = source;


        }
    }
    }


