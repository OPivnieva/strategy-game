using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Strategy
{
    internal partial class Buildes : Form
    {
        Character player;
        List<Unit> units;
        List<Squad> newArmy=new List<Squad>();
        bool armySend = false;
        
        public Buildes(Character walkingPlayer)
        {
            InitializeComponent();
            toolTipArmy.SetToolTip(listBoxArmy, "Click to delete.");
            if (walkingPlayer != null)
            {
                player = walkingPlayer;
                Info();
            }
        }

        private void Info()
        {
            InfoResources();
            InfoTownHall();
            InfoHouses();
            InfoWalls();
            InfoTemple();
            InfoBarracks();
        }
        private void InfoResources()
        {
            toolStripStatusCoins.Text="Coins: " + player.castle.GetCoins();
            toolStripStatusPeople.Text = "People: " + player.castle.GetPeople();
        }
        private void InfoTownHall()
        {
            lblLevelTownhall.Text= "Level: " + player.castle.GetLevel(1);
            lblMaxLevelTownhall.Text = "Max. level: " + player.castle.GetMaxLevel(1);
            lblCostUpgradeTownhall.Text = "Upgrade cost: " + player.castle.GetCost(1);
            List<object> details = player.castle.GetDetailsUpgrade(1);
            if (Convert.ToInt32(details[0]) != 0)
            lblTax.Text = "Tax: " + details[0];
            else
                lblTax.Text = "Tax:";
            if (Convert.ToInt32(details[1]) != 0)
                lblNextTax.Text = "Tax on next level: " + details[1];
            else
                lblNextTax.Text = "Tax on next level:";
            btnUpgradeTownhall.Enabled=player.castle.CanBeUpgrade(1);
        }

        private void InfoHouses()
        {
            lblLevelHouses.Text = "Level: " + player.castle.GetLevel(2);
            lblMaxLevelHouses.Text = "Max. level: " + player.castle.GetMaxLevel(2);
            lblCostUpgradeHouses.Text = "Upgrade cost: " + player.castle.GetCost(2);
            List<object> details = player.castle.GetDetailsUpgrade(2);
            if (Convert.ToInt32(details[0]) != 0)
                lblLimitHouses.Text = "Limit: " + details[0];
            else
                lblLimitHouses.Text = "Limit:";
            if (Convert.ToInt32(details[1]) != 0)
                lblNextLimitHouses.Text = "Limit on next level: " + details[1];
            else
                lblNextLimitHouses.Text = "Limit on next level:";

            if (Convert.ToInt32(details[2]) != 0)
                lblPeopleHouses.Text = "People: " + details[2];
            else
                lblPeopleHouses.Text = "People:";
            if (Convert.ToInt32(details[3]) != 0)
                lblNextPeopleHouses.Text = "People on next level: " + details[3];
            else
                lblNextPeopleHouses.Text = "People on next level:";
            btntUpgradeHouses.Enabled = player.castle.CanBeUpgrade(2);
        }

        private void InfoWalls()
        {
            lblLevelWalls.Text = "Level: " + player.castle.GetLevel(3);
            lblMaxLevelWalls.Text = "Max. level: " + player.castle.GetMaxLevel(3);
            lblCostUpgradeWalls.Text = "Upgrade cost: " + player.castle.GetCost(3);
            List<object> details = player.castle.GetDetailsUpgrade(3);
            if (Convert.ToInt32(details[0]) != 0)
                lblBonusWalls.Text = "Bonus: " + details[0];
            else
                lblBonusWalls.Text = "Bonus:";
            if (Convert.ToInt32(details[1]) != 0)
                lblNextBonusWalls.Text = "Bonus on next level: " + details[1];
            else
                lblNextBonusWalls.Text = "Bonus on next level:";
            btnUpgradeWalls.Enabled = player.castle.CanBeUpgrade(3);
        }

        private void InfoTemple()
        {
            lblLevelTemple.Text = "Level: " + player.castle.GetLevel(4);
            lblMaxLevelTemple.Text = "Max. level: " + player.castle.GetMaxLevel(4);
            lblCostUpgradeTemple.Text = "Upgrade cost: " + player.castle.GetCost(4);
            List<object> details = player.castle.GetDetailsUpgrade(4);
            if (Convert.ToInt32(details[0]) != 0)
                lblCampaignTemple.Text = "Campaign: " + details[0];
            else
                lblCampaignTemple.Text = "Campaign:";
            if (Convert.ToInt32(details[1]) != 0)
                lblNextCampaignTemple.Text = "Campaign on next level: " + details[1];
            else
                lblNextCampaignTemple.Text = "Campaign on next level:";

            if (Convert.ToInt32(details[2]) != 0)
                lblDefenseTemple.Text = "Defense: " + details[2];
            else
                lblDefenseTemple.Text = "Defense:";
            if (Convert.ToInt32(details[3]) != 0)
                lblNextDefenseTemple.Text = "Defense on next level: " + details[3];
            else
                lblNextDefenseTemple.Text = "Defense on next level:";
            btnUpgradeTemple.Enabled = player.castle.CanBeUpgrade(4);
        }

        private void InfoBarracks()
        {
            lblLevelBarracks.Text = "Level: " + player.castle.GetLevel(5);
            lblMaxLevelBarracks.Text = "Max. level: " + player.castle.GetMaxLevel(5);
            lblCostUpgradeBarracks.Text = "Upgrade cost: " + player.castle.GetCost(5);
            List<object> details = player.castle.GetDetailsUpgrade(5);
            if (Convert.ToString(details[0]) != "")
                lblUpgradeBarracks.Text = "Upgrade: " + details[0];
            else
                lblUpgradeBarracks.Text = "Upgrade:";
            //need check
            units = (List<Unit>)details[1];
            List<Squad> ready = (List<Squad>)details[2];
            List<(Squad,int)> train = (List<(Squad,int)>)details[3];
            comboBoxType.Items.Clear();
            comboBoxArmy.Items.Clear();
            comboBoxType.Text = "";
            comboBoxArmy.Text = "";
            textBoxNumber.Text = "";
            textBoxArmy.Text = "";
            dataGridViewUnits.Rows.Clear();
            for (int i = 0; i < units.Count; i++)
            {
                dataGridViewUnits.Rows.Add();
                dataGridViewUnits.Rows[i].Cells["Unit"].Value = units[i].Name;
                dataGridViewUnits.Rows[i].Cells["Defense"].Value = units[i].Protection;
                dataGridViewUnits.Rows[i].Cells["Attack"].Value = units[i].Attack;
                dataGridViewUnits.Rows[i].Cells["Speed"].Value = units[i].SpeedAndInitiative;
                dataGridViewUnits.Rows[i].Cells["Time"].Value = units[i].TimePreparation;
                dataGridViewUnits.Rows[i].Cells["Cost"].Value = units[i].PriceCoins + " coins/" + units[i].PricePeople + " man";
                foreach (Squad unit in ready)
                {
                    if (units[i].Name == unit.UnitInSquad.Name & unit.CountUnit>0)
                    {
                        dataGridViewUnits.Rows[i].Cells["Ready"].Value = unit.CountUnit;
                        comboBoxArmy.Items.Add(unit.UnitInSquad.Name);
                        break;
                    }
                }
                foreach (var unit in train)
                {
                    if (units[i].Name == unit.Item1.UnitInSquad.Name)
                    {
                        if (dataGridViewUnits.Rows[i].Cells["Train"].Value != null)
                        {
                            if (dataGridViewUnits.Rows[i].Cells["Train"].Value.ToString() == "")
                                dataGridViewUnits.Rows[i].Cells["Train"].Value = 0;
                            dataGridViewUnits.Rows[i].Cells["Train"].Value = Convert.ToInt32(dataGridViewUnits.Rows[i].Cells["Train"].Value) + unit.Item1.CountUnit;
                        }
                        else
                        {
                            dataGridViewUnits.Rows[i].Cells["Train"].Value = unit.Item1.CountUnit;
                        }
                    }
                }
                comboBoxType.Items.Add(units[i].Name);
                btnUpgradeBarracks.Enabled = player.castle.CanBeUpgrade(5);
            }
        }

        private void btnUpgrateTownhall_Click(object sender, EventArgs e)
        {
            if (UpgradeBuild(1))//id build
            {
                InfoTownHall();
            }
          
        }

        private void btntUpgradeHouses_Click(object sender, EventArgs e)
        {
            if (UpgradeBuild(2))//id build
            {
                InfoHouses();
            }
        }

        private void btnUpgradeWalls_Click(object sender, EventArgs e)
        {
            if (UpgradeBuild(3))//id build
            {
                InfoWalls();
            }
           
        }

        private void btnUpgradeTemple_Click(object sender, EventArgs e)
        {
            if (UpgradeBuild(4))//id build
            {
                InfoTemple();
            }
           
        }

        private void btnUpgradeBarracks_Click(object sender, EventArgs e)
        {
            if (UpgradeBuild(5))//id build
            {
                InfoBarracks();
            }
        }

        private bool UpgradeBuild(int id)
        {
            if (player.castle.GetCoins() >= player.castle.GetCost(id))
            {
                player.castle.UpgradeBuild(id);
                InfoResources();
                return true;
            }
            else
            {
                MessageBox.Show("Not enough coins!", "Attention", MessageBoxButtons.OK);
                return false;
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == -1 || (textBoxNumber.Text == "" || textBoxNumber.Text == "0"))
            {
                MessageBox.Show("Choose unit and number!", "Attention", MessageBoxButtons.OK);
                return;
            }

            string nameUnit = comboBoxType.Text;
            int countUnit = Convert.ToInt32(textBoxNumber.Text);
            foreach (Unit unit in units)
            {
                if (unit.Name == nameUnit)
                {
                    if (player.castle.GetCoins() >= (countUnit * unit.PriceCoins) & player.castle.GetPeople() >= (countUnit * unit.PricePeople))
                    {
                        player.castle.TrainUnits(nameUnit, countUnit);
                        player.castle.ReducePeople(countUnit * unit.PricePeople);
                        player.castle.ReduceCoins(countUnit * unit.PriceCoins);
                        InfoBarracks();
                        InfoResources();
                    }
                    else
                        MessageBox.Show("Not enough coins or people!", "Attention", MessageBoxButtons.OK);
                }
            }
        }

        private void textBoxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar < 48 || e.KeyChar > 57) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void btnAtmy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to send army?", "Attention", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (newArmy.Count > 0)
                {
                    MapManager.ChooseArmyGoal(newArmy);
                    armySend = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Add units!", "Attention", MessageBoxButtons.OK);
                }
            }
        }

        private void btnAddToArmy_Click(object sender, EventArgs e)
        {
            if (comboBoxArmy.SelectedIndex == -1 || (textBoxArmy.Text == "" || textBoxArmy.Text == "0"))
            {
                MessageBox.Show("Choose unit and number!", "Attention", MessageBoxButtons.OK);
                return;
            }

            string nameUnit = comboBoxArmy.Text;
            int countUnit = Convert.ToInt32(textBoxArmy.Text);
            foreach (Squad unit in player.castle.GetReadyUnits())
            {
                if (unit.UnitInSquad.Name == nameUnit)
                {
                    if (unit.CountUnit >= countUnit)
                    {
                        listBoxArmy.Items.Add(nameUnit + " - " + countUnit);
                        Squad newSquad = new Squad
                        {
                            UnitInSquad = unit.UnitInSquad,
                            CountUnit = countUnit
                        };
                        newArmy.Add(newSquad);
                        player.castle.ReduceReadyUnit(nameUnit, countUnit);
                        InfoBarracks();
                    }
                    else
                        MessageBox.Show("Not enough people!", "Attention", MessageBoxButtons.OK);
                    break;
                }
            }
        }

        private void Buildes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (player != null)
            {
                if (newArmy.Count != 0 & !armySend)
                {
                    player.castle.ReturnUnitsInCastle(newArmy);
                }
            }
        }

        private void listBoxArmy_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (listBoxArmy.SelectedIndex!=-1)
                {
                    int i = listBoxArmy.SelectedIndex;
                    List<Squad> list = new List<Squad>
                    {
                        newArmy[i]
                    };
                    player.castle.ReturnUnitsInCastle(list);
                    newArmy.RemoveAt(i);
                    listBoxArmy.Items.RemoveAt(i);
                    InfoBarracks();
                }
            }
        }
    }
}
