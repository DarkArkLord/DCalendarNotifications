using DCalendarNotifications.Core.CalendarData;
using System;

namespace DCalendarNotifications.Core.ReminderData
{
    public class Reminder
    {
        public CalendarEvent Event { get; set; }

        public DateTime Start { get; init; }

        public DateTime End { get; init; }

        public bool Called { get; set; } = false;

        public string Id => $"{Event?.Id}_{Start:yyyyMMddHHmmss}_{End:yyyyMMddHHmmss}";
    }
}
