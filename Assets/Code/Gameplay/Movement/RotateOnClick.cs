using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Movement
{
    public sealed class RotateOnClick : MonoBehaviour
    {
        [Inject] private IPlayer _player;
        [SerializeField] private RotationSettings _rotationSettings;

        private const float ROTATION_ANGLE = 90f;
        private const float BLOCKED_AXIS = 0f;
        private const float ANGLE_TOLERANCE = 0.1f;

        private SplineControlPoints _splineControlPoints;
        private Renderer _renderer;
        private bool IsRotating { get; set; }

        private void Start()
        {
            InitializeComponents();
            UpdateMaterialBasedOnAngle();
        }

        public void CheckForRotation()
        {
            if (Input.GetMouseButtonUp(0) && !IsRotating)
                StartRotation();
        }

        private void InitializeComponents()
        {
            _splineControlPoints = GetComponent<SplineControlPoints>();
            _renderer = GetComponent<Renderer>();
        }

        private void StartRotation()
        {
            IsRotating = true;
            RotateAndHandleCompletion();
        }

        private void RotateAndHandleCompletion() =>
            transform.DORotate(new Vector3(BLOCKED_AXIS, transform.eulerAngles.y + ROTATION_ANGLE, BLOCKED_AXIS), _rotationSettings.RotationSpeed)
                .SetEase(_rotationSettings.RotationEase)
                .OnComplete(OnRotationComplete);

        private void OnRotationComplete()
        {
            IsRotating = false;
            _splineControlPoints.InitializeSplinePoints();
            UpdateMaterialBasedOnAngle();
            _player.GetSplineMover()?.RestartSplineMovement(transform);
        }

        private void UpdateMaterialBasedOnAngle() =>
            SetMaterial(IsCorrectAngle(transform.eulerAngles.y));

        private bool IsCorrectAngle(float currentAngle)
        {
            currentAngle %= 360;
            return _rotationSettings.CorrectAngles.Any(angle => Mathf.Abs(currentAngle - angle) <= ANGLE_TOLERANCE || Mathf.Abs((currentAngle - 360) - angle) <= ANGLE_TOLERANCE);
        }

        private void SetMaterial(bool isCorrectAngle) =>
            _renderer.material = isCorrectAngle ? _rotationSettings.CorrectMaterial : _rotationSettings.IncorrectMaterial;
    }
}