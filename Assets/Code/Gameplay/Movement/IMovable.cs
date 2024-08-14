namespace Code.Gameplay.Movement
{
    public interface IMovable
    {
        void UpdateSplineMovement();
        public bool IsMoving { get; }
    }
}