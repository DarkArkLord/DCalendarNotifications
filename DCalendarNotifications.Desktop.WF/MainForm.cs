﻿using DCalendarNotifications.Core;
using DCalendarNotifications.Core.CalendarData;
using DCalendarNotifications.Core.Config;
using DCalendarNotifications.Core.ReminderData;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Day = DCalendarNotifications.Core.CalendarData.Day;

namespace DCalendarNotifications.Desctop.WF
{
    public partial class MainForm : Form
    {
        private ConfigData config;
        private ReminderContainer _reminderContainer = new ReminderContainer();
        private object _lock = new();

        public MainForm()
        {
            InitializeComponent();
            ConfigureReminder();
        }

        #region For Closing

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.Focus();
        }

        private void trayIconMenuItem_close_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
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
            // Вынести установку флага вызванности
            eventForm.Show();
        }

        private void AddLog(string message)
        {
            var newLogText = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: {message}";
            var logText = tbLog.Text.Length < 1
                ? newLogText
                : $"{newLogText}{Environment.NewLine}{tbLog.Text}";
            tbLog.Text = logText;
        }

        private void AddLog(Exception ex)
        {
            var message = $"{ex.Message} Stack Trace: {ex.StackTrace}";
            AddLog(message);
        }

        #endregion
    }
}
