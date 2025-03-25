using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal abstract class Character
    {
        public List<Army> armies=new List<Army>();//army and them coordinates
        public Castle castle;
        public string name;
        public int id;
        public bool isDefeated = false;
        public Character(string nm, int number) 
        { 
            name = nm;
            id = number;
        }
        public abstract void Move();
        public  int GetAttackCampaign() => castle.GetAttackCampaign();
       
        public  int GetAttackDefense() => castle.GetAttackDefense();
        

        public  int GetDefenseBonus() => castle.GetDefenseBonus();
        
       
        public void ReturnUnitsInCastle(List<Squad> returnUnits)
        {
            castle.ReturnUnitsInCastle(returnUnits);
        }
        public void AddArmy(Army newArmy)
        {
            armies.Add(newArmy);
        }
        public void MoveArmies()
        {
            if(armies.Count > 0)
            {
                foreach (var army in armies)
                {
                    if (!army.IsDefeated)//army don't lost
                    {
                        bool side = false;//true-left
                        int oldX = 0;
                        int oldY = 0;
                        if (army.CheckCoordinatesGoal())//goal isn't missing
                        {
                            if (army.NecessarySteps <= army.Steps + 1)
                            {
                                army.Steps = 0;
                                //calculate the distances to the goal along the X and Y
                                int deltaX = army.XCoordinateGoal - army.XCoordinate;
                                int deltaY = army.YCoordinateGoal - army.YCoordinate;
                                //determine the direction of movement along the X and Y 
                                int directionX = deltaX > 0 ? 1 : deltaX < 0 ? -1 : 0;
                                int directionY = deltaY > 0 ? 1 : deltaY < 0 ? -1 : 0;
                                oldX = army.XCoordinate;
                                oldY = army.YCoordinate;
                                //move the army one step in the direction of the goal
                                army.XCoordinate += directionX;
                                army.YCoordinate += directionY;
                                if (directionX < 0)
                                {
                                    side = true;
                                }
                            }
                            else
                            {
                                army.Steps++;
                                break;
                            }

                            if (army.XCoordinateGoal == army.XCoordinate && army.YCoordinateGoal == army.YCoordinate)
                            {

                                if (army.BackHome)//if goal is return to castle
                                {
                                    castle.IncreasCoins(army.Loot);
                                    castle.ReturnUnitsInCastle(army.Squads);
                                    army.IsDefeated = true;
                                    MapManager.ClearCell(oldX, oldY);
                                }
                                else if (MapManager.CheckGoal(army.XCoordinateGoal, army.YCoordinateGoal))
                                {
                                    Army armyEnemy = army.GetEnemyArmy();
                                    if (armyEnemy != null && armyEnemy.Squads.Count > 0)
                                    {
                                        MapManager.ClearCell(oldX, oldY);
                                        GameOn.Fight(army, this, armyEnemy, army.Enemy, army.AttacksCastle());

                                    }
                                    else
                                    {
                                        MapManager.ClearCell(oldX, oldY);
                                        GameOn.Victory(army.AttacksCastle(), this, army, army.Enemy, armyEnemy);
                                        MapManager.AddOrMoveOnMap(army.XCoordinate, army.YCoordinate, army.Name + (id + 1), name + "'s army", side);
                                    }
                                }
                            }
                            else//move on map
                            {
                                MapManager.AddOrMoveOnMap(army.XCoordinate, army.YCoordinate, army.Name + (id + 1), name + "'s army", side);
                                MapManager.ClearCell(oldX, oldY);
                            }
                            if (armies.Count == 0)
                                break;
                        }
                        else//goal is missing
                        {
                            MapManager.AddOrMoveOnMap(army.XCoordinate, army.YCoordinate, army.Name + (id + 1), name + "'s army", side);
                            MapManager.MessageNoGoal(name);
                            SendToCastle(army);
                        }
                    }
                }
                armies.RemoveAll(a => a.IsDefeated);
            }

        }
        public void SendToCastle(Army army)
        {
            army.Enemy = null;
            army.Goal = castle;
            int[] xy = castle.GetCoordinates();
            army.UpdateGoalCoordinates(xy[0], xy[1]);
            army.BackHome = true;
        }
        
        public void HandleDefeat()//delete all army and castle
        {
            foreach (Army army in armies)
            {
                MapManager.ClearCell(army.XCoordinate, army.YCoordinate);
            }
            int[] xy = castle.GetCoordinates();
            MapManager.ClearCell(xy[0], xy[1]);
            castle = null;
            armies.Clear();
            
        }
        

    }
}
