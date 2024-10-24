using DCalendarNotifications.Core;
using DCalendarNotifications.Core.CalendarData;
using DCalendarNotifications.Core.Config;
using DCalendarNotifications.Core.ReminderData;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Day = DCalendarNotifications.Core.CalendarData.Day;

namespace DCalendarNotifications.Desctop.WF
{
    public partial class MainForm : Form
    {
        private ConfigData config;

        private LogsContainer logs = new LogsContainer();

        private ReminderContainer _reminderContainer = new ReminderContainer();
        private object _lock = new();

        private bool minimizeInsteadClosing = true;

        public MainForm()
        {
            InitializeComponent();
            ConfigureReminder();
        }

        #region Minimizing

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (minimizeInsteadClosing)
            {
                e.Cancel = true;
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.Focus();
        }

        #endregion

        #region Events

        private async void calendarUpdateTimer_Tick(object sender, EventArgs e)
        {
            await UpdateReminderContainer();
        }

        private void reminderTimer_Tick(object sender, EventArgs e)
        {
            ReminderActivation();
        }

        private void lbEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbEvents.SelectedItem != null && lbEvents.SelectedItem is CalendarEvent calEvent)
            {
                ShowEventInfo(calEvent);
            }
        }

        #endregion

        #region Menu items

        private void menuItem_close_Click(object sender, EventArgs e)
        {
            minimizeInsteadClosing = false;
            Application.Exit();
        }

        private void menuItem_folder_Click(object sender, EventArgs e)
        {
            var path = $"/select, \"{Environment.CurrentDirectory}\\Config.xml\"";
            Process.Start("explorer.exe", path);
            AddLog("Директория открыта.");
        }

        private void menuItem_reload_Click(object sender, EventArgs e)
        {
            ConfigureReminder();
        }

        private async void menuItem_getData_Click(object sender, EventArgs e)
        {
            await UpdateReminderContainer();
        }

        private void menuItem_showNotifications_Click(object sender, EventArgs e)
        {
            ReminderActivation();
        }

        #endregion

        #region Events Methods

        private void ReadConfig()
        {
            var xml = ConfigParser.ReadXml("Config.xml");
            config = ConfigParser.Parse(xml);

            AddLog("Настройки загружены.");
        }

        private async Task UpdateReminderContainer()
        {
            try
            {
                var day = await CalendarService.LoadDayByICalUriAsync(DateTime.Today, config.Source);
                lock (_lock)
                {
                    try
                    {
                        _reminderContainer.Update(day, config.NotificationOffsets);
                    }
                    catch (Exception ex)
                    {
                        AddLog(ex);
                    }

                    UpdateEventList(day);
                    AddLog("События были успешно обновлены.");
                }
            }
            catch (Exception ex)
            {
                AddLog(ex);
            }
        }

        private void ReminderActivation()
        {
            try
            {
                var reminders = _reminderContainer.Call();
                foreach (var reminder in reminders)
                {
                    ShowEventInfo(reminder.Event);
                    AddLog($"Напоминание: {reminder.Event.Title}.");
                }

                var remindersCount = reminders.Count();
                if (remindersCount > 0)
                {
                    AddLog($"Отображено напоминаний: {reminders.Count()}.");
                }
            }
            catch (Exception ex)
            {
                AddLog(ex);
            }
        }

        #endregion

        #region Common Methods

        private async void ConfigureReminder()
        {
            try
            {
                calendarUpdateTimer.Stop();
                reminderTimer.Stop();

                ReadConfig();

                await UpdateReminderContainer();
                ReminderActivation();

                calendarUpdateTimer.Interval = (int)TimeSpan.FromSeconds(config.UpdateInterval).TotalMilliseconds;
                calendarUpdateTimer.Start();

                reminderTimer.Interval = (int)TimeSpan.FromSeconds(config.ReminderInterval).TotalMilliseconds;
                reminderTimer.Start();
            }
            catch (Exception ex)
            {
                AddLog(ex);
            }
        }

        private void UpdateEventList(Day day)
        {
            lbEvents.Items.Clear();
            foreach (var calEvent in day.Events.OrderBy(e => e.Start))
            {
                lbEvents.Items.Add(calEvent);
            }
        }

        private void ShowEventInfo(CalendarEvent calEvent)
        {
            var eventForm = new EventForm(calEvent);
            // Добавить звук?
            eventForm.Show();
        }

        private void AddLog(string message)
        {
            var newLogText = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: {message}";
            logs.AddLog(newLogText);
            tbLog.Text = logs.GetLogs();
        }

        private void AddLog(Exception ex)
        {
            var message = $"{ex.Message} Stack Trace: {ex.StackTrace}";
            AddLog(message);
        }

        #endregion
    }
}
