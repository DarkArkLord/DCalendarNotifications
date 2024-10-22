using DCalendarNotifications.Core.CalendarData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCalendarNotifications.Core.ReminderData
{
    public class ReminderContainer
    {
        private IList<Reminder> _reminders = new List<Reminder>();

        /// <summary>
        /// Обновление списка уведомлений
        /// </summary>
        /// <param name="day">Параметры дня, считанные из календаря</param>
        /// <param name="notificationOffsets">Набор временных сдвигов для уведомлений</param>
        public void Update(Day day, IEnumerable<int> notificationOffsets)
        {
            // Вернуть сортировку для notificationOffsets?
            var newRemainders = new List<Reminder>();
            // Для каждого события добавляем уведомление для каждого временного сдвига
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

                // Обновление уведомлений
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

                // Удаление уведомлений
                foreach (var deletedReminder in deletedReminders)
                {
                    _reminders.Remove(deletedReminder);
                }

                // Добавление новых уведомлений
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

        /// <summary>
        /// Вызов уведомлений для текущего времени
        /// </summary>
        /// <returns>Список уведомлений</returns>
        public IEnumerable<Reminder> Call()
        {
            return Call(DateTime.Now);
        }

        /// <summary>
        /// Вызов уведомлений для определенного времени
        /// </summary>
        /// <param name="dateTime">Время уведомлений</param>
        /// <returns>Список уведомлений</returns>
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
