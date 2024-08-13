using UnityEngine;

namespace Code.Gameplay.Movement
{
    public sealed class SplineMover : MonoBehaviour
    {
        [SerializeField] private SplineMovementSettings _settings;

        private Spline _spline;
        private float _time;
        private bool _isMoving;

        public bool IsMoving => _isMoving;

        private void FixedUpdate()
        {
            if (!_isMoving) return;
            UpdateSplineMovement();
        }

        public void StartMoving(Spline spline)
        {
            _spline = spline;
            _time = 0f;
            _isMoving = true;
        }

        private void UpdateSplineMovement()
        {
            _time += Time.deltaTime;
            float t = _time / _settings.Duration;

            if (t > 1.0f)
            {
                StopMoving();
                return;
            }

            Vector3 newPosition = CalculateSplinePosition(t);
            transform.position = newPosition;

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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360f);
        }

        private void StopMoving()
        {
            _time = 0f;
            _isMoving = false;
        }
    }
}