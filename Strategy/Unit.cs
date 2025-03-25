using System.Collections.Generic;


namespace Strategy
{
    class Unit
    {
        public string Name { get; set; }
        public int Protection { get; set; }
        public int Attack { get; set; }
        public int SpeedAndInitiative { get; set; }
        public int TimePreparation { get; set; }
        public int PriceCoins { get; set; }
        public int PricePeople { get; set; }
        public string Bilding { get; set; }
        public int LevelBilding { get; set; }
        public bool LongDistance { get; set; }//for fight, if true then this unit can't be counterattacked
    }

     class Squad
    {
        public Unit UnitInSquad { get; set; }
        public int CountUnit { get; set; }

    }
     class Army
    {
        public List<Squad> Squads { get; set; }
        public int XCoordinate { get; set; }    
        public int YCoordinate { get; set; }
        public Character Enemy {  get; set; }
        public int XCoordinateGoal { get; set; }
        public int YCoordinateGoal { get; set; }
        public object Goal { get; set; }
        public int NecessarySteps { get; set; }//how many steps are necessary for one cell
        public int Steps { get; set; }//how many steps were made for one cell
        public bool BackHome { get;set; }
        public string Name { get; set; }
        public bool IsDefeated { get; set; }
        public int Loot { get; set; }
        public bool CheckCoordinatesGoal()
        {
            if(Enemy!=null)
            if (Enemy.isDefeated)
                return false;

            if ( Goal == null)
                return false;//goal is missing

            if (Goal is Army)
            {
                if (((Army)Goal).BackHome) 
                    return false;//goal is missing
                var coordinates = ((Army)Goal).GetCoordinates();
                XCoordinateGoal = coordinates.Item1;
                YCoordinateGoal = coordinates.Item2;

            }
            else if (Goal is Castle)
            {
                int[] coordinates = ((Castle)Goal).GetCoordinates();
                XCoordinateGoal = coordinates[0];
                YCoordinateGoal = coordinates[1];
            }
            return true;

        }
        public Army GetEnemyArmy()
        {
            if (Enemy == null)
                return null;

            if (Goal is Army)
            {
                return (Army)Goal;
            }
            else if (Goal is Castle)
            {
                return ((Castle)Goal).GetDefenseArmy();
            }
            else return null;

        }
        public bool AttacksCastle()
        {
            return Goal is Castle;
        }
        public (int, int) GetCoordinates()
        {
            return (XCoordinate, YCoordinate);
        }
        public void UpdateGoalCoordinates( int x, int y)
        {
            XCoordinateGoal= x; 
            YCoordinateGoal= y; 
        }
        public void CountNecessarySteps()//how many steps are for one cell necessary
        {
            int step = int.MaxValue;
            foreach (Squad unit in this.Squads)
            {
                if (step > unit.UnitInSquad.SpeedAndInitiative)
                    step = unit.UnitInSquad.SpeedAndInitiative;
            }
            NecessarySteps=step;
        }
    }

}
