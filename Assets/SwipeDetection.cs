using UnityEngine;

public sealed class SwipeDetection : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwipeDetected = false;
    
    [SerializeField] private float minSwipeDistance = 50f;

    void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // Обработка свайпа мышью (для тестирования в редакторе Unity)
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            isSwipeDetected = true;
        }
#endif

#if UNITY_IOS || UNITY_ANDROID
        // Обработка свайпа через касания на мобильных устройствах
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                isSwipeDetected = true;
            }
        }
#endif

        if (isSwipeDetected)
        {
            HandleSwipe();
            isSwipeDetected = false;
        }
    }

    private void HandleSwipe()
    {
        float distance = endTouchPosition.y - startTouchPosition.y;
        if (Mathf.Abs(distance) >= minSwipeDistance)
        {
            if (distance > 0)
            {
                Debug.Log("Swipe Up");
                // Добавьте логику для свайпа вверх
            }
            else
            {
                Debug.Log("Swipe Down");
                // Добавьте логику для свайпа вниз
            }
        }
    }
}

