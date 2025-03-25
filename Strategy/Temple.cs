using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class Temple : IBuilding 
    {
        int id;
        string name;
        int maxLevel;
        int defensePlus;//how much increase the attack of the army in defense for levelPlus levels
        int defenseLevelPlus;//for how many levels increases the attack of the army in defense
        int defenseMaxPlus;
        int campaignPlus;//how much increase the attack of the army in campaign for levelPlus levels
        int campaignLevelPlus;//for how many levels increases the attack of the army in campaign
        int campaignMaxPlus;
        int cost;//coins × current level value
        int defense = 0;//the attack of the army in defense now
        int campaign = 0;//the attack of the army in campaign now
        int level = 1;

        public void AddCharacteristics(object[] characteristics)
        {
            if (characteristics != null)
            {

                this.id = Convert.ToInt32(characteristics[0].ToString());
                this.name = characteristics[1].ToString();
                this.maxLevel = Convert.ToInt32(characteristics[2].ToString());
                this.defensePlus = Convert.ToInt32(characteristics[3].ToString());
                this.defenseLevelPlus = Convert.ToInt32(characteristics[4].ToString());
                this.defenseMaxPlus = Convert.ToInt32(characteristics[5].ToString());
                this.campaignPlus = Convert.ToInt32(characteristics[6].ToString());
                this.campaignLevelPlus = Convert.ToInt32(characteristics[7].ToString());
                this.campaignMaxPlus = Convert.ToInt32(characteristics[8].ToString());
                this.cost = Convert.ToInt32(characteristics[9].ToString());
            }
        }
        //update the Temple and get the results
        public bool UpgradeBuild(ref int coins)
        {
            if (coins < (cost * level))
                return false;//not enough coins to upgrade

            if (!CanBeUpgrade())
                return false; //tempel is already at the maximum level

            if (!DefenseCheck() && !CampaignCheck())
                return false; //temple is already at the maximum defense or campaign level


            coins = coins - (cost * level);
            level++;

            if (level % defenseLevelPlus == 0)
            {
                defense = defense + defensePlus;
            }
            if (level % campaignLevelPlus == 0)
            {
                campaign = campaign + campaignPlus;
            }
            return true;
        }
        private bool DefenseCheck()
        {
            if (level >= 9)
            {
                if ((defense + defensePlus) <= defenseMaxPlus)
                {
                    return true;
                }
                else return false;
                
            }
            else
                return true;
        }
        private bool CampaignCheck()
        {
            if (level >= 9)
            {
                if ((campaign + campaignPlus) <= campaignMaxPlus)
                {
                    return true;
                }
                else return false;

            }
            else
                return true;
        }

        public bool CanBeUpgrade()
        {
            if ((level + 1) <= maxLevel)
                return true;
            else
                return false;//max level, not can be upgrade
        }
        public int GetCost()
        {
            if (CanBeUpgrade())
                return cost * level;
            else
                return 0;//max level, not can be upgrade
        }
        public string GetName() => name;

        public int GetId() => id;

        public int GetLevel() => level;
        
        public int GetMaxLevel() => maxLevel;
        
        public int GetCampaign() => campaign;
       
        public int GetPlusCampaign()
        {
            if ((level + 1) % campaignLevelPlus == 0)
            {
                if (CanBeUpgrade())
                    return campaign + campaignPlus;
                else 
                    return 0;//max level, not can be upgrade
            }
            else
                return 0;//max level, not can be upgrade
        }
        public int GetDefense() => defense;
       
        public int GetPlusDefense()
        {
            if ((level + 1) % defenseLevelPlus == 0)
            {
                if (CanBeUpgrade())
                    return defense + defensePlus;
                else 
                    return 0;//max level, not can be upgrade
            }
            else
                return 0;//max level, not can be upgrade
        }

        public List<object> GetDetailsUpgrade()
        {
            List<object> details = new List<object>();
            details.Add(GetCampaign());
            details.Add(GetPlusCampaign());
            details.Add(GetDefense());
            details.Add(GetPlusDefense());
            return details;
        }
    }
}
