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
                    return LinearCurve(inValue, a_rules);
                case CurveType.Quadratic:
                    return QuadraticCurve(inValue, a_rules);
                case CurveType.Logistic:
                    return LogisticCurve(inValue, a_rules);
                case CurveType.Logit:
                    return LogitCurve(inValue, a_rules);
            }

            return 0;
        }

        private static float LinearCurve(float a_input, CurveRules a_rules)
        {
            float exponent = Mathf.Pow((a_input - a_rules.c), a_rules.k);
            float result = a_rules.m * exponent + a_rules.b;
            Debug.Log("Linear Result : " + result.ToString());
            return result;
        }
        private static float QuadraticCurve(float a_input, CurveRules a_rules)
        {
            return 0;
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