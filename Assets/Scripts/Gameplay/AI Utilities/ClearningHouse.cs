using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilAI
{
    public class ClearningHouse
    {
        public static bool EnemiesAvailable(UtilityBrain a_brain)
        {
            foreach (Controller c in a_brain.m_controller.getGameManager.GetControllers())
            {
                if(c != a_brain.m_controller)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CreepsAvailable(UtilityBrain a_brain)
        {
            return (a_brain.m_controller.getGameManager.GetCreepLayers().Count > 0);
        }

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

        public static float MilitaryStrenght(UtilityBrain a_brain, bool a_considerEnemies, bool a_considerCreeps)
        {
            // Calculate this together with the strenght of the enemy to get a better result when introduction enemies
            float averageStrenght = 0;

            if (a_considerCreeps)
            {
                int count = 0;

                foreach (CreepLayer c in a_brain.m_controller.getGameManager.GetCreepLayers())
                {
                    averageStrenght += c.getStrenght;
                    count++;
                }

                if (count > 0)
                {
                    averageStrenght /= count;
                }
            }

            if (a_considerEnemies)
            {
                // Get weakest opponent

                float enemyStrenght = 0;

                foreach (Controller c in a_brain.m_controller.getGameManager.GetControllers())
                {
                    if (c == a_brain.m_controller)
                        continue;

                    if(enemyStrenght == 0 || c.getMilitaryStrenght < enemyStrenght)
                    {
                        enemyStrenght = c.getMilitaryStrenght;
                    }

                }
                averageStrenght += enemyStrenght;
            }


            foreach(Buildings b in a_brain.m_controller.getBuildings)
            {
                averageStrenght += b.m_building.GetStrenght();
            }

            float strenght = a_brain.m_controller.getMilitaryStrenght - averageStrenght;

            return strenght;
        }

    }
}
