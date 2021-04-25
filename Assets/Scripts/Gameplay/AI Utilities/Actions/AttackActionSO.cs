using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    public enum AttackType
    {
        Creep,
        Enemy
    }

    [CreateAssetMenu(fileName = "AttackAction", menuName = "AI/Utilities/Action/AttackAction", order = 1)]
    public class AttackActionSO : ActionSO
    {
        public AttackType m_attackType;

        public override float CalcWeight(UtilityBrain a_brain)
        {
            if(m_attackType == AttackType.Creep && !ClearningHouse.CreepsAvailable(a_brain))
            {
                return -1;
            }
            else if(m_attackType == AttackType.Enemy && !ClearningHouse.EnemiesAvailable(a_brain))
            {
                return -1;
            }

            return base.CalcWeight(a_brain);
        }

        public override void Action(UtilityBrain a_brain)
        {
            //Attack

            a_brain.m_controller.Attack(m_attackType);
        }

        private void OnValidate()
        {
            m_actionType = ActionType.Attack;
        }
    }
}