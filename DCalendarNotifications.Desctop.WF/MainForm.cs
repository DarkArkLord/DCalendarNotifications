using System.Windows.Forms;

namespace DCalendarNotifications.Desctop.WF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void trayIconMenuItem_close_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
