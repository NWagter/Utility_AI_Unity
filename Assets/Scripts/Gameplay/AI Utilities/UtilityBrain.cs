using System.Collections.Generic;
using UnityEngine;

namespace UtilAI
{

    public class UtilityBrain
    {
        private struct PotentialAction
        {
            public ActionSO m_action;
            public float m_weight;

            public PotentialAction(ActionSO a_action, UtilityBrain a_brain)
            {
                m_action = a_action;
                m_weight = a_action.CalcWeight(a_brain);
            }
        }

        private UtilityBrainSO m_brainSO = null;
        public AIController m_controller { get; private set; }
        public ResourceManager m_resourceManager => m_controller.m_resourceManager;
        public Builder m_builder => m_controller.m_builder;

        private bool m_inAction = false;

        private List<ActionSO> m_availableActions;
        private List<PotentialAction> m_potentialActions = new List<PotentialAction>();

        public UtilityBrain(AIController a_controller, List<ActionSO> a_actions, UtilityBrainSO a_brainSO)
        {
            m_controller = a_controller;
            m_availableActions = a_actions;
            m_brainSO = a_brainSO;
        }

        public void UpdateBrain(float a_dt)
        {
            m_potentialActions.Clear();
            GetPotentialActions();

            if (!m_inAction)
            {
                ActionSO action = GetAction();
                if (action != null)
                {
                    action.Action(this);
                }
            }
        }
        private void GetPotentialActions()
        {
            foreach(ActionSO a in m_availableActions)
            {
                if (a.m_actionType == ActionType.Building && m_controller.getBuildingTimer >= 0)
                    continue;


                m_potentialActions.Add(new PotentialAction(a, this));
            }
        }

        private ActionSO GetAction()
        {
            PotentialAction action = new PotentialAction();

            float weight = 0;

            foreach (PotentialAction posAction in m_potentialActions)
            {
                float w = posAction.m_weight;

                if (m_brainSO != null)
                {
                    Debug.Log("Before | " + m_brainSO.name + " | " + posAction.m_action.name + " : " + w);
                    w += Random.Range(m_brainSO.getMinOffset, m_brainSO.getMaxOffset);
                    Debug.Log("After | " + m_brainSO.name +" | " + posAction.m_action.name + " : " + w);
                }

                //if (posAction.m_weight != 0 && posAction.m_weight > weight)
                if (posAction.m_weight > weight)
                {
                    weight = w;
                    action = posAction;
                }
            }

            return action.m_action;
        }

    }
}
