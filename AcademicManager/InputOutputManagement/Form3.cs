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
using System.IO;

namespace InputOutputManagement
{
    public partial class InsertStudentClass : Form
    {

        private readonly IStudentClassRepository studentClassRepository;
        private IDatabaseModel databaseModel;

        public InsertStudentClass()
        {
            databaseModel = new DatabaseModel();
            
            this.studentClassRepository = new StudentClassRepository(databaseModel);
            InitializeComponent();
        }

        private void insertClass_Click(object sender, EventArgs e)
        {

            List<StudentClassDo> listClassStudents = studentClassRepository.GetAll();
            StudentClassDo studentClass = listClassStudents.Last();
            int studentClassId = studentClass.Id;

            StudentClassDo studentClassData = new StudentClassDo();
            studentClassData.Id = ++studentClassId;
            studentClassData.Name = txtClassName.Text;
            studentClassData.Promoted = combPromoted.Text;
            studentClassData.StudentId = Int32.Parse(textStudentId.Text);
            studentClassRepository.Insert(studentClassData);
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
                var listClassStudents = studentClassRepository.GetAll();
                var student = listClassStudents.Last();
                var studentClassId = student.Id;
                StudentClassDo studentData = new StudentClassDo
                {
                    Id = ++studentClassId,
                    Name = splitter[0],
                    StudentId = Int32.Parse(splitter[1]),
                    Promoted = splitter[2]
                    
                };
                studentClassRepository.Insert(studentData);
            }



        }
    }
}
