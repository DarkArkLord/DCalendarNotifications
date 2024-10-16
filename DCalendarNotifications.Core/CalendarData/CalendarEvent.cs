﻿using System;
using System.Collections.Generic;

namespace DCalendarNotifications.Core.CalendarData
{
    public class CalendarEvent
    {
        public string Id { get; init; }

        public string Title { get; init; }

        public DateTime Start { get; init; }

        public DateTime End { get; init; }

        public string Description { get; init; }

        public Uri Organizer { get; init; }

        public IEnumerable<Uri> Attendees { get; init; }

        public string Location { get; init; }

        public Uri Url { get; init; }

        public override string ToString() => $"{Start:HH:mm}-{End:HH:mm} - {Title}";
    }
}