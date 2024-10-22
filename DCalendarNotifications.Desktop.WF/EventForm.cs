using DCalendarNotifications.Core.CalendarData;
using System.Diagnostics;
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
            rtbLocation.Text = calEvent.Location;
            rtbDescription.Text = calEvent.Description;
        }

        private void titleLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var psi = new ProcessStartInfo(calEvent.Url.AbsoluteUri) { UseShellExecute = true, };
            Process.Start(psi);
        }

        private void OnLinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.LinkText))
            {
                var psi = new ProcessStartInfo(e.LinkText) { UseShellExecute = true, };
                Process.Start(psi);
            }
        }
    }
}
