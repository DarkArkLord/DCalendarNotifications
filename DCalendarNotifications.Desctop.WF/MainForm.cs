using DCalendarNotifications.Core;
using System;
using System.Windows.Forms;

namespace DCalendarNotifications.Desctop.WF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        #region From Closing

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

        #endregion

        private void lbEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbEvents.SelectedItem != null && lbEvents.SelectedItem is CalendarEvent calEvent)
            {
                ShowEventInfo(calEvent);
            }
        }

        private void ShowEventInfo(CalendarEvent calEvent)
        {
            var eventForm = new EventForm(calEvent)
            {
                TopMost = true,
                StartPosition = FormStartPosition.CenterScreen,
            };

            eventForm.Show();
        }

        private void AddLog(string message)
        {
            var logText = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]: {message}";
            lbLog.Items.Add(logText);
        }

        private void AddLog(Exception ex)
        {
            var message = $"{ex.Message} Stack Trace: {ex.StackTrace}";
            AddLog(message);
        }
    }
}
