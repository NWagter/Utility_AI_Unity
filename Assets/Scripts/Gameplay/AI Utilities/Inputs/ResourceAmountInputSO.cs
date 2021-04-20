using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "ResourceInput", menuName = "AI/Utilities/Input/Resource/AmountInput", order = 1)]
    public class ResourceAmountInputSO : InputSO
    {
        public ResourceType m_type;
        public override float CurveCal(UtilityBrain a_brain)
        {
            float input = a_brain.m_resourceManager.GetResource(m_type) / m_max;
            float result = ResponseCurveCalculator.Calculate(Mathf.Clamp(input, 0, 1), m_curveRules, m_curveType);

            Debug.Log(m_type.ToString() + " Amount input : " + Mathf.Clamp(input, 0, 1) + " Result" + result);
            return result;
        }
    }
}
