namespace HistoryMaps
{
    partial class ModifyEventControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifyEventControl));
            this._picture = new System.Windows.Forms.PictureBox();
            this._flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._back = new System.Windows.Forms.Button();
            this._save = new System.Windows.Forms.Button();
            this._selectCountry = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._picture)).BeginInit();
            this._flowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _picture
            // 
            this._picture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._picture.BackColor = System.Drawing.Color.White;
            this._picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._picture.Location = new System.Drawing.Point(0, 30);
            this._picture.Margin = new System.Windows.Forms.Padding(0);
            this._picture.Name = "_picture";
            this._picture.Size = new System.Drawing.Size(557, 120);
            this._picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._picture.TabIndex = 0;
            this._picture.TabStop = false;
            this._picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this._picture_MouseDown);
            this._picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this._picture_MouseMove);
            // 
            // _flowPanel
            // 
            this._flowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._flowPanel.BackColor = System.Drawing.Color.White;
            this._flowPanel.Controls.Add(this._back);
            this._flowPanel.Controls.Add(this._save);
            this._flowPanel.Controls.Add(this._selectCountry);
            this._flowPanel.Location = new System.Drawing.Point(0, 0);
            this._flowPanel.Name = "_flowPanel";
            this._flowPanel.Size = new System.Drawing.Size(557, 30);
            this._flowPanel.TabIndex = 1;
            // 
            // _back
            // 
            this._back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_back.BackgroundImage")));
            this._back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._back.Location = new System.Drawing.Point(3, 3);
            this._back.Name = "_back";
            this._back.Size = new System.Drawing.Size(26, 23);
            this._back.TabIndex = 0;
            this._back.UseVisualStyleBackColor = true;
            this._back.Click += new System.EventHandler(this._back_Click);
            // 
            // _save
            // 
            this._save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_save.BackgroundImage")));
            this._save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._save.Location = new System.Drawing.Point(35, 3);
            this._save.Name = "_save";
            this._save.Size = new System.Drawing.Size(26, 23);
            this._save.TabIndex = 1;
            this._save.UseVisualStyleBackColor = true;
            this._save.Click += new System.EventHandler(this._save_Click);
            // 
            // _selectCountry
            // 
            this._selectCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._selectCountry.FormattingEnabled = true;
            this._selectCountry.Location = new System.Drawing.Point(67, 3);
            this._selectCountry.Name = "_selectCountry";
            this._selectCountry.Size = new System.Drawing.Size(121, 23);
            this._selectCountry.TabIndex = 2;
            this._selectCountry.SelectedIndexChanged += new System.EventHandler(this._selectCountry_SelectedIndexChanged);
            // 
            // ModifyEventControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._flowPanel);
            this.Controls.Add(this._picture);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ModifyEventControl";
            this.Size = new System.Drawing.Size(557, 150);
            ((System.ComponentModel.ISupportInitialize)(this._picture)).EndInit();
            this._flowPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox _picture;
        private FlowLayoutPanel _flowPanel;
        private Button _back;
        private Button _save;
        private ComboBox _selectCountry;
    }
}
