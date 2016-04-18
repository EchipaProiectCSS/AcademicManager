using System.Configuration;
using Database.Implementations;
using Database.Interfaces;
using ProcessManagement.Interfaces;
using ProcessManagement.DOs;
using System.IO;
using System;

namespace ProcessManagement.Implementations
{
    public class DatabaseModel : IDatabaseModel
    {
        private readonly FileSystemDatabaseManager databaseManager;
        private FileSystemDatabase Context;

        private IDatabaseModel databaseModel;
        private StudentRepository student;

        public DatabaseModel()
        {
            databaseManager = new FileSystemDatabaseManager();

            SetInstance();
        }

        private const string DatabaseName = "SampleDatabase";

        public void SetInstance()
        {
            if (Context != null)
            {
                return;
            }         
            string DatabaseFilePath = ConfigurationSettings.AppSettings["connectionString"];
            if (!Directory.Exists(DatabaseFilePath))
            {
                databaseManager.Create(DatabaseFilePath, DatabaseName);
               
            }
            Context = this.databaseManager.Open(ConfigurationSettings.AppSettings["connectionString"]);

            if (Context != null)
            {

                student = new StudentRepository(this);
                var studentClass = new StudentClassRepository(this);
                var studentStatus = new StudentStatusRepository(this);

                StudentDo studentData = new StudentDo()
                {
                    Id = 3,
                    Age = "25",
                    FirstName = "Cornel"
                };

               // student.Insert(studentData);


                StudentStatusDo studentStat = new StudentStatusDo()
                {
                    Id = 2,
                    Credits = 33,
                    StudentId = 1,
                    ECTS = 8,
                    Year = 2016
                };

               // studentStatus.Insert(studentStat);


                //TODO: not complete
                var studentClss = new StudentClassDo()
                {
                    Id = 2,
                    Name = "Math",
                    StudentId = 1,
                    Promoted = "No"
                };
                ;

               // studentClass.Insert(studentClss);


            }       
            
        }

        public IDatabase GetInstance()
        {
            return Context;
        }
    }
}
