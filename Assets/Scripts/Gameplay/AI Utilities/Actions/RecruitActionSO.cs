using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "BuildAction", menuName = "AI/Utilities/Action/BuildAction", order = 1)]
    public class RecruitActionSO : ActionSO
    {
        public UnitSO m_unit;

        public override float CalcWeight(UtilityBrain a_brain)
        {
            // Check if barrack available
            // Check if resources for unit available
            return base.CalcWeight(a_brain);
        }

        public override void Action(UtilityBrain a_brain)
        {
            //Fetch Building to recruit
        }

        private void OnValidate()
        {
            m_actionType = ActionType.Recruit;
        }
    }
}