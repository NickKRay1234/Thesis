using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Movement
{
    public sealed class RotatableRail : MonoBehaviour
    {
        [Inject] private IPlayer _player;
        [SerializeField] private RotationSettings _rotationSettings;
        [SerializeField] private float _exitDelay = 0.5f; 

        private const float ROTATION_ANGLE = 90f;
        private const float BLOCKED_AXIS = 0f;
        private const float ANGLE_TOLERANCE = 0.1f;
        
        private SplineControlPoints _splineControlPoints;
        private Tween _rotationTween;
        private Renderer _renderer;
        
        public bool IsRotating { get; private set; }

        private void Start()
        {
            InitializeComponents();
            UpdateMaterialBasedOnAngle();
        }

        private void InitializeComponents()
        {
            _splineControlPoints = GetComponent<SplineControlPoints>();
            _renderer = GetComponent<Renderer>();
        }

        public void StartRotatableRailRotate(bool rotateRight = true)
        {
            IsRotating = true;
            RotateAndHandleCompletion(rotateRight);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == _player.TrainLayerName) 
                _player.IsPlayerOnRotatableRail = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == _player.TrainLayerName)
                StartCoroutine(SetPlayerOnRotatableRailWithDelay(false, _exitDelay));
        }

        private IEnumerator SetPlayerOnRotatableRailWithDelay(bool isOnRail, float delay)
        {
            yield return new WaitForSeconds(delay);
            _player.IsPlayerOnRotatableRail = isOnRail;
        }

        private void RotateAndHandleCompletion(bool rotateRight)
        {
            _rotationTween?.Kill();
            float rotationAngle = rotateRight ? ROTATION_ANGLE : -ROTATION_ANGLE;
            _rotationTween = transform.DORotate(new Vector3(BLOCKED_AXIS, transform.eulerAngles.y + rotationAngle, BLOCKED_AXIS), _rotationSettings.RotationSpeed)
                .SetUpdate(true)
                .SetEase(_rotationSettings.RotationEase)
                .OnComplete(OnRotationComplete);
        }
        
        public void CancelRotation() 
        {
            if (!IsRotating) return;
            _rotationTween?.Kill();
            transform.eulerAngles = new Vector3(BLOCKED_AXIS, Mathf.Round(transform.eulerAngles.y / ROTATION_ANGLE) * ROTATION_ANGLE, BLOCKED_AXIS);
            _splineControlPoints.InitializeSplinePoints();
            UpdateMaterialBasedOnAngle();
            IsRotating = false;
        }

        private void OnRotationComplete()
        {
            IsRotating = false;
            _splineControlPoints.InitializeSplinePoints();
            UpdateMaterialBasedOnAngle();
            _player.GetSplineMover()?.RestartSplineMovement(transform);
        }

        private void UpdateMaterialBasedOnAngle() =>
            SetMaterial(IsCorrectAngle(transform.eulerAngles.y));

        public bool IsCorrectAngle(float currentAngle)
        {
            currentAngle %= 360;
            return _rotationSettings.CorrectAngles.Any(angle => Mathf.Abs(currentAngle - angle) <= ANGLE_TOLERANCE || Mathf.Abs((currentAngle - 360) - angle) <= ANGLE_TOLERANCE);
        }

        private void SetMaterial(bool isCorrectAngle) =>
            _renderer.material = isCorrectAngle ? _rotationSettings.CorrectMaterial : _rotationSettings.IncorrectMaterial;
    }
}