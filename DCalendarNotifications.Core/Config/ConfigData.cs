using System;
using System.Collections.Generic;

namespace DCalendarNotifications.Core.Config
{
    public class ConfigData
    {
        /// <summary>
        /// Ссылка на каледарь-источник
        /// </summary>
        public Uri Source { get; set; }

        /// <summary>
        /// Интервал обновления данных в ссекундах
        /// </summary>
        public int UpdateInterval { get; set; }

        /// <summary>
        /// Интервал проверки отображения уведомлений в секундах
        /// </summary>
        public int ReminderInterval { get; set; }

        /// <summary>
        /// Список сдвигов по времени дял уведомлений
        /// </summary>
        public IList<int> NotificationOffsets { get; } = new List<int>();
    }
}
