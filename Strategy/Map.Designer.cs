namespace Strategy
{
    partial class Map
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Map));
            this.mapDataGridView = new System.Windows.Forms.DataGridView();
            this.ListBoxMessages = new System.Windows.Forms.ListBox();
            this.Stepbtn = new System.Windows.Forms.Button();
            this.Cancelbtn = new System.Windows.Forms.Button();
            this.toolTipCamcel = new System.Windows.Forms.ToolTip(this.components);
            this.Guidebtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mapDataGridView
            // 
            this.mapDataGridView.AllowUserToAddRows = false;
            this.mapDataGridView.AllowUserToDeleteRows = false;
            this.mapDataGridView.AllowUserToResizeColumns = false;
            this.mapDataGridView.AllowUserToResizeRows = false;
            this.mapDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mapDataGridView.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mapDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.mapDataGridView.Location = new System.Drawing.Point(12, 12);
            this.mapDataGridView.Name = "mapDataGridView";
            this.mapDataGridView.ReadOnly = true;
            this.mapDataGridView.RowHeadersVisible = false;
            this.mapDataGridView.Size = new System.Drawing.Size(754, 712);
            this.mapDataGridView.TabIndex = 0;
            this.mapDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MapDataGridView_CellMouseDown);
            this.mapDataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.mapDataGridView_CellMouseEnter);
            this.mapDataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MapDataGridView_CellMouseUp);
            this.mapDataGridView.SizeChanged += new System.EventHandler(this.MapDataGridView_SizeChanged);
            // 
            // ListBoxMessages
            // 
            this.ListBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBoxMessages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ListBoxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListBoxMessages.FormattingEnabled = true;
            this.ListBoxMessages.ItemHeight = 24;
            this.ListBoxMessages.Location = new System.Drawing.Point(785, 41);
            this.ListBoxMessages.Name = "ListBoxMessages";
            this.ListBoxMessages.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ListBoxMessages.Size = new System.Drawing.Size(165, 580);
            this.ListBoxMessages.TabIndex = 1;
            this.ListBoxMessages.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBoxMessages_DrawItem);
            this.ListBoxMessages.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.ListBoxMessages_MeasureItem);
            // 
            // Stepbtn
            // 
            this.Stepbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Stepbtn.Location = new System.Drawing.Point(785, 641);
            this.Stepbtn.Name = "Stepbtn";
            this.Stepbtn.Size = new System.Drawing.Size(165, 38);
            this.Stepbtn.TabIndex = 2;
            this.Stepbtn.Text = "Step";
            this.Stepbtn.UseVisualStyleBackColor = true;
            this.Stepbtn.Click += new System.EventHandler(this.Stepbtn_Click);
            // 
            // Cancelbtn
            // 
            this.Cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancelbtn.Location = new System.Drawing.Point(785, 685);
            this.Cancelbtn.Name = "Cancelbtn";
            this.Cancelbtn.Size = new System.Drawing.Size(165, 39);
            this.Cancelbtn.TabIndex = 3;
            this.Cancelbtn.Text = "Cancel";
            this.Cancelbtn.UseVisualStyleBackColor = true;
            this.Cancelbtn.Visible = false;
            this.Cancelbtn.Click += new System.EventHandler(this.Cancelbtn_Click);
            // 
            // Guidebtn
            // 
            this.Guidebtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Guidebtn.Location = new System.Drawing.Point(785, 12);
            this.Guidebtn.Name = "Guidebtn";
            this.Guidebtn.Size = new System.Drawing.Size(165, 23);
            this.Guidebtn.TabIndex = 4;
            this.Guidebtn.Text = "Guide";
            this.Guidebtn.UseVisualStyleBackColor = true;
            this.Guidebtn.Click += new System.EventHandler(this.Guidebtn_Click);
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 747);
            this.Controls.Add(this.Guidebtn);
            this.Controls.Add(this.Cancelbtn);
            this.Controls.Add(this.Stepbtn);
            this.Controls.Add(this.ListBoxMessages);
            this.Controls.Add(this.mapDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Map";
            this.Text = "Map";
            ((System.ComponentModel.ISupportInitialize)(this.mapDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mapDataGridView;
        private System.Windows.Forms.ListBox ListBoxMessages;
        private System.Windows.Forms.Button Stepbtn;
        private System.Windows.Forms.Button Cancelbtn;
        private System.Windows.Forms.ToolTip toolTipCamcel;
        private System.Windows.Forms.Button Guidebtn;
    }
}

