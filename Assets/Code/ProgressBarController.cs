using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ProgressBarController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private Slider progressBar;

        private float totalDistance;

        private void Start()
        {
            if (startPoint == null || endPoint == null || progressBar == null || playerTransform == null)
            {
                Debug.LogError("All references need to be assigned in the inspector.");
                enabled = false;
                return;
            }

            totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
            progressBar.minValue = 0;
            progressBar.maxValue = 1;
            progressBar.value = 0;
        }

        private void Update() => UpdateProgressBar();

        private void UpdateProgressBar()
        {
            Vector3 playerPositionProjected = ProjectPointOnLine(startPoint.position, endPoint.position, playerTransform.position);
            float currentDistance = Vector3.Distance(startPoint.position, playerPositionProjected);
            float progress = currentDistance / totalDistance;
        
            progressBar.value = Mathf.Clamp01(progress);
        }

        private Vector3 ProjectPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
        {
            Vector3 lineDirection = (lineEnd - lineStart).normalized;
            float projection = Vector3.Dot((point - lineStart), lineDirection);
            Vector3 projectedPoint = lineStart + lineDirection * projection;
            return projectedPoint;
        }
    }
}