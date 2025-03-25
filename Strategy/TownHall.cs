using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class TownHall : IBuilding 
    {
        int id;
        string name;
        int maxLevel;
        int plusTax;//how much increases for each level (after the first) tax
        int maxTax;
        int cost;//upgrade cost(coins × current level value)
        double tax;//how much in % from the number of people in the castle
        int level=1;

       
        public void AddСharacteristics(object[] characteristics)
        {
            if (characteristics != null)
            {
                this.id = Convert.ToInt32(characteristics[0].ToString());
                this.name= characteristics[1].ToString();
                this.maxLevel = Convert.ToInt32(characteristics[2].ToString());
                this.plusTax = Convert.ToInt32(characteristics[3].ToString());
                this.maxTax = Convert.ToInt32(characteristics[4].ToString());
                this.cost = Convert.ToInt32(characteristics[5].ToString());
                this.tax = Convert.ToDouble(characteristics[6].ToString());
            }
        }
       
        public bool UpgradeBuild(ref int coins) //update the Townhall and get the results
        {
            if (coins < (cost * level))
                return false; //not enough coins to upgrade
            if (!CanBeUpgrade())
                return false;//townhall is already at the maximum level

            if ((tax + plusTax) > maxTax)
                return false; //tax is already maximal

            coins = coins - (cost * level);
            level++;
            tax = tax + plusTax;
            return true;
        }


        public string GetName() => name;
       
        public int GetId() => id;
        
        public int UpdateCoins(int people)
        {
            double pro = tax/100;
            return (int)(people*pro);
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
        public int GetLevel() => level;

        public int GetMaxLevel() => maxLevel;

        public double GetTax() => tax;

        public int GetPlusTax()
        {
            if (CanBeUpgrade())
                return (int)tax+plusTax;
            else
                return 0;//max level, not can be upgrade
        }

        public List<object> GetDetailsUpgrade()
        {
            List<object> details = new List<object>();
            details.Add(GetTax());
            details.Add(GetPlusTax());
            return details;
        }


    }
}
