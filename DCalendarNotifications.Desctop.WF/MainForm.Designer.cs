namespace DCalendarNotifications.Desctop.WF
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
            trayIconMenuStrip.SuspendLayout();
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
            trayIconMenuStrip.Size = new System.Drawing.Size(181, 48);
            // 
            // закрытьToolStripMenuItem
            // 
            закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            закрытьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            закрытьToolStripMenuItem.Text = "Закрыть";
            закрытьToolStripMenuItem.Click += trayIconMenuItem_close_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            trayIconMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
    }
}
