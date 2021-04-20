using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "ResourceInput", menuName = "AI/Utilities/Input/ResourceInput", order = 1)]
    public class ResourceInputSO : InputSO
    {
        public ResourceType a_type;
        public override float CurveCal(UtilityBrain a_brain)
        {
            float input = a_brain.m_resourceManager.GetResource(a_type) / m_max;
            return ResponseCurveCalculator.Calculate(Mathf.Clamp(input, 0, 1), m_curveRules, m_curveType);
        }
    }
}
