using UnityEngine;

namespace Code.Gameplay.Camera
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Camera/Settings", order = 1)]
    public class CameraSettings : ScriptableObject
    {
        public bool followX = true;
        public bool followY = true;
        public bool followZ = true;
    }
}