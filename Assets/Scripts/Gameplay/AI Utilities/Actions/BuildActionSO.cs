using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "BuildAction", menuName = "AI/Utilities/Action/BuildAction", order = 1)]
    public class BuildActionSO : ActionSO
    {
        public BuildingSO m_building;

        public override float CalcWeight(UtilityBrain a_brain)
        {
            if(!a_brain.m_resourceManager.CanSpend(m_building.m_cost)) // don't have resources don't consider
            {
                return -1;
            }

            return base.CalcWeight(a_brain);
        }

        public override void Action(UtilityBrain a_brain)
        {
            a_brain.m_controller.Build(m_building);
        }

        private void OnValidate()
        {
            m_actionType = ActionType.Building;
        }
    }
}