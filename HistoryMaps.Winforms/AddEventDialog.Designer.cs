namespace HistoryMaps
{
    partial class AddEventDialog
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
            this._name = new System.Windows.Forms.TextBox();
            this._year = new System.Windows.Forms.NumericUpDown();
            this._endYear = new System.Windows.Forms.NumericUpDown();
            this._hasEndYear = new System.Windows.Forms.CheckBox();
            this._ok = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._yearLabel = new System.Windows.Forms.Label();
            this._nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._endYear)).BeginInit();
            this.SuspendLayout();
            // 
            // _name
            // 
            this._name.Location = new System.Drawing.Point(1, 34);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(403, 23);
            this._name.TabIndex = 0;
            // 
            // _year
            // 
            this._year.Location = new System.Drawing.Point(0, 93);
            this._year.Name = "_year";
            this._year.Size = new System.Drawing.Size(404, 23);
            this._year.TabIndex = 1;
            // 
            // _endYear
            // 
            this._endYear.Enabled = false;
            this._endYear.Location = new System.Drawing.Point(1, 154);
            this._endYear.Name = "_endYear";
            this._endYear.Size = new System.Drawing.Size(403, 23);
            this._endYear.TabIndex = 2;
            // 
            // _hasEndYear
            // 
            this._hasEndYear.AutoSize = true;
            this._hasEndYear.Location = new System.Drawing.Point(1, 132);
            this._hasEndYear.Name = "_hasEndYear";
            this._hasEndYear.Size = new System.Drawing.Size(108, 19);
            this._hasEndYear.TabIndex = 3;
            this._hasEndYear.Text = "Год окончания";
            this._hasEndYear.UseVisualStyleBackColor = true;
            this._hasEndYear.CheckedChanged += new System.EventHandler(this._hasEndYear_CheckedChanged);
            // 
            // _ok
            // 
            this._ok.Location = new System.Drawing.Point(0, 199);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 4;
            this._ok.Text = "ОК";
            this._ok.UseVisualStyleBackColor = true;
            this._ok.Click += new System.EventHandler(this._ok_Click);
            // 
            // _cancel
            // 
            this._cancel.Location = new System.Drawing.Point(329, 199);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 5;
            this._cancel.Text = "Отмена";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this._cancel_Click);
            // 
            // _yearLabel
            // 
            this._yearLabel.AutoSize = true;
            this._yearLabel.Location = new System.Drawing.Point(0, 75);
            this._yearLabel.Name = "_yearLabel";
            this._yearLabel.Size = new System.Drawing.Size(26, 15);
            this._yearLabel.TabIndex = 6;
            this._yearLabel.Text = "Год";
            // 
            // _nameLabel
            // 
            this._nameLabel.AutoSize = true;
            this._nameLabel.Location = new System.Drawing.Point(0, 16);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(59, 15);
            this._nameLabel.TabIndex = 7;
            this._nameLabel.Text = "Название";
            // 
            // AddEventDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(405, 223);
            this.Controls.Add(this._nameLabel);
            this.Controls.Add(this._yearLabel);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._ok);
            this.Controls.Add(this._hasEndYear);
            this.Controls.Add(this._endYear);
            this.Controls.Add(this._year);
            this.Controls.Add(this._name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEventDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление события";
            ((System.ComponentModel.ISupportInitialize)(this._year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._endYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox _name;
        private NumericUpDown _year;
        private NumericUpDown _endYear;
        private CheckBox _hasEndYear;
        private Button _ok;
        private Button _cancel;
        private Label _yearLabel;
        private Label _nameLabel;
    }
}