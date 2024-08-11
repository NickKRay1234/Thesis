using UnityEngine;

namespace Code.Gameplay.Movement
{
    public static class Bezier
    {
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 point = uu * p0;
            point += 2 * u * t * p1; 
            point += tt * p2;

            return point;
        }
    }
}