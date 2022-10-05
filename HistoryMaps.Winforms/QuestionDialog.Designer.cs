namespace HistoryMaps
{
    partial class QuestionDialog
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
            this._text = new System.Windows.Forms.Label();
            this._ok = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _text
            // 
            this._text.Dock = System.Windows.Forms.DockStyle.Top;
            this._text.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this._text.Location = new System.Drawing.Point(0, 0);
            this._text.Name = "_text";
            this._text.Size = new System.Drawing.Size(296, 32);
            this._text.TabIndex = 1;
            this._text.Text = "Message";
            this._text.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _ok
            // 
            this._ok.Location = new System.Drawing.Point(112, 66);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 2;
            this._ok.Text = "OK";
            this._ok.UseVisualStyleBackColor = true;
            this._ok.Click += new System.EventHandler(this._ok_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 23);
            this.textBox1.TabIndex = 3;
            // 
            // QuestionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 97);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this._ok);
            this.Controls.Add(this._text);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuestionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuestionDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label _text;
        private Button _ok;
        private TextBox textBox1;
    }
}