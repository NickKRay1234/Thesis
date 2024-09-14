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
            #if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseSwipe();
            #else
            HandleTouchSwipe();
            #endif
        }

        private void HandleTouchSwipe()
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
                    StartSwipe(touch.position);
                    break;
                case TouchPhase.Ended when IsSwiping:
                    EndSwipe(touch.position);
                    break;
                case TouchPhase.Canceled:
                    CancelSwipe();
                    break;
            }
        }

        private void HandleMouseSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartSwipe(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0) && IsSwiping)
            {
                EndSwipe(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                CancelSwipe();
            }
        }

        private void StartSwipe(Vector2 position)
        {
            _startTouchPosition = position;
            IsSwiping = true;
        }

        private void EndSwipe(Vector2 position)
        {
            var swipeDelta = position - _startTouchPosition;
            IsSwipeRight = swipeDelta.x > 0 && Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y);
            IsSwiping = Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y);
        }

        private void CancelSwipe() =>
            IsSwiping = false;
    }
}