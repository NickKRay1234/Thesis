using UnityEngine;

namespace Code.StateMachine
{
    public sealed class SwipeHandler : ISwipeHandler
    {
        private Vector2 _startTouchPosition;
        
        public bool IsSwiping { get; private set; }
        public bool IsSwipeRight { get; private set; }

        public void HandleSwipe()
        {
            if (Input.touchCount == 0)
            {
                IsSwiping = false;
                return;
            }

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartSwipe(touch);
                    break;
                case TouchPhase.Ended when IsSwiping:
                    EndSwipe(touch);
                    break;
                case TouchPhase.Canceled:
                    CancelSwipe();
                    break;
            }
        }

        private void StartSwipe(Touch touch)
        {
            _startTouchPosition = touch.position;
            IsSwiping = true;
        }

        private void EndSwipe(Touch touch)
        {
            var swipeDelta = touch.position - _startTouchPosition;
            IsSwipeRight = swipeDelta.x > 0 && Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y);
            IsSwiping = Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y); // Убедиться, что это горизонтальный свайп
        }

        private void CancelSwipe() =>
            IsSwiping = false;
    }
}