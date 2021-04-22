using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "UnitProductionInput", menuName = "AI/Utilities/Input/UnitProduction/Queue", order = 1)]
    public class UnitProductionQueueInputSO : InputSO
    {
        public override float CurveCal(UtilityBrain a_brain)
        {
            //Get the amount of idle trainingCamps
            float available = ClearningHouse.UnitProductionQueueAvailable(a_brain) / m_max;

            float input = Mathf.Clamp(available, 0, 1);

            return ResponseCurveCalculator.Calculate(input, m_curveRules, m_curveType);
        }
    }
}
