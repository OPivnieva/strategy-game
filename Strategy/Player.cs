using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class Player : Character
    {
        public Player(string nm, int number):base (nm, number)
        {
            name = nm;
            id = number;
            AddPlayer();
        }
        void AddPlayer()
        {
            castle = new Castle();
        }
        public override void Move()
        {
            castle.UpdateResources();
            MoveArmies();
           
        }
        
    }
}
