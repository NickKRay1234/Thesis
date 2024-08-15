using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Movement
{
    [CreateAssetMenu(fileName = "RotationSettings", menuName = "Settings/RotationSettings")]
    public class RotationSettings : ScriptableObject
    {
        public Ease RotationEase;
        public float RotationSpeed;
        public Material CorrectMaterial;
        public Material IncorrectMaterial;
        public List<float> CorrectAngles;
    }
}