using System.Windows.Forms;

namespace GUI
{
    public partial class frmTaskActions : Form
    {
        private int maTask;

        public frmTaskActions(int maTask)
        {
            InitializeComponent();
            this.maTask = maTask;
        }
    }
}
