using System.Windows.Forms;
using ProcessManagement.DOs;
using ProcessManagement.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.IO;
using InputOutputManagement.Models;

namespace InputOutputManagement
{
    public partial class Form1 : Form
    {
        private readonly IStudentRepository studentRepository;
        private readonly IStudentClassRepository studentClassRepository;
        private readonly IStudentStatusRepository studentStatusRepository;

        public Form1(IDatabaseContext databaseContext)
        {
            InitializeComponent();

            studentRepository = databaseContext.Student;
            studentClassRepository = databaseContext.StudentClass;
            studentStatusRepository = databaseContext.StudentStatus;

            PopulateSpecificInterfaceComponent();
        }

        private void PopulateSpecificInterfaceComponent()
        {
            combStatus.Items.Add("Budget");
            combStatus.Items.Add("Fee");

            combPromoted.Items.Add("Yes");
            combPromoted.Items.Add("No");
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
            List<int> studentsToAdd = GetStudentsId();

            List<int> studentsClassToAdd = new List<int>();
            List<int> studentsStatusToAdd = new List<int>();
            List<StudentView> resultStudents = new List<StudentView>();

            if (combPromoted.Text != "" || txtClass.Text != "")
            {
                List<StudentClassDo> studentsClass = studentClassRepository.GetAll();
                foreach (var studClass in studentsClass)
                {
                    var toAdd = true;
                    if (combPromoted.Text != "" && !combPromoted.Text.Equals(studClass.Promoted))
                    {
                        toAdd = false;
                    }
                    if (txtClass.Text != "" && !txtClass.Text.Equals(studClass.Name))
                    {
                        toAdd = false;
                    }
                    if (toAdd)
                    {
                        studentsClassToAdd.Add(studClass.StudentId);
                    }

                }
            }

            if (combStatus.Text != "")
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

                    }
                    else
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
                        studView.Class = studentsClass.First().Name;
                    }

                    if (studentsStatuses != null)
                    {
                        studView.Student_Year = studentsStatuses.First().Year.ToString();
                        studView.ECTS = studentsStatuses.First().ECTS.ToString();
                    }
                    if (student != null)
                    {
                        studView.ID = student.Id.ToString();
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

        public List<int> GetStudentsId()
        {
            List<StudentDo> students = studentRepository.GetAll();

            List<int> studentsToAdd = new List<int>();

            foreach (var student in students)
            {
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
            return studentsToAdd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertStudentClass insertStudentClass = new InsertStudentClass();
            insertStudentClass.Show();

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

                MessageBox.Show("Done !");
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var name = saveFileDialog1.FileName;
            if (dataGridView2.RowCount <= 0) return;
            var dr = new DataGridViewRow();
            var swOut = new StreamWriter(name);
            for (var i = 0; i <= dataGridView2.Columns.Count - 1; i++)
            {
                if (i > 0)
                {
                    swOut.Write(",");
                }
                swOut.Write(dataGridView2.Columns[i].HeaderText);
            }
            swOut.WriteLine();
            for (var j = 0; j <= dataGridView2.Rows.Count - 2; j++)
            {
                if (j > 0)
                {
                    swOut.WriteLine();
                }

                dr = dataGridView2.Rows[j];
                for (int i = 0; i < dataGridView2.Columns.Count - 2; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(",");
                    }
                    if (dr.Cells[i].Value != null)
                    {
                        var value = dr.Cells[i].Value.ToString();
                        value = value.Replace(',', ' ');
                        value = value.Replace(Environment.NewLine, " ");
                        swOut.Write(value);
                    }


                }
            }
            swOut.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void btnInsertStatus_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            if (result != DialogResult.OK) return;
            var itemList = File.ReadLines(file).ToList();
            foreach (var line in itemList)
            {
                var splitter = line.Split('\t');
                var listStatusStudents = studentStatusRepository.GetAll();
                var studentStatus = listStatusStudents.Last();
                var studentStatusId = studentStatus.Id;
                StudentStatusDo studentStatusData = new StudentStatusDo
                {
                    Id = ++studentStatusId,
                    Credits = Int32.Parse(splitter[0]),
                    StudentId = Int32.Parse(splitter[1]),
                    ECTS = Int32.Parse(splitter[2]),
                    Year = Int32.Parse(splitter[3])

                };
                studentStatusRepository.Insert(studentStatusData);
                MessageBox.Show("Done !");

            }

        }
    }
}



