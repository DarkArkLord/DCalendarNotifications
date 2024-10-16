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
            SuspendLayout();
            // 
            // titleLinkLabel
            // 
            titleLinkLabel.Dock = System.Windows.Forms.DockStyle.Top;
            titleLinkLabel.Location = new System.Drawing.Point(0, 0);
            titleLinkLabel.Name = "titleLinkLabel";
            titleLinkLabel.Size = new System.Drawing.Size(584, 15);
            titleLinkLabel.TabIndex = 0;
            titleLinkLabel.TabStop = true;
            titleLinkLabel.Text = "titleLinkLabel";
            titleLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EventForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 411);
            Controls.Add(titleLinkLabel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EventForm";
            Text = "EventForm";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.LinkLabel titleLinkLabel;
    }
}