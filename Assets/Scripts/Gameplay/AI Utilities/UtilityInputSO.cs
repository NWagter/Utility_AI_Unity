using UnityEngine;

namespace UtilAI
{
    public enum CurveType
    {
        Linear,
        Quadratic,
        Logistic,
        Logit
    }

    [System.Serializable]
    public struct CurveRules
    {
        [Tooltip("Slope of the Line")]
        public float m;
        [Tooltip("Exponent change the depth of the curve")]
        public float k;
        [Tooltip("y-intercept")]
        public float b;
        [Tooltip("x-intercept")]
        public float c;



        public CurveRules(float a_m = 1, float a_k = 2, float a_b = 0, float a_c = 1)
        {
            m = a_m;
            k = a_k;
            b = a_b;
            c = a_c;
        }
    }

    public class InputSO : ContentSO
    {

        [Tooltip("Curve Used")]
        public CurveType m_curveType = CurveType.Linear;
        public CurveRules m_curveRules = new CurveRules(1,2,0,1);

        [Tooltip("To Clamp max")]
        public float m_max = 50;

        public virtual float CurveCal(UtilityBrain a_brain)
        {
            return 0;
        }

    }
}