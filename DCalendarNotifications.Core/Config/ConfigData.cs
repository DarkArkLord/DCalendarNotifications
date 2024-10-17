using System;
using System.Collections.Generic;

namespace DCalendarNotifications.Core.Config
{
    public class ConfigData
    {
        public Uri Source { get; set; }
        public int UpdateInterval { get; set; }
        public int ReminderInterval { get; set; }
        public IList<int> NotificationOffsets { get; } = new List<int>();
    }
}
