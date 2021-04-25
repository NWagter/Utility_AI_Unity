using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "MilitaryStrenghtInput", menuName = "AI/Utilities/Input/Military/MilitaryStrenght", order = 1)]
    public class MilitaryStrenghtSO : InputSO
    {
        public bool m_considerEnemies = false;
        public bool m_considerCreeps = true;

        public override float CurveCal(UtilityBrain a_brain)
        {
            float strenght = ClearningHouse.MilitaryStrenght(a_brain, m_considerEnemies, m_considerCreeps) / m_max;

            float input = Mathf.Clamp(strenght,0,1);

            if (m_enableDebug)
            {
                Debug.Log(" | " + a_brain.m_controller + " | Strenght without clearning House : " + a_brain.m_controller.getMilitaryStrenght);
                Debug.Log(" | " + a_brain.m_controller + " | Strenght : " + strenght + " result : " + input);
            }

            return ResponseCurveCalculator.Calculate(input, m_curveRules, m_curveType);
        }
    }
}
