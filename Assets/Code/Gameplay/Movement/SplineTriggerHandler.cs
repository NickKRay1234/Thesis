using UnityEngine;
using Zenject;

namespace Code.Gameplay.Movement
{
    public sealed class SplineTriggerHandler : MonoBehaviour
    {
        [Inject] 
        private SplineMover _splineMover;
        private Transform _lastPlatform;
    }
}