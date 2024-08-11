using UnityEngine;

namespace Code.Gameplay.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target; 
        [SerializeField] private CameraSettings _cameraSettings; 
        private Vector3 _offset;

        private void Start() => InitializeOffset();
        private void LateUpdate() => UpdateCameraPosition();
    
    
        /// Initialization of camera offset relative to the target object
        private void InitializeOffset() =>
            _offset = _target ? transform.position - _target.position : Vector3.zero;

        /// Update the camera position to follow the target object according to the settings
        private void UpdateCameraPosition()
        {
            if (!_target) return;
            Vector3 targetPosition = _target.position + _offset;
            transform.position = CalculateNewPosition(targetPosition);;
        }
    
        /// Update camera position coordinates depending on the settings
        private Vector3 CalculateNewPosition(Vector3 targetPosition) =>
            transform.position = new Vector3(
                _cameraSettings.followX ? targetPosition.x : transform.position.x,
                _cameraSettings.followY ? targetPosition.y : transform.position.y,
                _cameraSettings.followZ ? targetPosition.z : transform.position.z);
    }
}