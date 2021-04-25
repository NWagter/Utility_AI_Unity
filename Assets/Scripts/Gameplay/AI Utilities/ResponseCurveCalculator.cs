using System;
using UnityEngine;

namespace UtilAI
{
    public class ResponseCurveCalculator
    {
        public static float Calculate(float input, CurveRules a_rules, CurveType a_type)
        {
            float inValue = Mathf.Clamp(input, 0, 1);

            switch (a_type)
            {
                case CurveType.Linear:
                    return Mathf.Clamp(LinearQuadraticCurve(inValue, a_rules), 0,1);
                case CurveType.Quadratic:
                    return Mathf.Clamp(LinearQuadraticCurve(inValue, a_rules), 0, 1);
                case CurveType.Logistic:
                    return Mathf.Clamp(LogisticCurve(inValue, a_rules), 0, 1);
                case CurveType.Logit:
                    return Mathf.Clamp(LogitCurve(inValue, a_rules), 0, 1);
            }

            return 0;
        }
        private static float LinearQuadraticCurve(float a_input, CurveRules a_rules)
        {
            float exponent = Mathf.Pow((a_input - a_rules.c), a_rules.k);
            float result = a_rules.m * exponent + a_rules.b;
            return result;
        }

        private static float LogisticCurve(float a_input, CurveRules a_rules)
        {
            return 0;
        }
        private static float LogitCurve(float a_input, CurveRules a_rules)
        {
            return 0;
        }
    }
}