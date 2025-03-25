using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class Walls : IBuilding
    {
        int id;
        string name;
        int maxLevel;
        int defensePlus;//how much increase the defense of the army in defense for levelPlus levels
        int levelPlus;//how many levels increase defense
        int maxPlus;
        int cost;//coins × current level value
        int defense=0;//the defense of the army in defense now
        int level = 1;


        public void AddCharacteristics(object[] characteristics)
        {
            if (characteristics != null)
            {
                this.id = Convert.ToInt32(characteristics[0].ToString());
                this.name= characteristics[1].ToString();
                this.maxLevel = Convert.ToInt32(characteristics[2].ToString());
                this.defensePlus = Convert.ToInt32(characteristics[3].ToString());
                this.levelPlus = Convert.ToInt32(characteristics[4].ToString());
                this.maxPlus = Convert.ToInt32(characteristics[5].ToString());
                this.cost = Convert.ToInt32(characteristics[6].ToString());
            }
        }

        public bool UpgradeBuild(ref int coins)//update the Walls and get the results
        {
            if (coins < (cost * level))
                return false;//not enough coins to upgrade

            if (!CanBeUpgrade())
                return false; //walls is already at the maximum level

            if ((defense + defensePlus) > maxPlus)
                return false; //defense is already maximal

            coins = coins - (cost * level);
            level++;
            if (level % levelPlus == 0)
            {
                defense = defense + defensePlus;
            }
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
        public int GetDefense() => defense;
        public int GetPlusDefense()
        {
            if (CanBeUpgrade())
                if (level % levelPlus != 0)
                    return defense+defensePlus;
                else
                    return 0;//max level, not can be upgrade
            else
                return 0;//max level, not can be upgrade
        }
        public List<object> GetDetailsUpgrade()
        {
            List<object> details = new List<object>();
            details.Add(GetDefense());
            details.Add(GetPlusDefense());
            return details;
        }

    }
}
