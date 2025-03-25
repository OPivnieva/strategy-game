namespace Strategy
{
    partial class Start
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.btnStart = new System.Windows.Forms.Button();
            this.textBoxEnemies = new System.Windows.Forms.TextBox();
            this.radiobtnEnemy = new System.Windows.Forms.RadioButton();
            this.radiobtnRandom = new System.Windows.Forms.RadioButton();
            this.toolTipNumber = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxCell = new System.Windows.Forms.TextBox();
            this.lblCells = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(134, 92);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // textBoxEnemies
            // 
            this.textBoxEnemies.Location = new System.Drawing.Point(134, 27);
            this.textBoxEnemies.Name = "textBoxEnemies";
            this.textBoxEnemies.Size = new System.Drawing.Size(31, 20);
            this.textBoxEnemies.TabIndex = 2;
            this.toolTipNumber.SetToolTip(this.textBoxEnemies, "From 3 to 5");
            this.textBoxEnemies.Visible = false;
            this.textBoxEnemies.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumber_KeyPress);
            // 
            // radiobtnEnemy
            // 
            this.radiobtnEnemy.AutoSize = true;
            this.radiobtnEnemy.Location = new System.Drawing.Point(12, 27);
            this.radiobtnEnemy.Name = "radiobtnEnemy";
            this.radiobtnEnemy.Size = new System.Drawing.Size(116, 17);
            this.radiobtnEnemy.TabIndex = 3;
            this.radiobtnEnemy.Text = "Number of enemies";
            this.toolTipNumber.SetToolTip(this.radiobtnEnemy, "From 3 to 5");
            this.radiobtnEnemy.UseVisualStyleBackColor = true;
            this.radiobtnEnemy.CheckedChanged += new System.EventHandler(this.radiobtnNumber_CheckedChanged);
            // 
            // radiobtnRandom
            // 
            this.radiobtnRandom.AutoSize = true;
            this.radiobtnRandom.Checked = true;
            this.radiobtnRandom.Location = new System.Drawing.Point(12, 59);
            this.radiobtnRandom.Name = "radiobtnRandom";
            this.radiobtnRandom.Size = new System.Drawing.Size(65, 17);
            this.radiobtnRandom.TabIndex = 4;
            this.radiobtnRandom.TabStop = true;
            this.radiobtnRandom.Text = "Random";
            this.radiobtnRandom.UseVisualStyleBackColor = true;
            this.radiobtnRandom.CheckedChanged += new System.EventHandler(this.radiobtnRandom_CheckedChanged);
            // 
            // textBoxCell
            // 
            this.textBoxCell.Location = new System.Drawing.Point(283, 44);
            this.textBoxCell.Name = "textBoxCell";
            this.textBoxCell.Size = new System.Drawing.Size(31, 20);
            this.textBoxCell.TabIndex = 6;
            this.toolTipNumber.SetToolTip(this.textBoxCell, "From 3 to 5");
            this.textBoxCell.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCell_KeyPress);
            // 
            // lblCells
            // 
            this.lblCells.AutoSize = true;
            this.lblCells.Location = new System.Drawing.Point(188, 47);
            this.lblCells.Name = "lblCells";
            this.lblCells.Size = new System.Drawing.Size(80, 13);
            this.lblCells.TabIndex = 5;
            this.lblCells.Text = "Number of cells";
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 139);
            this.Controls.Add(this.textBoxCell);
            this.Controls.Add(this.lblCells);
            this.Controls.Add(this.radiobtnRandom);
            this.Controls.Add(this.radiobtnEnemy);
            this.Controls.Add(this.textBoxEnemies);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Strategy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox textBoxEnemies;
        private System.Windows.Forms.RadioButton radiobtnEnemy;
        private System.Windows.Forms.RadioButton radiobtnRandom;
        private System.Windows.Forms.ToolTip toolTipNumber;
        private System.Windows.Forms.Label lblCells;
        private System.Windows.Forms.TextBox textBoxCell;
    }
}