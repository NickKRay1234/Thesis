using UnityEngine;

namespace Code.Gameplay.Movement
{
    [CreateAssetMenu(fileName = "SplineMovementSettings", menuName = "Settings/SplineMovementSettings", order = 1)]
    public sealed class SplineMovementSettings : ScriptableObject
    {
        [SerializeField] private float _duration = 2.5f;
        public float Duration => _duration;
    }
}