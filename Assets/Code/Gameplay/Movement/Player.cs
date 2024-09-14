using UnityEngine;

namespace Code.Gameplay.Movement
{
    public sealed class Player : MonoBehaviour, IPlayer
    {
        private SplineMover _moveAlongSpine;
        private SplineMovementSettings _settings;
        private const int _trainLayerNumber = 3;
        private string _trainLayerName;

        public Transform GetTransform() => transform;
        public SplineMover GetSplineMover() => _moveAlongSpine;
        public bool IsPlayerOnRotatableRail { get; set; }
        public LayerMask TrainLayerName { get; set; }

        public void Awake()
        {
            _settings = ScriptableObject.CreateInstance<SplineMovementSettings>();
            _moveAlongSpine = new SplineMover(this, _settings);
            IsPlayerOnRotatableRail = false;
            _trainLayerName = LayerMask.LayerToName(_trainLayerNumber);
            TrainLayerName = LayerMask.NameToLayer(_trainLayerName);
        }

        // TODO: Should be fixed. Optimization question.
        private void OnTriggerStay(Collider other) => 
            _moveAlongSpine.TryStartMovement(other);
        
    }
}