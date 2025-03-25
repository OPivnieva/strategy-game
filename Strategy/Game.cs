using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Strategy
{
    internal class Game
    {
        private List<Character> players = new List<Character>();//array of players
        private List<Character> bots = new List<Character>();//array of bots

        Random rnd = new Random();
        int mapsSize = 0;//max x and y
        int step = 0;//distance for castles
        public Game(int size)
        {
            mapsSize = size;
            if (size > 15)
                step = 3;
            else step = 2;
        }
        public void StartGame(int number)
        {
            CreateCharacters(number);
            MapManager.TakePlayer(players[0]);
        }

        private void CreateCharacters(int index)
        {
            Character player = new Player("Player", 0);
            players.Add(player);
            int[] coordinate = player.castle.GetCoordinates();
            MapManager.AddOrMoveOnMap(coordinate[0], coordinate[1], "CastlePlayer" + (player.id + 1), player.name + "'s castle", false);

            int numberOfEnemies;
            if (index == 0)//random
            {

                numberOfEnemies = rnd.Next(3, 6);//right border of Random is not included in the interval
            }
            else//number
            {
                numberOfEnemies = index;
            }
            int indexBots = 0;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                if (rnd.Next(1, 101) < 50)
                {
                    Character botBuilder = new BotBuilder("Bot "+(indexBots+1), indexBots);
                    bots.Add(botBuilder);
                    coordinate = botBuilder.castle.GetCoordinates();
                    MapManager.AddOrMoveOnMap(coordinate[0], coordinate[1], "CastleBot" + (indexBots + 1), botBuilder.name + "'s castle" , false);
                    indexBots++;
                }
                else
                {
                    Character botAggressor = new BotAggressor("Bot " + (indexBots + 1), indexBots);
                    bots.Add(botAggressor);
                    coordinate = botAggressor.castle.GetCoordinates();
                    MapManager.AddOrMoveOnMap(coordinate[0], coordinate[1], "CastleBot" + (indexBots + 1), botAggressor.name + "'s castle", false);
                    indexBots++;
                }
            }

        }
        public int[] CreateOnMap()//create coordinates castle
        {
            int[] coordinates = new int[2];
            do
            {
                coordinates = GetUniqueCoordinates(mapsSize);
            }
            while (CoordinatesIsOccupied(coordinates));

            return coordinates;
        }

        private int[] GetUniqueCoordinates(int max)//make new coordinates
        {
            int[] coordinates = new int[2];
            coordinates[0] = rnd.Next(0, max);
            coordinates[1] = rnd.Next(0, max);
            return coordinates;
        }

        private bool CoordinatesIsOccupied(int[] coordinates) //check if the given coordinates match the coordinates of other players or bots
        {
            foreach (var player in players)
            {
                int[] coordinatesCheck = player.castle.GetCoordinates();
                if (coordinatesCheck[0] == coordinates[0] && coordinatesCheck[1] == coordinates[1])//the coordinates are already occupied
                {
                    return true;
                }
                int distance = Math.Abs(coordinatesCheck[0] - coordinates[0]) + Math.Abs(coordinatesCheck[1] - coordinates[1]);
                if (distance <= step * 2)//coordinates is too close
                {
                    return true;
                }

            }
            foreach (var bot in bots)
            {
                int[] coordinatesCheck = bot.castle.GetCoordinates();
                if (coordinatesCheck[0] == coordinates[0] && coordinatesCheck[1] == coordinates[1])//coordinates are already occupied
                {
                    return true;
                }
                int distance = Math.Abs(coordinatesCheck[0] - coordinates[0]) + Math.Abs(coordinatesCheck[1] - coordinates[1]);
                if (distance <= step * 2)//coordinates is too close
                {
                    return true;
                }
            }
            return false;//it's considered unoccupied
        }

        public void Move()
        {
            CheckCastles();
            foreach (var player in players)
            {
                if (player != null)
                {
                    player.Move();
                }
            }

            foreach (var bot in bots)
            {
                if (bot != null && !bot.isDefeated)
                {
                    bot.Move();
                    if (bot.castle.GetReadyUnits().Count > 0)
                    {
                        if (bot.GetType() == typeof(BotBuilder))
                        {
                            if (rnd.Next(1, 101) > 80)//send army or not
                            {
                                PlanAttack(bot, bot.id);
                            }
                        }
                        else if (bot.GetType() == typeof(BotAggressor))
                        {
                            if (rnd.Next(1, 101) < 80)//send army or not
                            {
                                PlanAttack(bot, bot.id);
                            }
                        }
                    }
                }
            }

            players.RemoveAll(a => a.isDefeated);
            bots.RemoveAll(a => a.isDefeated);
        }
        public void PlanAttack(Character bot, int index) //plan attack for bot
        {
            Character enemy = ChooseEnemy(index);
            if (enemy == null)
                return;

            List<Squad> squads = GenerateSquads(bot);
            if (squads.Count == 0)
                return;

            Army newArmy = new Army()
            {
                Squads = squads,
                BackHome = false,
                Steps = 0,
                Name = "ArmyBot"
            };
            newArmy.CountNecessarySteps();

            newArmy.Enemy = enemy;
            if (rnd.Next(1, 101) < 50 && newArmy.Enemy.armies.Count > 0)//army or castle
            {
                var armiesEnemy = newArmy.Enemy.armies
                    .Where(army => !army.BackHome && !army.IsDefeated)
                    .ToList();
                if (armiesEnemy.Count > 0)//army
                {
                    newArmy.Goal = armiesEnemy[rnd.Next(armiesEnemy.Count)];
                }
                else//castle
                {
                    newArmy.Goal = newArmy.Enemy.castle;
                }
            }
            else//castle
            {
                newArmy.Goal = newArmy.Enemy.castle;
            }
            newArmy.CheckCoordinatesGoal();
            bool side = false;
            var coordinates = MapManager.GiveArmyCoordinates(newArmy.XCoordinateGoal, newArmy.YCoordinateGoal, bot.castle.GetCoordinates(), ref side);
            newArmy.XCoordinate = coordinates.Item1;
            newArmy.YCoordinate = coordinates.Item2;
            bot.AddArmy(newArmy);
            MapManager.AddOrMoveOnMap(newArmy.XCoordinate, newArmy.YCoordinate, newArmy.Name + (index + 1), bot.name + "'s army", side);
            MapManager.MessageSendArmy(bot.name);
        }

        private Character ChooseEnemy(int index)//for bot
        {
            if (players != null || bots != null)
            {
                int piece = 100/ (bots.Count - 1 + players.Count);
                //bot or player
                if (rnd.Next(1, 101) < piece || bots.Count == 1)//player
                {
                    var availablePlayers = players
                        .Where(p => !p.isDefeated).ToList();//list of undefeated players
                    if (availablePlayers.Count == 0)
                        return null;
                    else
                    {
                        int indexPlayer = rnd.Next(0, availablePlayers.Count);

                        return availablePlayers[indexPlayer];
                    }
                }
                else//bot
                {
                    var availableBots = bots
                        .Where(bot => !bot.isDefeated && bot.id != index).ToList();//list of undefeated bots
                    if (availableBots.Count == 0)
                        return null;
                    else
                    {
                        int indexBot = rnd.Next(0, availableBots.Count);
                        return availableBots[indexBot];
                    }
                }
            }
            else return null;
        }
        private List<Squad> GenerateSquads(Character bot)//for bot
        {
            List<Squad> squads = new List<Squad>();
            for (int i = 0; i < rnd.Next(1, 5); i++)
            {
                Squad squad = bot.castle.GetSquad();
                if (squad != null)
                {
                    squads.Add(squad);
                }
            }
            return squads;
        }

        private void CheckCastles()//check the display of castles
        {
            foreach(Character player in players)
            {
                int[] coordinates = player.castle.GetCoordinates();
                MapManager.AddOrMoveOnMap(coordinates[0], coordinates[1], "CastlePlayer" + (player.id + 1), player.name + "'s castle", false);
            }
            foreach(Character bot in bots)
            {
                int[] coordinates = bot.castle.GetCoordinates();
                MapManager.AddOrMoveOnMap(coordinates[0], coordinates[1], "CastleBot" + (bot.id + 1), bot.name + "'s castle", false);
            }
        }

        public void Fight(Army attackerArmy, Character attackerCharacter, Army defensiveArmy, Character defensiveCharacter, bool defense)
        {
            // army is not empty
            bool isUnitAttacker = true;
            bool isUnitDefensive = true;
            //remove empty squads
            attackerArmy.Squads.RemoveAll(squad => squad.CountUnit <= 0);
            defensiveArmy.Squads.RemoveAll(squad => squad.CountUnit <= 0);
            if (!IsUnit(attackerArmy.Squads, ref isUnitAttacker) || !IsUnit(defensiveArmy.Squads, ref isUnitDefensive))//check that the armies aren't empty
                return;

            int[] initiativeAttacker = MakeQueue(attackerArmy.Squads);
            int[] initiativeDefensive;
            int bonusAttackAttacker = 0;
            int bonusAttackDefensive = 0;
            int bonusDefenseAttacker = 0;//always 0
            int bonusDefenseDefensive = 0;
            if (defense)
            {
                bonusAttackAttacker = attackerCharacter.GetAttackCampaign();
                bonusAttackDefensive = defensiveCharacter.GetAttackDefense();
                bonusDefenseDefensive = defensiveCharacter.GetDefenseBonus();
            }
            else
            {
                bonusAttackAttacker = attackerCharacter.GetAttackCampaign();
                bonusAttackDefensive = defensiveCharacter.GetAttackCampaign();

            }
            int indexAttacker = 0;//index for array initiative
            int indexDefensive = -1;//-1 because need to check 

            while (isUnitAttacker && isUnitDefensive)
            {
                Attack(attackerArmy.Squads, initiativeAttacker, indexAttacker, defensiveArmy.Squads, bonusAttackAttacker, bonusDefenseAttacker, bonusAttackDefensive, bonusDefenseDefensive);
                attackerArmy.Squads.RemoveAll(squad => squad.CountUnit <= 0);
                defensiveArmy.Squads.RemoveAll(squad => squad.CountUnit <= 0);
                if (!IsUnit(attackerArmy.Squads, ref isUnitAttacker) || !IsUnit(defensiveArmy.Squads, ref isUnitDefensive)) //checking that it isn't empty 
                    break;                                                                                                    
                //second attack
                initiativeDefensive = MakeQueue(defensiveArmy.Squads);
                indexDefensive = Index(initiativeDefensive, indexDefensive);
                Attack(defensiveArmy.Squads, initiativeDefensive, indexDefensive, attackerArmy.Squads, bonusAttackAttacker, bonusDefenseAttacker, bonusAttackDefensive, bonusDefenseDefensive);
                attackerArmy.Squads.RemoveAll(squad => squad.CountUnit <= 0);
                defensiveArmy.Squads.RemoveAll(squad => squad.CountUnit <= 0);
                if (!IsUnit(attackerArmy.Squads, ref isUnitAttacker) || !IsUnit(defensiveArmy.Squads, ref isUnitDefensive)) //checking that it isn't empty
                    break;
                initiativeAttacker = MakeQueue(attackerArmy.Squads);
                indexAttacker = Index(initiativeAttacker, indexAttacker);
            }
            //something with empty army doing 
            if (!isUnitAttacker)// attacker lost
            {
                attackerArmy.IsDefeated = true;

                if (defense)//castle
                {
                    MapManager.MessageDefendCastle(defensiveCharacter.name, attackerCharacter.name);
                    //army passed by reference, no need to increas

                }
                else
                {
                    defensiveArmy.CountNecessarySteps();
                    MapManager.MessageDefeatArmy(defensiveCharacter.name, attackerCharacter.name);
                    MapManager.AddOrMoveOnMap(defensiveArmy.XCoordinate, defensiveArmy.YCoordinate, defensiveArmy.Name + (defensiveCharacter.id + 1), defensiveCharacter.name + "'s army", false);
                }
            }
            else if (!isUnitDefensive)//defensiver lost
            {
                Victory(defense, attackerCharacter, attackerArmy, defensiveCharacter, defensiveArmy);
                MapManager.AddOrMoveOnMap(attackerArmy.XCoordinate, attackerArmy.YCoordinate, attackerArmy.Name + (attackerCharacter.id + 1), attackerCharacter.name + "'s army", false);

            }
        }

        private void Attack(List<Squad> armyOne, int[] initiativeOne, int indexOne, List<Squad> armyTwo, int bonusAttackOne, int bonusDefenseOne, int bonusAttackTwo, int bonusDefenseTwo)
        {
            int enemy = FindEnemy(armyTwo);
            
            if (enemy > -1)
            {
                int countAttack = armyOne[initiativeOne[indexOne]].CountUnit;
                int countDefensive = armyTwo[enemy].CountUnit;
                double attackPointsOne = armyOne[initiativeOne[indexOne]].CountUnit * (armyOne[initiativeOne[indexOne]].UnitInSquad.Attack + bonusAttackOne);
                double defensePointsOne = armyOne[initiativeOne[indexOne]].CountUnit * (armyOne[initiativeOne[indexOne]].UnitInSquad.Protection + bonusDefenseOne);
                double defensePointsTwo = armyTwo[enemy].CountUnit * (armyTwo[enemy].UnitInSquad.Protection + bonusDefenseTwo);
                double squaLossesPoints = (defensePointsTwo - attackPointsOne) / (armyTwo[enemy].UnitInSquad.Protection + bonusDefenseTwo);
                armyTwo[enemy].CountUnit = (int)Math.Ceiling(squaLossesPoints);
                if (armyTwo[enemy].CountUnit < 0)
                {
                    armyTwo[enemy].CountUnit = 0;
                }
                else if (armyTwo[enemy].CountUnit > 0 && !armyOne[initiativeOne[indexOne]].UnitInSquad.LongDistance)
                {
                    double counterattack = armyTwo[enemy].CountUnit * (armyTwo[enemy].UnitInSquad.Attack + bonusAttackTwo) * 0.75;
                    int counterattackPoint = (int)Math.Round(counterattack);
                    squaLossesPoints = (defensePointsOne - counterattackPoint) / (armyOne[initiativeOne[indexOne]].UnitInSquad.Protection + bonusDefenseOne);
                    armyOne[initiativeOne[indexOne]].CountUnit  = (int)Math.Ceiling(squaLossesPoints);
                    if (armyOne[initiativeOne[indexOne]].CountUnit < 0)
                    {
                        armyOne[initiativeOne[indexOne]].CountUnit = 0;
                    }
                }
                if(countAttack== armyOne[initiativeOne[indexOne]].CountUnit & countDefensive== armyTwo[enemy].CountUnit)
                {
                    armyOne[initiativeOne[indexOne]].CountUnit--;
                    if (armyOne[initiativeOne[indexOne]].CountUnit < 0)
                    {
                        armyOne[initiativeOne[indexOne]].CountUnit = 0;

                    }
                }
            }
        }
        private int[] MakeQueue(List<Squad> army)
        {
            int[] unitIndexesByInitiative = new int[army.Count];
            
            for (int i = 0; i < unitIndexesByInitiative.Length; i++)// filling the index array 
            {
                unitIndexesByInitiative[i] = i;
            }

            // sorting indexes by initiative
            Array.Sort(unitIndexesByInitiative, (a, b) => army[b].UnitInSquad.SpeedAndInitiative.CompareTo(army[b].UnitInSquad.SpeedAndInitiative));
            
            return unitIndexesByInitiative;

        }
        private int FindEnemy(List<Squad> army) //find in army weakest squad 
        {
            int enemy = -1;
            int smallerEnemy = int.MaxValue; ;
            for (int i = 0; i < army.Count; i++)
            {
                if (smallerEnemy > army[i].CountUnit & army[i].CountUnit > 0)
                {
                    smallerEnemy = army[i].CountUnit;
                    enemy = i;
                }
            }
            return enemy;//index in array army
        }
       
        private int Index(int[] initiative, int index) //taking next index in queue
        {
            index = (index + 1) % initiative.Length;
            return index;
        }
        
        private bool IsUnit(List<Squad> army, ref bool notEmpty)//array army isn't empty
        {
            if (army.Count > 0)
            {
                for (int i = 0; i < army.Count; i++)
                {
                    if (army[i].CountUnit > 0)
                    {
                        notEmpty = true;
                        return true;
                    }
                }

                notEmpty = false;
                return false;
            }
            else
            {
               notEmpty=false;
                return false;
            }
        }
        
        public void ReturnToCastle(Character character, Army army)//return army in castle
        {
            character.SendToCastle(army);
            MapManager.MessageBackToCastle(character.name);
        }

        public (object, Character) GetEnemy(int x, int y, Character walking)//check and get enemy and goal
        {
            object goal=null;
            Character enemy = null;
            foreach (Character player in players)
            {
                if (player == null | player == walking)
                    continue;
                goal = IsGoal(x, y, player);
                if (goal != null)
                {
                    enemy = player;
                    break;
                }
            }
            if (goal == null)
            {
                foreach (Character bot in bots)
                {
                    if(bot == null) 
                        continue;
                    goal=IsGoal(x, y, bot);
                    if (goal != null)
                    {
                        enemy = bot;
                        break;
                    }
                }
            }
            return (goal, enemy);
        }

        private object IsGoal(int x, int y, Character character)//check goal
        {
            int[] coordinates = character.castle.GetCoordinates();
            if (x == coordinates[0] & y == coordinates[1])//castle
                return character.castle;

            foreach (Army army in character.armies)
            {
                if (army == null)
                    continue;
                if (army.XCoordinate == x && army.YCoordinate == y)//army
                {
                    if (!army.BackHome)
                        return army;
                    else
                        MessageBox.Show("You can't attack this army! ", "Attention", MessageBoxButtons.OK);
                }
            }
            return null;
        }

        public void Victory(bool defense, Character winningCharacter, Army winningArmy, Character losingCharacter, Army losingArmy )
        {
            if (defense)//castle
            {
                MapManager.MessageDestroyCastle(winningCharacter.name, losingCharacter.name);
                int[] coordinates = losingCharacter.castle.GetCoordinates();
                MapManager.AddOrMoveOnMap(coordinates[0], coordinates[1], winningArmy.Name + (winningCharacter.id + 1), winningCharacter.name + "'s army", false);
                if (players.Remove(losingCharacter))//the player lost
                {
                    GameOver(false);

                }
                else//bot lost
                {
                    winningArmy.Loot = losingCharacter.castle.GetCoins() / 2;
                    losingCharacter.HandleDefeat();
                    losingCharacter.isDefeated = true;
                    winningArmy.CountNecessarySteps();
                    ReturnToCastle(winningCharacter, winningArmy);
                    //check enemy for player 
                    if (bots.Where(b => !b.isDefeated).ToList().Count == 0)
                    {
                        GameOver(true);
                    }
                }
            }
            else//not castle
            {
                MapManager.MessageDefeatArmy(winningCharacter.name, losingCharacter.name);
                MapManager.ClearCell(losingArmy.XCoordinate, losingArmy.YCoordinate);
                losingArmy.IsDefeated = true;
                ReturnToCastle(winningCharacter, winningArmy);
            }
        }

        private void GameOver(bool victory)
        {
            if (victory)
            {
                MessageBox.Show("You won!", "Congratulations", MessageBoxButtons.OK);
                Application.Exit();
            }
            else
            {
                MessageBox.Show("You lost!", "Game over", MessageBoxButtons.OK);
                Application.Exit();

            }
        }
    }

     internal static class GameOn
    {
        private static Game gameInstance;

        public static void InitializeGame(Game game)
        {
            gameInstance = game;
        }
        private static void MessageError()
        {
            MessageBox.Show("Error: Game not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
        public static void Move()
        {
            if (gameInstance != null)
            {
                gameInstance.Move();
            }
            else
            {
                MessageError();
            }
        }

        public static void Fight(Army attackerArmy,  Character attackerCharacter, Army defensiveArmy, Character defensiveCharacter, bool defense)
        {
            if (gameInstance != null)
            {
                gameInstance.Fight(attackerArmy, attackerCharacter, defensiveArmy, defensiveCharacter, defense);
                
            }
            else
            {
                MessageError();
            }
        }
        public static int[] CreateOnMap()
        {
            if (gameInstance != null)
            {
                return gameInstance.CreateOnMap();

            }
            else
            {
                MessageError();
                int[] a = new int[] {0};
                return a;
            }
        }
        public static (object, Character) GetEnemy(int x, int y, Character player)
        {
            if (gameInstance != null)
            {
                return gameInstance.GetEnemy(x,y, player);
            }
            else
            {
                MessageError();
                return (null,null);
            }
        }

        public static void Victory(bool defense, Character winningCharacter, Army winningArmy, Character losingCharacter, Army losingArmy)
        {
            if (gameInstance != null)
            {
               gameInstance.Victory(defense, winningCharacter, winningArmy, losingCharacter, losingArmy);
            }
            else
            {
                MessageError();
            }
        }
        public static void ReturnToCastle(Character character, Army army)
        {
            if (gameInstance != null)
            {
                gameInstance.ReturnToCastle(character, army);
            }
            else
            {
                MessageError();
            }
        }


    }

}
