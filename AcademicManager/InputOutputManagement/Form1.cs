using System.Windows.Forms;
using ProcessManagement.Interfaces;

namespace InputOutputManagement
{
    public partial class Form1 : Form
    {
        public Form1(IStudent student)
        {
            InitializeComponent();
        }
    }
}
