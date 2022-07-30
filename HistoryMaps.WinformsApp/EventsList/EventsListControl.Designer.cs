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
            this._table = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // _table
            // 
            this._table.AutoSize = true;
            this._table.ColumnCount = 3;
            this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._table.Dock = System.Windows.Forms.DockStyle.Top;
            this._table.Location = new System.Drawing.Point(0, 0);
            this._table.Name = "_table";
            this._table.RowCount = 2;
            this._table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._table.Size = new System.Drawing.Size(150, 0);
            this._table.TabIndex = 0;
            // 
            // EventListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._table);
            this.Name = "EventsListControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel _table;
    }
}
