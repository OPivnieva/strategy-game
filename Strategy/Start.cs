using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strategy
{
    public partial class Start : Form
    {
        public int numberOfEnemies=0;
        public int numberOfCells = 0;
        public Start()
        {
            InitializeComponent();
            textBoxCell.Text = "15";

        }

        private void radiobtnRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobtnRandom.Checked)
            {
                textBoxEnemies.Text = "";
                textBoxEnemies.Visible = false;
            }

        }

        private void radiobtnNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobtnEnemy.Checked)
            {
                textBoxEnemies.Visible = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (radiobtnEnemy.Checked)
            {
                if (textBoxEnemies.Text == "")
                {
                    MessageBox.Show("Enter the number of enemies from 3 to 5", "Attention", MessageBoxButtons.OK);
                }
                else if (textBoxCell.Text == "")
                {
                    MessageBox.Show("Enter the number of cells from 15 to 50", "Attention", MessageBoxButtons.OK);
                }
                else
                {
                    int enemies = Convert.ToInt32(textBoxEnemies.Text);
                    int cells = Convert.ToInt32(textBoxCell.Text);
                    if (enemies < 3 | enemies > 5)
                    {
                        MessageBox.Show("Enter the number of enemies from 3 to 5", "Attention", MessageBoxButtons.OK);
                    }
                    else if (cells < 15 | cells > 50)
                    {
                        MessageBox.Show("Enter the number of cells from 15 to 50", "Attention", MessageBoxButtons.OK);
                    }
                    else
                    {
                        numberOfEnemies = enemies;
                        numberOfCells = cells;
                        
                        StartGame();//number
                    }


                }
            }
            else
            {
               if (textBoxCell.Text == "")
                {
                    MessageBox.Show("Enter the number of cells from 15 to 50", "Attention", MessageBoxButtons.OK);
                }
                else
                {
                    int cells = Convert.ToInt32(textBoxCell.Text);

                    if (cells < 15 | cells > 50)
                    {
                        MessageBox.Show("Enter the number of cells from 15 to 50", "Attention", MessageBoxButtons.OK);
                    }
                    else
                    {
                        numberOfEnemies = 0;
                        numberOfCells = cells;
                        StartGame();//random
                    }

                }
                
            }
        }
        private void StartGame()
        {
            DialogResult= System.Windows.Forms.DialogResult.Yes;

        }

        private void textBoxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 50 || e.KeyChar >= 54) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBoxCell_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar < 48 || e.KeyChar > 57) && number != 8)
            {
                e.Handled = true;
            }
        }

        
    }
}
