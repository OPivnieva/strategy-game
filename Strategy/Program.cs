using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strategy
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Start start = new Start();
            if(start.ShowDialog()== System.Windows.Forms.DialogResult.Yes)
            Application.Run(new Map(start.numberOfEnemies, start.numberOfCells));
        }
    }
}
