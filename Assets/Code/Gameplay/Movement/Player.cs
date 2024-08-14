using UnityEngine;

namespace Code.Gameplay.Movement
{
    public sealed class Player : MonoBehaviour, IPlayer
    {
        private SplineMover _moveAlongSpine;
        private SplineMovementSettings _settings;
        
        public Transform GetTransform() => transform;
        public SplineMover GetSplineMover() => _moveAlongSpine;

        public void Awake()
        {
            _settings = ScriptableObject.CreateInstance<SplineMovementSettings>();
            _moveAlongSpine = new SplineMover(this, _settings);
        }
        
        private void OnTriggerStay(Collider other) => 
            _moveAlongSpine.TryStartMovement(other);
        
    }
}