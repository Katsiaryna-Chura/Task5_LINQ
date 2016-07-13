using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Numerics;

namespace Task5_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            bool isContinue = false;
            do
            {
                menu.ViewMenu();
                int menuItem = menu.SelectMenuItem();
                isContinue = menu.PerformMenuItem(menuItem);
            } while (isContinue);
        }
    }
}
