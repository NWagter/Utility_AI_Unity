using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{
    public enum ActionType
    {
        Building,
        Recruit,
        Attack
    }

    [System.Serializable]
    public struct Axis
    {
        public InputSO m_input;

        public float GetResult(UtilityBrain a_brain)
        {      
            return Mathf.Clamp(m_input.CurveCal(a_brain), 0,1);
        }
    }

    public class ActionSO : ContentSO
    {
        [Range(1, 256)]
        public float m_weightCoefficient = 1;
        public List<Axis> m_contions = new List<Axis>();

        [HideInInspector]
        public ActionType m_actionType;

        public virtual float CalcWeight(UtilityBrain a_brain)
        {
            if (m_contions.Count <= 0)
                return m_weightCoefficient;

            float value = 0;

            for (int i = 0; i < m_contions.Count; i++)
            {
                if(i == 0)
                {
                    value = m_contions[i].GetResult(a_brain);
                    continue;
                }

                value *= m_contions[i].GetResult(a_brain);
            }

            return value * m_weightCoefficient;
        }

        public virtual void Action(UtilityBrain a_brain)
        {

        }
    }
}