using DCalendarNotifications.Core.CalendarData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCalendarNotifications.Core.ReminderData
{
    public class ReminderContainer
    {
        private IList<Reminder> _reminders = new List<Reminder>();

        public void Update(Day day, IEnumerable<int> notificationOffsets)
        {
            // Вернуть сортировку для notificationOffsets?
            var newRemainders = new List<Reminder>();
            foreach (var calEvent in day.Events)
            {
                int? previousTime = null;
                foreach (var time in notificationOffsets)
                {
                    var startPeriod = calEvent.Start.AddSeconds(time);
                    var endPeriod = previousTime == null
                        ? calEvent.Start.AddMinutes(1)
                        : calEvent.Start.AddSeconds(previousTime.Value).AddSeconds(-1);
                    previousTime = time;

                    newRemainders.Add(new Reminder()
                    {
                        Event = calEvent,
                        Start = startPeriod,
                        End = endPeriod,
                    });
                }
            }

            if (_reminders.Count == 0)
            {
                _reminders = newRemainders;
            }
            else
            {
                var deletedReminders = new List<Reminder>();

                // Update reminders
                foreach (var oldReminder in _reminders)
                {
                    var updatedReminder = newRemainders.FirstOrDefault(r => r.Id.Equals(oldReminder.Id));
                    if (updatedReminder != null)
                    {
                        oldReminder.Event = updatedReminder.Event;
                    }
                    else
                    {
                        deletedReminders.Add(oldReminder);
                    }
                }

                // Delete reminders
                foreach (var deletedReminder in deletedReminders)
                {
                    _reminders.Remove(deletedReminder);
                }

                // Add new reminders
                foreach (var newRemainder in newRemainders)
                {
                    var oldReminder = _reminders.FirstOrDefault(r => r.Id.Equals(newRemainder.Id));
                    if (oldReminder == null)
                    {
                        _reminders.Add(newRemainder);
                    }
                }
            }
        }

        public IEnumerable<Reminder> Call()
        {
            return Call(DateTime.Now);
        }

        public IEnumerable<Reminder> Call(DateTime dateTime)
        {
            var result = new List<Reminder>();

            foreach (var reminder in _reminders.Where(reminder => reminder.Start <= dateTime && dateTime <= reminder.End && !reminder.Called))
            {
                reminder.Called = true;
                result.Add(reminder);
            }

            return result;
        }
    }
}
