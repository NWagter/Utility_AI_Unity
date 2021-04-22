using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "RecruitAction", menuName = "AI/Utilities/Action/RecruitAction", order = 1)]
    public class RecruitActionSO : ActionSO
    {
        public UnitSO m_unit;

        public override float CalcWeight(UtilityBrain a_brain)
        {

            if(ClearningHouse.UnitProductionQueueAvailable(a_brain) <= 0 || !a_brain.m_resourceManager.CanSpend(m_unit.m_cost))
            {
                return -1;
            }

            return base.CalcWeight(a_brain);
        }

        public override void Action(UtilityBrain a_brain)
        {
            //Fetch Building to recruit
            Debug.Log(" !Important | Should Recruit a " + m_unit.m_name + " Right about now! | ");
            a_brain.m_controller.RecruitUnit(m_unit);
        }

        private void OnValidate()
        {
            m_actionType = ActionType.Recruit;
        }
    }
}