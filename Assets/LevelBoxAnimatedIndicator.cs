using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public sealed class LevelBoxAnimatedIndicator : MonoBehaviour
{
    [SerializeField] private float _startPoint;
    [SerializeField] private float _endPoint;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Image _indicatorImage;
    
    private Tweener _animationTweener;


    private void OnEnable() => StartIndicatorAnimation();
    private void OnDisable()
    {
        StopIndicatorAnimation();
        ResetIndicatorPosition();
    }

    private void StartIndicatorAnimation() =>
        _animationTweener = _indicatorImage.transform
            .DOLocalMoveY(_endPoint, _animationDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .OnStepComplete(SwapAnimationPoints);

    private void StopIndicatorAnimation()
    {
        if (_animationTweener != null && _animationTweener.IsActive())
            _animationTweener.Kill();
    }

    private void SwapAnimationPoints() => 
        (_startPoint, _endPoint) = (_endPoint, _startPoint);
    
    private void ResetIndicatorPosition()
    {
        var currentPosition = _indicatorImage.transform.localPosition;
        _indicatorImage.transform.localPosition = new Vector3(currentPosition.x, _startPoint, currentPosition.z);
    }
}