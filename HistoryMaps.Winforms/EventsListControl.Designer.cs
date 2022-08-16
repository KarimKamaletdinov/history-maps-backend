namespace HistoryMaps
{
    partial class EventsListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventsListControl));
            this._table = new System.Windows.Forms.TableLayoutPanel();
            this._add = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this._createApp = new System.Windows.Forms.Button();
            this._loadAdded = new System.Windows.Forms.Button();
            this._reload = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _table
            // 
            this._table.AutoSize = true;
            this._table.ColumnCount = 3;
            this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this._table.Dock = System.Windows.Forms.DockStyle.Top;
            this._table.Location = new System.Drawing.Point(0, 0);
            this._table.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._table.Name = "_table";
            this._table.RowCount = 1;
            this._table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._table.Size = new System.Drawing.Size(218, 0);
            this._table.TabIndex = 0;
            // 
            // _add
            // 
            this._add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_add.BackgroundImage")));
            this._add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._add.Location = new System.Drawing.Point(159, 56);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(40, 40);
            this._add.TabIndex = 1;
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this._add_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this._table);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 112);
            this.panel1.TabIndex = 2;
            // 
            // _createApp
            // 
            this._createApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._createApp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_createApp.BackgroundImage")));
            this._createApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._createApp.Location = new System.Drawing.Point(21, 56);
            this._createApp.Name = "_createApp";
            this._createApp.Size = new System.Drawing.Size(40, 40);
            this._createApp.TabIndex = 4;
            this._createApp.UseVisualStyleBackColor = true;
            this._createApp.Click += new System.EventHandler(this._createApp_Click);
            // 
            // _loadAdded
            // 
            this._loadAdded.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._loadAdded.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_loadAdded.BackgroundImage")));
            this._loadAdded.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._loadAdded.Location = new System.Drawing.Point(67, 56);
            this._loadAdded.Name = "_loadAdded";
            this._loadAdded.Size = new System.Drawing.Size(40, 40);
            this._loadAdded.TabIndex = 3;
            this._loadAdded.UseVisualStyleBackColor = true;
            this._loadAdded.Click += new System.EventHandler(this._loadAdded_Click);
            // 
            // _reload
            // 
            this._reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._reload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_reload.BackgroundImage")));
            this._reload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._reload.Location = new System.Drawing.Point(113, 56);
            this._reload.Name = "_reload";
            this._reload.Size = new System.Drawing.Size(40, 40);
            this._reload.TabIndex = 2;
            this._reload.UseVisualStyleBackColor = true;
            this._reload.Click += new System.EventHandler(this._reload_Click);
            // 
            // EventsListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._add);
            this.Controls.Add(this._createApp);
            this.Controls.Add(this._loadAdded);
            this.Controls.Add(this._reload);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "EventsListControl";
            this.Size = new System.Drawing.Size(218, 112);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel _table;
        private Button _add;
        private Panel panel1;
        private Button _reload;
        private Button _loadAdded;
        private Button _createApp;
    }
}
