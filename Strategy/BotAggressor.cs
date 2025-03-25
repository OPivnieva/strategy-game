using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class BotAggressor : Character
    {
        Random rnd = new Random();
        public BotAggressor(string nm, int number) : base(nm, number)
        {
            name = nm;
            id = number;
            AddBot();
        }
        void AddBot()
        {
            castle = new Castle();
        }
        public override void Move()
        {
            castle.UpdateResources();

            if (rnd.Next(1, 101) > 80)
            {
                for (int i = 0; i < 4; i++)
                {
                    int build = rnd.Next(1, 4);
                    switch (build)
                    {
                        case 1:
                            castle.UpgradeBuild(1);
                            break;
                        case 2:
                            castle.UpgradeBuild(2);
                            break;
                        case 3:
                            castle.UpgradeBuild(3);
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    int build = rnd.Next(1, 3);
                    switch (build)
                    {
                        case 1:
                            castle.UpgradeBuild(4);
                            break;
                        case 2:
                            castle.UpgradeBuild(5);
                            break;
                    }
                }
            }
            MoveArmies();
            for (int i = 0; i < 5; i++)
            {
                if (castle.NeedProtect())
                {
                    castle.TrainUnits();
                }
                else
                {
                    if (rnd.Next(1, 101) < 50)
                    {
                        castle.TrainUnits();
                    }
                }
            }
        }
    }
}
