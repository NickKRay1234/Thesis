using UnityEngine;
using System.Collections.Generic;

namespace Code.Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SplineControlPoints : MonoBehaviour
    {
        private Transform _bezierCoordinatesContainer;
        private readonly List<Point> _controlPoints = new();
        private const int FIRST_CHILD = 0;
        
        private void Awake()
        {
            _bezierCoordinatesContainer = transform.GetChild(FIRST_CHILD);
            InitializeSplinePoints();
        }

        public void InitializeSplinePoints()
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
#if UNITY_EDITOR
            if (_controlPoints.Count < 3)
                Debug.LogWarning("Spline requires at least 3 points for a valid bezier curve.");
#endif
        }

        public Point? GetControlPoint(int index)
        {
            if (index >= 0 && index < _controlPoints.Count) return _controlPoints[index];
#if UNITY_EDITOR
            Debug.LogError("Index out of range.");
#endif
            return null;
        }
    }
}