﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    public class HealthBar
    {
        public int startingHP { get; set; }
        public int currentHP { get; set; }
        public int startingBars { get; set; }

        const int HEALTHBARMAX = 20;
        double dmgPerBar;
        string healthbarString = "";

        public string BarConsoleUpdate(int startingHP, int currentHP)
        {
            //get actual/real hp per bar 
            dmgPerBar = startingHP * 0.05;

            healthbarString = "[";       // start/reset

            double barDmgCounter = dmgPerBar; //to increase every iteration

            for (int i = 0; i < HEALTHBARMAX; i++)
            {
                if (i == 0 && currentHP > 0)    //initial check to assure 1 bar when true hp goes below dmgPerBar (alive monster/player must have at least 1 bar)
                    healthbarString += "|";
                else if (currentHP >= barDmgCounter)
                    healthbarString += "|";
                else
                    healthbarString += " ";

                barDmgCounter += dmgPerBar;
            }

            healthbarString += "]";

            return healthbarString;
        }


    }
}
