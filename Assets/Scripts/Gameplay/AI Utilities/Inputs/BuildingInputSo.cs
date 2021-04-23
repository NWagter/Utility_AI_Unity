using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "BuildingInput", menuName = "AI/Utilities/Input/BuildingInput", order = 1)]
    public class BuildingInputSO : InputSO
    {
        public BuildingSO m_building;

        public override float CurveCal(UtilityBrain a_brain)
        {
            //Check the amount of these buildings for this controller
            float input = 0;
            return ResponseCurveCalculator.Calculate(input, m_curveRules, m_curveType);
        }
    }
}
