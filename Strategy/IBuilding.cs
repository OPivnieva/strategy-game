using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal interface IBuilding
    {
        bool UpgradeBuild(ref int coins);//upgrade or return false
        int GetLevel();
        int GetMaxLevel();
        int GetCost();
        string GetName();
        int GetId();
        bool CanBeUpgrade();//check the possibility of updating
        List<object> GetDetailsUpgrade();
    }
}
