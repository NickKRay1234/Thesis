using UnityEngine;

namespace Code.Gameplay.Movement
{
    public interface IPlayer
    {
        Transform GetTransform();
        SplineMover GetSplineMover();
        bool IsPlayerOnRotatableRail { get; set; }
        public LayerMask TrainLayerName { get; set; }
    }
}