using System;
using System.Windows.Forms;
using Dependencies;
using Ninject;
using ProcessManagement.Interfaces;

namespace InputOutputManagement
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IKernel kernel = new StandardKernel(new NinjectCommon());

            var student = kernel.GetService(typeof(IStudentRepository));

            Application.Run(new Form1((IStudentRepository)student));
        }
    }
}
