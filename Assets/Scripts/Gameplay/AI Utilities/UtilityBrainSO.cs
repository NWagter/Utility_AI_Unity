using UnityEngine;

namespace UtilAI
{
    [CreateAssetMenu(fileName = "Brain", menuName = "AI/Utilities/Brain/Brain", order = 1)]
    public class UtilityBrainSO : ScriptableObject
    {
        public float m_minOffset => _minOffset;
        public float m_maxOffset => _maxOffset;

        [Tooltip("Random offet for inputs")]
        [SerializeField]
        private float _minOffset = -0.05f;
        [SerializeField]
        private float _maxOffset = 0.05f;
    }
}
