﻿namespace DCalendarNotifications.Desctop.WF
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            trayIcon = new System.Windows.Forms.NotifyIcon(components);
            trayIconMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gbEvents = new System.Windows.Forms.GroupBox();
            lbEvents = new System.Windows.Forms.ListBox();
            gbLog = new System.Windows.Forms.GroupBox();
            tbLog = new System.Windows.Forms.TextBox();
            calendarUpdateTimer = new System.Windows.Forms.Timer(components);
            reminderTimer = new System.Windows.Forms.Timer(components);
            trayIconMenuStrip.SuspendLayout();
            gbEvents.SuspendLayout();
            gbLog.SuspendLayout();
            SuspendLayout();
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = trayIconMenuStrip;
            trayIcon.Icon = (System.Drawing.Icon)resources.GetObject("trayIcon.Icon");
            trayIcon.Text = "trayIcon";
            trayIcon.Visible = true;
            trayIcon.MouseClick += trayIcon_MouseClick;
            // 
            // trayIconMenuStrip
            // 
            trayIconMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { закрытьToolStripMenuItem });
            trayIconMenuStrip.Name = "trayIconMenuStrip";
            trayIconMenuStrip.Size = new System.Drawing.Size(121, 26);
            // 
            // закрытьToolStripMenuItem
            // 
            закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            закрытьToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            закрытьToolStripMenuItem.Text = "Закрыть";
            закрытьToolStripMenuItem.Click += trayIconMenuItem_close_Click;
            // 
            // gbEvents
            // 
            gbEvents.Controls.Add(lbEvents);
            gbEvents.Dock = System.Windows.Forms.DockStyle.Top;
            gbEvents.Location = new System.Drawing.Point(0, 0);
            gbEvents.Name = "gbEvents";
            gbEvents.Size = new System.Drawing.Size(480, 207);
            gbEvents.TabIndex = 2;
            gbEvents.TabStop = false;
            gbEvents.Text = "События";
            // 
            // lbEvents
            // 
            lbEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            lbEvents.FormattingEnabled = true;
            lbEvents.ItemHeight = 15;
            lbEvents.Location = new System.Drawing.Point(3, 19);
            lbEvents.Name = "lbEvents";
            lbEvents.Size = new System.Drawing.Size(474, 185);
            lbEvents.TabIndex = 0;
            lbEvents.MouseDoubleClick += lbEvents_MouseDoubleClick;
            // 
            // gbLog
            // 
            gbLog.Controls.Add(tbLog);
            gbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            gbLog.Location = new System.Drawing.Point(0, 207);
            gbLog.Name = "gbLog";
            gbLog.Size = new System.Drawing.Size(480, 150);
            gbLog.TabIndex = 3;
            gbLog.TabStop = false;
            gbLog.Text = "Журнал";
            // 
            // tbLog
            // 
            tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            tbLog.Location = new System.Drawing.Point(3, 19);
            tbLog.Multiline = true;
            tbLog.Name = "tbLog";
            tbLog.ReadOnly = true;
            tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            tbLog.Size = new System.Drawing.Size(474, 128);
            tbLog.TabIndex = 0;
            // 
            // calendarUpdateTimer
            // 
            calendarUpdateTimer.Tick += calendarUpdateTimer_Tick;
            // 
            // reminderTimer
            // 
            reminderTimer.Tick += reminderTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(480, 357);
            Controls.Add(gbLog);
            Controls.Add(gbEvents);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "MainForm";
            TopMost = true;
            FormClosing += MainForm_FormClosing;
            trayIconMenuStrip.ResumeLayout(false);
            gbEvents.ResumeLayout(false);
            gbLog.ResumeLayout(false);
            gbLog.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbEvents;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.ListBox lbEvents;
        private System.Windows.Forms.Timer calendarUpdateTimer;
        private System.Windows.Forms.Timer reminderTimer;
        private System.Windows.Forms.TextBox tbLog;
    }
}
