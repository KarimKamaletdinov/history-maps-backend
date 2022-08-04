namespace HistoryMaps
{
    partial class MainForm
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
            this._eventsListControl = new HistoryMaps.EventsListControl();
            this.SuspendLayout();
            // 
            // _eventsListControl
            // 
            this._eventsListControl.BackColor = System.Drawing.Color.White;
            this._eventsListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._eventsListControl.Location = new System.Drawing.Point(0, 0);
            this._eventsListControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._eventsListControl.Name = "_eventsListControl";
            this._eventsListControl.Size = new System.Drawing.Size(800, 450);
            this._eventsListControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._eventsListControl);
            this.Name = "MainForm";
            this.Text = "Админка исторических карт";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private EventsListControl _eventsListControl;
    }
}