using UnityEngine;
using System.Collections.Generic;

namespace Code.Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SplineContainer : MonoBehaviour
    {
        private Transform _bezierCoordinatesContainer;
        private readonly List<Point> _controlPoints = new();

        private void Awake()
        {
            _bezierCoordinatesContainer = transform.GetChild(0);
            if (_bezierCoordinatesContainer == null)
            {
                Debug.LogError("Bezier Coordinates Container is not assigned.");
                return;
            }

            InitializeSplinePoints();
        }

        private void InitializeSplinePoints()
        {
            _controlPoints.Clear();
            for (int i = 0; i < _bezierCoordinatesContainer.childCount; i++)
            {
                Transform controlPoint = _bezierCoordinatesContainer.GetChild(i);
                _controlPoints.Add(new Point(controlPoint.position));
            }
            ValidateSolines();
        }

        private void ValidateSolines()
        {
            if (_controlPoints.Count < 3)
                Debug.LogWarning("Spline requires at least 3 points for a valid bezier curve.");
        }

        public Point? GetControlPoint(int index)
        {
            if (index >= 0 && index < _controlPoints.Count) return _controlPoints[index];
            Debug.LogError("Index out of range.");
            return null;
        }
    }
}