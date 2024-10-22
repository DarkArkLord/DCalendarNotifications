using DCalendarNotifications.Core;
using DCalendarNotifications.Core.CalendarData;
using DCalendarNotifications.Core.ReminderData;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DCalendarNotifications.Tests
{
    public class ReminderContainerTests
    {
        private DateTime testStartDate;
        private string icsFilePathV1;
        private Day day;
        private ReminderContainer reminderContainer;

        [SetUp]
        public async Task Setup()
        {
            testStartDate = new DateTime(2024, 10, 21);
            icsFilePathV1 = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "test-cal-v1.ics");
            day = await CalendarService.LoadDayByICalFileAsync(testStartDate, icsFilePathV1);

            reminderContainer = new ReminderContainer();
            reminderContainer.Update(day, new int[] { 0, -900 });
        }

        [Test]
        public void BeforeTimeTest()
        {
            var testDate = testStartDate.AddHours(1);
            var reminders = reminderContainer.Call(testDate);

            Assert.IsNotNull(reminders);
            Assert.That(reminders.Count(), Is.EqualTo(0));
        }

        [Test]
        public void FirstTimeTest()
        {
            var testDate = testStartDate.AddHours(15).AddMinutes(-10);
            var reminders = reminderContainer.Call(testDate);

            Assert.IsNotNull(reminders);
            Assert.That(reminders.Count(), Is.EqualTo(1));

            // Уже отображенные напоминания не отображаются
            reminders = reminderContainer.Call(testDate);

            Assert.IsNotNull(reminders);
            Assert.That(reminders.Count(), Is.EqualTo(0));
        }

        [Test]
        public void SecondTimeTest()
        {
            var testDate = testStartDate.AddHours(15).AddMinutes(1);
            var reminders = reminderContainer.Call(testDate);

            Assert.IsNotNull(reminders);
            Assert.That(reminders.Count(), Is.EqualTo(1));

            // Уже отображенные напоминания не отображаются
            reminders = reminderContainer.Call(testDate);

            Assert.IsNotNull(reminders);
            Assert.That(reminders.Count(), Is.EqualTo(0));
        }

        [Test]
        public void AfterTimeTest()
        {
            var testDate = testStartDate.AddHours(16);
            var reminders = reminderContainer.Call(testDate);

            Assert.IsNotNull(reminders);
            Assert.That(reminders.Count(), Is.EqualTo(0));
        }

        [Test]
        public void AllTimeTest()
        {
            BeforeTimeTest();
            FirstTimeTest();
            SecondTimeTest();
            AfterTimeTest();
        }
    }
}