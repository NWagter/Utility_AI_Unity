using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "ResourceInput", menuName = "AI/Utilities/Input/Resource/GainInput", order = 1)]
    public class ResourceGainInputSO : InputSO
    {
        public ResourceType m_type;
        public override float CurveCal(UtilityBrain a_brain)
        {
            float input = a_brain.m_resourceManager.GetResourceGain(m_type) / m_max;

            float result = ResponseCurveCalculator.Calculate(Mathf.Clamp(input, 0, 1), m_curveRules, m_curveType);

            if(m_enableDebug)
                Debug.Log("| " + name + " | " + m_type.ToString() + " Resource Gain : " + Mathf.Clamp(input, 0, 1) + " Result " + result);
            return result;

        }
    }
}
