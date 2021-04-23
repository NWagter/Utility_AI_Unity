using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilAI
{
    public class ClearningHouse
    {

        public static float UnitProductionQueueAvailable(UtilityBrain a_brain)
        {
            float queue = 0;

            foreach(Buildings b in a_brain.m_controller.getBuildings)
            {
                if(b.m_building.m_buildingType == BuildingType.Military)
                {
                    if(((UnitProductionBuilding)b.m_building).QueueAvailable())
                    {
                        queue++;
                    }
                }
            }

            return queue;
        }

        public static float MilitaryStrenght(UtilityBrain a_brain)
        {
            // Calculate this together with the strenght of the enemy to get a better result when introduction enemies
            float strenght = a_brain.m_controller.getMilitaryStrenght;

            return strenght;
        }

    }
}
