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

            foreach(Buildings b in a_brain.m_controller.GetBuildings)
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

    }
}
