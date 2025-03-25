using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Strategy
{
    internal class Houses : IBuilding
    {
        int id;
        string name;
        int maxLevel;
        int plusLimit;//how much the base population limit is increased for each level
        int maxLimit;
        int plusPeople;//how much increase the number of people who come to the castle each level
        int maxPeople;
        int cost;//coins × current level value
        int limit;// the base population limit is increased for each move
        int comePeople;//number of people who come to the castle each move
        int level = 1;

        public void AddCharacteristics(object[] characteristics)
        {
            if (characteristics != null)
            {
                this.id = Convert.ToInt32(characteristics[0].ToString());
                this.name = characteristics[1].ToString();
                this.maxLevel = Convert.ToInt32(characteristics[2].ToString());
                this.plusLimit = Convert.ToInt32(characteristics[3].ToString());
                this.maxLimit = Convert.ToInt32(characteristics[4].ToString());
                this.plusPeople = Convert.ToInt32(characteristics[5].ToString());
                this.maxPeople = Convert.ToInt32(characteristics[6].ToString());
                this.cost = Convert.ToInt32(characteristics[7].ToString());
                this.limit=Convert.ToInt32(characteristics[8].ToString());
                this.comePeople=Convert.ToInt32(characteristics[9].ToString());
            }
        }
        public bool UpgradeBuild(ref int coins)//update the Houses and get the results
        {
            if (coins < (cost * level))
                return false;//not enough coins to upgrade 

            if (!CanBeUpgrade())
                return false;//barracks is already at the maximum level

            if ((limit + plusLimit) > maxLimit)
                return false;//population limit is already maximal

            if ((comePeople + plusPeople) > maxPeople)
                return false;//level come people is already maximal

            coins = coins - (cost * level);
            level++;
            limit = limit + plusLimit;
            comePeople = comePeople + plusPeople;
            return true;

        }

        public int UpdatePeople(int peopleInCastle) => Math.Min((peopleInCastle + comePeople), limit);
       
        public bool CanBeUpgrade()
        {
            if ((level + 1) <= maxLevel)
                return true;
            else
                return false;//max level, not can be upgrade
        }
        public string GetName() => name;
        
        public int GetId() => id;
        
        public int GetCost()
        {
            if (CanBeUpgrade())
                return cost * level;
            else
                return 0;//max level, not can be upgrade
        }
        public int GetLevel() => level;
       
        public int GetMaxLevel() => maxLevel;
        
        public double GetLimit() => limit;
        
        public int GetPlusLimit()
        {
            if (CanBeUpgrade())
                return limit+plusLimit;
            else
                return 0;//max level, not can be upgrade
        }
        public double GetComePeople() => comePeople;
        
        public int GetPlusComePeople()
        {
            if (CanBeUpgrade())
                return comePeople+plusPeople;
            else
                return 0;//max level, not can be upgrade
        }
        public List<object> GetDetailsUpgrade()
        {
            List<object> details = new List<object>();
            details.Add(GetLimit());
            details.Add(GetPlusLimit());
            details.Add(GetComePeople());
            details.Add(GetPlusComePeople());
            return details;
        }
    }
}
