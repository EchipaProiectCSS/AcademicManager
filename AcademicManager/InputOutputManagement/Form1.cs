using System.Windows.Forms;
using ProcessManagement.Interfaces;

namespace InputOutputManagement
{
    public partial class Form1 : Form
    {
        private readonly IStudentRepository studentRepository;

        public Form1(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;

            InitializeComponent();
        }
    }
}
