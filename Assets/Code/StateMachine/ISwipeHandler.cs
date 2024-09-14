namespace Code.StateMachine
{
    public interface ISwipeHandler
    {
        bool IsSwiping { get; }
        bool IsSwipeRight { get; }
        void HandleSwipe();
    }
}