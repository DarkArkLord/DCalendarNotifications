namespace DCalendarNotifications.Desctop.WF
{
    partial class EventForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventForm));
            titleLinkLabel = new System.Windows.Forms.LinkLabel();
            gbOrganizer = new System.Windows.Forms.GroupBox();
            tbOrganizer = new System.Windows.Forms.TextBox();
            gbAttendees = new System.Windows.Forms.GroupBox();
            tbAttendees = new System.Windows.Forms.TextBox();
            gbLocation = new System.Windows.Forms.GroupBox();
            rtbDescription = new System.Windows.Forms.RichTextBox();
            rtbLocation = new System.Windows.Forms.RichTextBox();
            gbOrganizer.SuspendLayout();
            gbAttendees.SuspendLayout();
            gbLocation.SuspendLayout();
            SuspendLayout();
            // 
            // titleLinkLabel
            // 
            titleLinkLabel.Dock = System.Windows.Forms.DockStyle.Top;
            titleLinkLabel.Location = new System.Drawing.Point(0, 0);
            titleLinkLabel.Name = "titleLinkLabel";
            titleLinkLabel.Size = new System.Drawing.Size(584, 20);
            titleLinkLabel.TabIndex = 0;
            titleLinkLabel.TabStop = true;
            titleLinkLabel.Text = "titleLinkLabel";
            titleLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            titleLinkLabel.LinkClicked += titleLinkLabel_LinkClicked;
            // 
            // gbOrganizer
            // 
            gbOrganizer.Controls.Add(tbOrganizer);
            gbOrganizer.Dock = System.Windows.Forms.DockStyle.Top;
            gbOrganizer.Location = new System.Drawing.Point(0, 20);
            gbOrganizer.Name = "gbOrganizer";
            gbOrganizer.Size = new System.Drawing.Size(584, 50);
            gbOrganizer.TabIndex = 1;
            gbOrganizer.TabStop = false;
            gbOrganizer.Text = "Организатор";
            // 
            // tbOrganizer
            // 
            tbOrganizer.Dock = System.Windows.Forms.DockStyle.Fill;
            tbOrganizer.Location = new System.Drawing.Point(3, 19);
            tbOrganizer.Name = "tbOrganizer";
            tbOrganizer.ReadOnly = true;
            tbOrganizer.Size = new System.Drawing.Size(578, 23);
            tbOrganizer.TabIndex = 0;
            // 
            // gbAttendees
            // 
            gbAttendees.Controls.Add(tbAttendees);
            gbAttendees.Dock = System.Windows.Forms.DockStyle.Top;
            gbAttendees.Location = new System.Drawing.Point(0, 70);
            gbAttendees.Name = "gbAttendees";
            gbAttendees.Size = new System.Drawing.Size(584, 100);
            gbAttendees.TabIndex = 2;
            gbAttendees.TabStop = false;
            gbAttendees.Text = "Участники";
            // 
            // tbAttendees
            // 
            tbAttendees.Dock = System.Windows.Forms.DockStyle.Fill;
            tbAttendees.Location = new System.Drawing.Point(3, 19);
            tbAttendees.Multiline = true;
            tbAttendees.Name = "tbAttendees";
            tbAttendees.ReadOnly = true;
            tbAttendees.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            tbAttendees.Size = new System.Drawing.Size(578, 78);
            tbAttendees.TabIndex = 0;
            // 
            // gbLocation
            // 
            gbLocation.Controls.Add(rtbLocation);
            gbLocation.Dock = System.Windows.Forms.DockStyle.Top;
            gbLocation.Location = new System.Drawing.Point(0, 170);
            gbLocation.Name = "gbLocation";
            gbLocation.Size = new System.Drawing.Size(584, 50);
            gbLocation.TabIndex = 3;
            gbLocation.TabStop = false;
            gbLocation.Text = "Расположение";
            // 
            // rtbDescription
            // 
            rtbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbDescription.Location = new System.Drawing.Point(0, 220);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.ReadOnly = true;
            rtbDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            rtbDescription.Size = new System.Drawing.Size(584, 191);
            rtbDescription.TabIndex = 4;
            rtbDescription.Text = "";
            rtbDescription.LinkClicked += OnLinkClicked;
            // 
            // rtbLocation
            // 
            rtbLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbLocation.Location = new System.Drawing.Point(3, 19);
            rtbLocation.Name = "rtbLocation";
            rtbLocation.ReadOnly = true;
            rtbLocation.Size = new System.Drawing.Size(578, 28);
            rtbLocation.TabIndex = 0;
            rtbLocation.Text = "";
            rtbLocation.LinkClicked += OnLinkClicked;
            // 
            // EventForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 411);
            Controls.Add(rtbDescription);
            Controls.Add(gbLocation);
            Controls.Add(gbAttendees);
            Controls.Add(gbOrganizer);
            Controls.Add(titleLinkLabel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EventForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "EventForm";
            TopMost = true;
            gbOrganizer.ResumeLayout(false);
            gbOrganizer.PerformLayout();
            gbAttendees.ResumeLayout(false);
            gbAttendees.PerformLayout();
            gbLocation.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.LinkLabel titleLinkLabel;
        private System.Windows.Forms.GroupBox gbOrganizer;
        private System.Windows.Forms.TextBox tbOrganizer;
        private System.Windows.Forms.GroupBox gbAttendees;
        private System.Windows.Forms.TextBox tbAttendees;
        private System.Windows.Forms.GroupBox gbLocation;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.RichTextBox rtbLocation;
    }
}