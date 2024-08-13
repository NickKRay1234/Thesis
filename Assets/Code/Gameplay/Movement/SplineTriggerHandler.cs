using UnityEngine;

namespace Code.Gameplay.Movement
{
    public sealed class SplineTriggerHandler : MonoBehaviour
    {
        private SplineMover _splineMover;
        private Transform _lastPlatform;

        private void Awake() => _splineMover = GetComponent<SplineMover>();

        private void OnTriggerStay(Collider other) => TryStartMovement(other);

        private void TryStartMovement(Collider other)
        {
            if (!CanStartMovement(other)) return;

            SplineContainer container = other.GetComponent<SplineContainer>();
            if (container == null) return;

            Spline spline = CreateSplineFromContainer(container);
            _splineMover.StartMoving(spline);

            _lastPlatform = other.transform;
        }

        private bool CanStartMovement(Collider other) =>
            !_splineMover.IsMoving && other.transform != _lastPlatform && other.GetComponent<SplineContainer>();

        private Spline CreateSplineFromContainer(SplineContainer container) => new(
            container.GetControlPoint(0) ?? new Point(Vector3.zero),
            container.GetControlPoint(1) ?? new Point(Vector3.zero),
            container.GetControlPoint(2) ?? new Point(Vector3.zero));
    }
}