using UnityEngine;

namespace Code.Gameplay.Movement
{
    public interface IPlayer
    {
        Transform GetTransform();
        SplineMover GetSplineMover();
    }
}