using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "MilitaryStrenghtInput", menuName = "AI/Utilities/Input/Military/MilitaryStrenght", order = 1)]
    public class MilitaryStrenghtSO : InputSO
    {
        public override float CurveCal(UtilityBrain a_brain)
        {
            float strenght = ClearningHouse.MilitaryStrenght(a_brain) / m_max;
            float input = Mathf.Clamp(strenght,0,1);

            return ResponseCurveCalculator.Calculate(input, m_curveRules, m_curveType);
        }
    }
}
