using System.Collections.Generic;

namespace DCalendarNotifications.Core
{
    public class Day
    {
        private readonly IList<CalendarEvent> _event = new List<CalendarEvent>();

        public IEnumerable<CalendarEvent> Events => _event;

        public void AddEvent(CalendarEvent calEvent)
        {
            _event.Add(calEvent);
        }
    }
}
