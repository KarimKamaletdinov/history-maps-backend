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
            this._delete = new System.Windows.Forms.Button();
            this._plus = new System.Windows.Forms.Button();
            this._minus = new System.Windows.Forms.Button();
            this._pencil = new System.Windows.Forms.Button();
            this._fill = new System.Windows.Forms.Button();
            this._undo = new System.Windows.Forms.Button();
            this._redo = new System.Windows.Forms.Button();
            this._createCountry = new System.Windows.Forms.Button();
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
            this._picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this._picture_MouseUp);
            // 
            // _flowPanel
            // 
            this._flowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._flowPanel.BackColor = System.Drawing.Color.White;
            this._flowPanel.Controls.Add(this._back);
            this._flowPanel.Controls.Add(this._save);
            this._flowPanel.Controls.Add(this._selectCountry);
            this._flowPanel.Controls.Add(this._createCountry);
            this._flowPanel.Controls.Add(this._delete);
            this._flowPanel.Controls.Add(this._plus);
            this._flowPanel.Controls.Add(this._minus);
            this._flowPanel.Controls.Add(this._pencil);
            this._flowPanel.Controls.Add(this._fill);
            this._flowPanel.Controls.Add(this._undo);
            this._flowPanel.Controls.Add(this._redo);
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
            // _delete
            // 
            this._delete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_delete.BackgroundImage")));
            this._delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._delete.Location = new System.Drawing.Point(226, 3);
            this._delete.Name = "_delete";
            this._delete.Size = new System.Drawing.Size(26, 23);
            this._delete.TabIndex = 5;
            this._delete.UseVisualStyleBackColor = true;
            this._delete.Click += new System.EventHandler(this._delete_Click);
            // 
            // _plus
            // 
            this._plus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_plus.BackgroundImage")));
            this._plus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._plus.Location = new System.Drawing.Point(258, 3);
            this._plus.Name = "_plus";
            this._plus.Size = new System.Drawing.Size(24, 23);
            this._plus.TabIndex = 3;
            this._plus.UseVisualStyleBackColor = true;
            this._plus.Click += new System.EventHandler(this._plus_Click);
            // 
            // _minus
            // 
            this._minus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_minus.BackgroundImage")));
            this._minus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._minus.Location = new System.Drawing.Point(288, 3);
            this._minus.Name = "_minus";
            this._minus.Size = new System.Drawing.Size(24, 23);
            this._minus.TabIndex = 4;
            this._minus.UseVisualStyleBackColor = true;
            this._minus.Click += new System.EventHandler(this._minus_Click);
            // 
            // _pencil
            // 
            this._pencil.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_pencil.BackgroundImage")));
            this._pencil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._pencil.Enabled = false;
            this._pencil.Location = new System.Drawing.Point(318, 3);
            this._pencil.Name = "_pencil";
            this._pencil.Size = new System.Drawing.Size(24, 23);
            this._pencil.TabIndex = 6;
            this._pencil.UseVisualStyleBackColor = true;
            this._pencil.Click += new System.EventHandler(this._pencil_Click);
            // 
            // _fill
            // 
            this._fill.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_fill.BackgroundImage")));
            this._fill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._fill.Location = new System.Drawing.Point(348, 3);
            this._fill.Name = "_fill";
            this._fill.Size = new System.Drawing.Size(24, 23);
            this._fill.TabIndex = 7;
            this._fill.UseVisualStyleBackColor = true;
            this._fill.Click += new System.EventHandler(this._fill_Click);
            // 
            // _undo
            // 
            this._undo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_undo.BackgroundImage")));
            this._undo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._undo.Location = new System.Drawing.Point(378, 3);
            this._undo.Name = "_undo";
            this._undo.Size = new System.Drawing.Size(24, 23);
            this._undo.TabIndex = 8;
            this._undo.UseVisualStyleBackColor = true;
            this._undo.Click += new System.EventHandler(this._undo_Click);
            // 
            // _redo
            // 
            this._redo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_redo.BackgroundImage")));
            this._redo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._redo.Location = new System.Drawing.Point(408, 3);
            this._redo.Name = "_redo";
            this._redo.Size = new System.Drawing.Size(24, 23);
            this._redo.TabIndex = 9;
            this._redo.UseVisualStyleBackColor = true;
            this._redo.Click += new System.EventHandler(this._redo_Click);
            // 
            // _createCountry
            // 
            this._createCountry.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_createCountry.BackgroundImage")));
            this._createCountry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._createCountry.Location = new System.Drawing.Point(194, 3);
            this._createCountry.Name = "_createCountry";
            this._createCountry.Size = new System.Drawing.Size(26, 23);
            this._createCountry.TabIndex = 10;
            this._createCountry.UseVisualStyleBackColor = true;
            this._createCountry.Click += new System.EventHandler(this._createCountry_Click);
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
        private Button _plus;
        private Button _minus;
        private Button _delete;
        private Button _pencil;
        private Button _fill;
        private Button _undo;
        private Button _redo;
        private Button _createCountry;
    }
}
