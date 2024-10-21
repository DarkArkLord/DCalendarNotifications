using DCalendarNotifications.Core;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DCalendarNotifications.Tests
{
    public class CalendarServiceTests
    {
        private DateTime testDate;
        private string icsFilePathV1;

        [SetUp]
        public void Setup()
        {
            testDate = new DateTime(2024, 10, 21);
            icsFilePathV1 = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "test-cal-v1.ics");
        }

        [Test]
        public async Task CalendarParsingTest()
        {
            var day = await CalendarService.LoadDayByICalFileAsync(testDate, icsFilePathV1);
                
            Assert.IsNotNull(day);
            Assert.IsNotNull(day.Events);

            Assert.That(day.Events.Count(), Is.EqualTo(1));

            var testEvent = day.Events.FirstOrDefault();
            Assert.IsNotNull(testEvent);

            Assert.That(testEvent.Title, Is.EqualTo("Test 1"));

            Assert.That(testEvent.Description, Is.EqualTo("Тестовое событие для теста"));

            var expectedStartDate = testDate.AddHours(15);
            Assert.That(testEvent.Start, Is.EqualTo(expectedStartDate));

            var expectedEndDate = testDate.AddHours(15).AddMinutes(30);
            Assert.That(testEvent.End, Is.EqualTo(expectedEndDate));

            Assert.That(testEvent.Location, Is.EqualTo("Цитадель Ледяной Короны"));

            Assert.That(testEvent.Organizer, Is.EqualTo("DarkArkLord (test@test.test)"));

            Assert.IsNotNull(testEvent.Attendees);
            var attendees = testEvent.Attendees.ToArray();
            Assert.That(attendees.Length, Is.EqualTo(2));
            Assert.That(attendees[0], Is.EqualTo("DarkArkLord (test@test.test)"));
            Assert.That(attendees[1], Is.EqualTo("cat (cat@cat.cat)"));
        }
    }
}