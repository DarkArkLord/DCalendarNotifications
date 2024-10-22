using DCalendarNotifications.Core.CalendarData;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DCalendarNotifications.Core
{
    public static class CalendarService
    {
        /// <summary>
        /// Загрузка данных из календаря по ссылке
        /// </summary>
        /// <param name="date">Дата, к которой относятся данные</param>
        /// <param name="uri">Ссылка на каледнарь-источник</param>
        /// <returns>Данные о событиях</returns>
        public static async Task<Day> LoadDayByICalUriAsync(DateTime date, Uri uri)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(uri);

            var responseString = await response.Content.ReadAsStringAsync();

            return ParseICalToDay(date, responseString);
        }

        /// <summary>
        /// Загрузка данных из календаря из файла
        /// </summary>
        /// <param name="date">Дата, к которой относятся данные</param>
        /// <param name="icsFilePath">Путь для файла с данными</param>
        /// <returns>Данные о событиях</returns>
        public static async Task<Day> LoadDayByICalFileAsync(DateTime date, string icsFilePath)
        {
            var iCalString = await File.ReadAllTextAsync(icsFilePath);
            return ParseICalToDay(date, iCalString);
        }

        // Парсинг ICal данных
        private static Day ParseICalToDay(DateTime date, string iCalString)
        {
            var calendar = Ical.Net.Calendar.Load(iCalString);
            var currentOccurrences = calendar.GetOccurrences(date);

            var day = new Day();

            if (currentOccurrences == null)
            {
                return day;
            }

            foreach (var occurrence in currentOccurrences)
            {
                if (occurrence.Source is Ical.Net.CalendarComponents.CalendarEvent calEvent)
                {
                    day.AddEvent(new CalendarEvent()
                    {
                        Id = calEvent.Uid,
                        Title = calEvent.Summary,
                        Start = occurrence.Period.StartTime.AsUtc.ToLocalTime(),
                        End = occurrence.Period.EndTime.AsUtc.ToLocalTime(),
                        Description = calEvent.Description,
                        Organizer = calEvent.Organizer is not null
                            ? $"{calEvent.Organizer.CommonName} ({GetMail(calEvent.Organizer.Value)})"
                            : null,
                        Attendees = calEvent.Attendees.Select(a => $"{a.CommonName} ({GetMail(a.Value)})"),
                        Location = calEvent.Location,
                        Url = calEvent.Url
                    });
                }
            }

            return day;
        }

        // Очистка почтовых ссылок
        private static string GetMail(Uri uri)
        {
            return uri.AbsoluteUri.Replace("mailto:", "");
        }
    }
}
