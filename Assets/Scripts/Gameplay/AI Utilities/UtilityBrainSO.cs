using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "Brain", menuName = "AI/Utilities/Brain/Brain", order = 1)]
    public class UtilityBrainSO : ContentSO
    {
        public float getMinOffset => m_minOffset;
        public float getMaxOffset => m_maxOffset;

        [Tooltip("Random offet for inputs")]
        [SerializeField]
        private float m_minOffset = -0.05f;
        [SerializeField]
        private float m_maxOffset = 0.05f;
    }
}
