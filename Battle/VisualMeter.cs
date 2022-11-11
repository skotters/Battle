using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    //currently used for both player and monster health bar AND player MP bar
    public class VisualMeter
    {
        public static string GetFullMeterString(int startingHP, int currentHP)
        {
            const int VISUAL_METER_MAX_BARS = 20;
            double dmgPerBar;
            string healthbarString = "";

            //get actual/real hp per bar 
            dmgPerBar = startingHP * 0.05;

            healthbarString = "[";       // start/reset

            double barDmgCounter = dmgPerBar; //to increase every iteration

            for (int i = 0; i < VISUAL_METER_MAX_BARS; i++)
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
