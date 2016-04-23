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

            var context = kernel.GetService(typeof(IDatabaseContext));

            Application.Run(new Form1((IDatabaseContext)context));
        }
    }
}
