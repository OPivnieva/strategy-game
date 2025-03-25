using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Strategy
{
    internal class Castle
    {
        TownHall townHall = new TownHall();
        Houses houses=new Houses();
        Walls walls=new Walls();
        Temple temple=new Temple();
        Barracks barracks=new Barracks();

        List<IBuilding> buildes = new List<IBuilding>();
        Random rnd=new Random();
        int coins = 500;
        int people=100;
        int[] coordinates = new int[2];
        public Castle()
        {
            List<object[]> listBuildes = JsonConvert.DeserializeObject<List<object[]>>(Properties.Resources.Buildes.ToString());
            townHall.AddСharacteristics(listBuildes[0]);
            buildes.Add(townHall);
            houses.AddCharacteristics(listBuildes[1]);
            buildes.Add(houses);
            walls.AddCharacteristics(listBuildes[2]);
            buildes.Add(walls);
            temple.AddCharacteristics(listBuildes[3]);
            buildes.Add(temple);
            barracks.AddСharacteristics(listBuildes[4]);
            buildes.Add(barracks);
            coordinates = GameOn.CreateOnMap();

        }
        
        public void UpdateResources()
        {
            coins=coins + townHall.UpdateCoins(people);
            people = houses.UpdatePeople(people);
            barracks.UpdateUnitsTraining();
        }

        public void ReduceCoins(int sum) => coins -= sum;
        
        public void IncreasCoins(int sum)=>coins += sum;
        
        public void ReducePeople(int number)=>people -= number;
        
        public int[] GetCoordinates() => coordinates;
        
        public int GetCoins() => coins;

        public int GetPeople() => people;

        private IBuilding GetBuild(int id) => buildes.FirstOrDefault(b => b.GetId() == id);
        public bool UpgradeBuild(int id)
        {
            IBuilding build = GetBuild(id);
            if (build != null)
                return build.UpgradeBuild(ref coins);
            else return false;
        }
        public int GetAttackCampaign() => temple.GetCampaign();
       
        public int GetAttackDefense() => temple.GetDefense();
       
        public int GetDefenseBonus() => walls.GetDefense();
        
        public Army GetDefenseArmy() => barracks.GetDefenseArmy();
        
        public int GetLevel(int id)
        {
            IBuilding build = GetBuild(id);
            if (build != null)
                return build.GetLevel();
            else return 0;
        }
        public int GetMaxLevel(int id)
        {
            IBuilding build = GetBuild(id);
            if (build != null)
                return build.GetMaxLevel();
            else
                return 0;
        }
        public int GetCost(int id)
        {
            IBuilding build = GetBuild(id);
            if (build != null)
                return build.GetCost();
            else return 0;
        }
        public bool CanBeUpgrade(int id)
        {
            IBuilding build = GetBuild(id);
            if (build != null)
                return build.CanBeUpgrade();
            return false;
        }
        public List<object> GetDetailsUpgrade(int id)
        {
            IBuilding build = GetBuild(id);
            if (build != null)
                return build.GetDetailsUpgrade();
            else return null;
        }

        public void TrainUnits(string name, int count) => barracks.TrainUnits(name, count);

        public List<Squad> GetReadyUnits() => barracks.GetReadyUnits();

        public void ReduceReadyUnit(string name, int number) => barracks.ReduceReadyUnits(name, number);

        public void TrainUnits()//for bot
        {
            if (people > 1)
            {
                List<object> details = GetDetailsUpgrade(5);
                List<Unit> units = (List<Unit>)details[1];//available units
                int index = rnd.Next(0, units.Count);
                string nameUnit = units[index].Name;
                int countUnit = rnd.Next(1, people);
                if (coins >= (countUnit * units[index].PriceCoins) && people >= (countUnit * units[index].PricePeople))
                {
                    TrainUnits(nameUnit, countUnit);
                    ReducePeople(countUnit * units[index].PricePeople);
                    ReduceCoins(countUnit * units[index].PriceCoins);
                }
            }
        }
        public Squad GetSquad()//for bot
        {
            List<Squad> units = GetReadyUnits();
            int index = rnd.Next(0, units.Count);
            if (units[index].CountUnit == 0)
                return null;//not enough unit

            int countUnit = rnd.Next(1, units[index].CountUnit);
            if (countUnit < 50)
                return null;//not enough unit

            string nameUnit = units[index].UnitInSquad.Name;
            Squad newSquad = new Squad
            {
                UnitInSquad = units[index].UnitInSquad,
                CountUnit = countUnit
            };
            ReduceReadyUnit(nameUnit, countUnit);
            return newSquad;

        }
        public void ReturnUnitsInCastle(List<Squad> units) => barracks.IncreasReadyUnits(units);

        public bool NeedProtect() => barracks.NeedProtection();
        
    }
}
