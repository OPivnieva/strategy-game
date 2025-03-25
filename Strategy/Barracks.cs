using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Strategy
{
    internal class Barracks : IBuilding 
    {
        int id;
        string name;
        int maxLevel;
        int cost;//coins × current level value
        int level = 1;
        List<Unit> units = new List<Unit>();//the list of all units
        List<(string, int)> levelBonuses = new List<(string, int)>();//unit and how much is a minus to the preparation speed, index is level barracks when gets a bonus
        List<(Squad, int)> trainingUnits = new List<(Squad, int)>();//squad and time preparation
        List<Squad> readyUnits = new List<Squad>();

        public void AddСharacteristics(object[] characteristics)
        {
            if (characteristics != null)
            {
                this.id = Convert.ToInt32(characteristics[0].ToString());
                this.name = characteristics[1].ToString();
                this.maxLevel = Convert.ToInt32(characteristics[2].ToString());
                string jsonString = characteristics[3].ToString();
                this.levelBonuses = JsonConvert.DeserializeObject<List<(string, int)>>(jsonString);
                this.cost = Convert.ToInt32(characteristics[4].ToString());
            }
            units = Units();
        }
        private List<Unit> Units()//reading Units.json, creating list units
        {
            var unit = JsonConvert.DeserializeObject< List<Unit>>(Properties.Resources.Units);
            return unit;
        }

        public bool UpgradeBuild(ref int coins)//update the Barracks and get the results
        {
            if (coins < (cost * level))
                return false;//not enough coins to upgrade 

            if (!CanBeUpgrade()) return false;//barracks is already at the maximum level

            var (unitName, speedBonus) = levelBonuses[level];
            if (!string.IsNullOrEmpty(unitName))
            {
                foreach (Unit unit in units)
                {
                    if (unitName == unit.Name)
                    {
                        unit.TimePreparation -= speedBonus;//update minus
                    }
                }
            }
            coins = coins - (cost * level);
            level++;
            return true;
        }

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
        
       
        public string GetUpgrade()//get info about upgrade
        {
            string upgrade = "";
            if (CanBeUpgrade())
            {
                var (unitName, speedBonus) = levelBonuses[level];
                if (!string.IsNullOrEmpty(unitName))//low speed
                {
                    upgrade = unitName + "'s training speed -" + speedBonus;
                    return upgrade;
                }
                else//new units
                {
                    foreach (Unit unit in units)
                    {
                        if (unit.Bilding == "Barracks" && unit.LevelBilding == (level + 1))
                        {
                            upgrade = unit.Name;
                            break;
                        }
                    }
                    return upgrade;
                }
            }
            else
                return upgrade;//max level, not can be upgrade
        }

        public List<object> GetDetailsUpgrade()//get details about upgrade and training
        {
            List<object> details = new List<object>
            {
                GetUpgrade(),
                units.Where(u => u.LevelBilding <= level).ToList(), //available units
                readyUnits,
                trainingUnits
            };
            return details;
        }
        public List<Squad> GetReadyUnits()
        {
            return readyUnits;
        }

        public Army GetDefenseArmy()
        {
            Army armyCastle = new Army
            {
                Squads = readyUnits.Where(s => s.CountUnit > 0).ToList()//passed by reference, no need to reduce
            };
            return armyCastle;
        }

        public void TrainUnits(string nameUnit, int countUnits)//train a squad of units
        {
            var unit = units.FirstOrDefault(u => u.Name == nameUnit);
            if (unit == null) return;
            Squad squad = new Squad
            {
                UnitInSquad = unit,
                CountUnit = countUnits
            };
            trainingUnits.Add((squad, unit.TimePreparation));
        }
        
        public void UpdateUnitsTraining()//check train units if time is over
        {
            for(int i = 0; i < trainingUnits.Count; i++)
            {
                var (squad, time)  = trainingUnits[i];
                time--;
                if (time > 0)
                {
                    trainingUnits[i] = (squad, time);
                }
                else
                {
                    var checkSquad = readyUnits.FirstOrDefault(s => s.UnitInSquad.Name == squad.UnitInSquad.Name);
                    if (checkSquad != null)
                        checkSquad.CountUnit += squad.CountUnit;//passed by reference
                    else
                        readyUnits.Add(squad);
                    trainingUnits.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ReduceReadyUnits(string name, int number)
        {
            var squad = readyUnits.FirstOrDefault(s => s.UnitInSquad.Name == name);
            if (squad != null)
            {
                squad.CountUnit -= number;//passed by reference
            }

        }
        public void IncreasReadyUnits(List<Squad> returnUnits)
        {
            foreach(Squad squad in returnUnits)
            {
                var ready = readyUnits.FirstOrDefault(s => s.UnitInSquad == squad.UnitInSquad);
                if (ready != null)
                {
                    ready.CountUnit += squad.CountUnit;//passed by reference
                }
            }
        }
        public bool NeedProtection()//check if that needs protection
        {
            return readyUnits.Any(s => s.CountUnit < 50);
        }

    }
}
