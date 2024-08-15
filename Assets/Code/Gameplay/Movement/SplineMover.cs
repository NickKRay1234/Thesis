using UnityEngine;

namespace Code.Gameplay.Movement
{
    public sealed class SplineMover : IMovable
    {
        private readonly SplineMovementSettings _settings;
        private readonly IPlayer _player;
        private Transform _lastPlatform;

        public SplineMover(IPlayer player, SplineMovementSettings movementSettings = null)
        {
            _player = player;
            _settings = movementSettings;
        }

        private Spline _spline;
        private float _time;
        private bool _isMoving;

        public bool IsMoving => _isMoving;

        private void StartMoving(Spline spline)
        {
            _spline = spline;
            _time = 0f;
            _isMoving = true;
        }

        public void UpdateSplineMovement()
        {
            _time += Time.deltaTime;
            float t = _time / _settings.Duration;

            if (t > 1.0f)
            {
                StopMoving();
                return;
            }

            Vector3 newPosition = CalculateSplinePosition(t);
            _player.GetTransform().position = newPosition;

            UpdateRotation(newPosition, t);
        }

        private Vector3 CalculateSplinePosition(float t)
        {
            Point startPoint = _spline.StartPoint;
            Point middlePoint = _spline.MiddlePoint;
            Point endPoint = _spline.EndPoint;

            return Bezier.GetPoint(startPoint.Position, middlePoint.Position, endPoint.Position, t);
        }

        private void UpdateRotation(Vector3 newPosition, float t)
        {
            float nextT = Mathf.Clamp01(t + 0.01f);
            Vector3 nextPosition = CalculateSplinePosition(nextT);

            Vector3 direction = (nextPosition - newPosition).normalized;
            if (direction == Vector3.zero) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _player.GetTransform().rotation = Quaternion.RotateTowards(_player.GetTransform().rotation, targetRotation, Time.deltaTime * 360f);
        }
        
        public void TryStartMovement(Collider other)
        {
            if (!CanStartMovement(other)) return;

            SplineControlPoints controlPoints = other.GetComponent<SplineControlPoints>();
            if (controlPoints == null) return;

            Spline spline = CreateSplineFromContainer(controlPoints);
            StartMoving(spline);

            _lastPlatform = other.transform;
        }
        
        private bool CanStartMovement(Collider other) =>
            !IsMoving && other.transform != _lastPlatform && other.GetComponent<SplineControlPoints>();

        private Spline CreateSplineFromContainer(SplineControlPoints controlPoints) => new(
            controlPoints.GetControlPoint(0) ?? new Point(Vector3.zero),
            controlPoints.GetControlPoint(1) ?? new Point(Vector3.zero),
            controlPoints.GetControlPoint(2) ?? new Point(Vector3.zero));

        private void StopMoving()
        {
            _time = 0f;
            _isMoving = false;
        }
        
        public void RestartSplineMovement(Transform currentPlatform)
        {
            if (_lastPlatform != currentPlatform) return;
            SplineControlPoints controlPoints = currentPlatform.GetComponent<SplineControlPoints>();
            if (controlPoints == null) return;

            _spline = CreateSplineFromContainer(controlPoints);
        }
    }
}