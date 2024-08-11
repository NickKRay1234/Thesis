using UnityEngine;

namespace Code.Gameplay.Movement
{
    public sealed class MoveAlongSpline : MonoBehaviour
    {
        [SerializeField] private Transform _p0;
        [SerializeField] private Transform _p1;
        [SerializeField] private Transform _p2;
        [SerializeField] private float _duration = 5.0f;

        private float _time;

        private void Update()
        {
            _time += Time.deltaTime;

            // Ensure the normalized time [0, 1]
            float t = _time / _duration;

            if (t > 1.0f)
            {
                _time = 0f;
                t = 0f;
            }

            Vector3 newPosition = Bezier.GetPoint(_p0.position, _p1.position, _p2.position, t);

            // Update position
            transform.position = newPosition;

            // Calculate the next normalization time
            float nextT = Mathf.Clamp01(t + 0.01f); // Small increment to get the next point

            // Calculate the next position on the Bezier curve
            Vector3 nextPosition = Bezier.GetPoint(_p0.position, _p1.position, _p2.position, nextT);

            // Calculate direction to look at next position
            Vector3 direction = (nextPosition - newPosition).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360f);
            }
        }
    }
}