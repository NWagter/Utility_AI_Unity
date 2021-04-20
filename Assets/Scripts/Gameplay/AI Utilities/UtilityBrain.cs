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

        public AIController m_controller { get; private set; }
        public ResourceManager m_resourceManager => m_controller.m_resourceManager;
        public Builder m_builder => m_controller.m_builder;

        private bool m_inAction = false;

        private List<ActionSO> m_availableActions;
        private List<PotentialAction> m_potentialActions = new List<PotentialAction>();

        private Vector2 m_randomness = new Vector2(-0.1f, 0.1f);

        public UtilityBrain(AIController a_controller, List<ActionSO> a_actions)
        {
            m_controller = a_controller;
            m_availableActions = a_actions;
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
                //if (posAction.m_weight != 0 && posAction.m_weight > weight)
                if (posAction.m_weight >= weight)
                {
                    weight = (posAction.m_weight);
                    action = posAction;
                }
            }

            return action.m_action;
        }

    }
}
