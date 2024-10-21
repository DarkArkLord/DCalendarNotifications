using DCalendarNotifications.Core.CalendarData;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace DCalendarNotifications.Desctop.WF
{
    public partial class EventForm : Form
    {
        public readonly CalendarEvent calEvent;

        public EventForm(CalendarEvent calEvent)
        {
            InitializeComponent();

            this.calEvent = calEvent;
            UIUpdate();
        }

        private void UIUpdate()
        {
            titleLinkLabel.Text = $"{calEvent.Title} ({calEvent.Start:HH:mm}-{calEvent.End:HH:mm})";
            tbOrganizer.Text = calEvent.Organizer;
            tbAttendees.Text = string.Join("; ", calEvent.Attendees);
            tbLocation.Text = calEvent.Location;
            rtbDescription.Text = calEvent.Description;
        }

        private void titleLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(calEvent.Url.AbsoluteUri)
            {
                UseShellExecute = true,
            });
        }
    }
}
