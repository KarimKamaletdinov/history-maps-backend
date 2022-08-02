namespace HistoryMaps
{
    partial class CommonForm
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
            this._mainPanel = new System.Windows.Forms.Panel();
            this._bottomPanel = new System.Windows.Forms.Panel();
            this._message = new System.Windows.Forms.Label();
            this._bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(699, 320);
            this._mainPanel.TabIndex = 0;
            // 
            // _bottomPanel
            // 
            this._bottomPanel.BackColor = System.Drawing.SystemColors.Highlight;
            this._bottomPanel.Controls.Add(this._message);
            this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomPanel.Location = new System.Drawing.Point(0, 320);
            this._bottomPanel.Name = "_bottomPanel";
            this._bottomPanel.Size = new System.Drawing.Size(700, 18);
            this._bottomPanel.TabIndex = 1;
            // 
            // _message
            // 
            this._message.AutoSize = true;
            this._message.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this._message.Location = new System.Drawing.Point(0, 0);
            this._message.Name = "_message";
            this._message.Size = new System.Drawing.Size(0, 15);
            this._message.TabIndex = 0;
            // 
            // CommonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 338);
            this.Controls.Add(this._bottomPanel);
            this.Controls.Add(this._mainPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CommonForm";
            this.Text = "Админка Исторических карт";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._bottomPanel.ResumeLayout(false);
            this._bottomPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel _mainPanel;
        private Panel _bottomPanel;
        private Label _message;
    }
}