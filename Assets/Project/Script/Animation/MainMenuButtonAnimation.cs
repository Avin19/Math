using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainMenuButtonAnimation : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Animation")]
    [SerializeField] private float startOffsetY = -150f;
    [SerializeField] private float entranceDuration = 0.45f;
    [SerializeField] private float startScale = 0.75f;
    [SerializeField] private float overshootScale = 1.05f;

    [Header("Idle")]
    [SerializeField] private bool enableIdleAnimation = true;
    [SerializeField] private float idleScale = 1.02f;
    [SerializeField] private float idleDuration = 1.5f;

    [Header("Press")]
    [SerializeField] private float pressScale = 0.94f;
    [SerializeField] private float pressDuration = 0.08f;

    private RectTransform rectTransform;

    private Vector2 originalPosition;
    private Vector3 originalScale;

    private Tween idleTween;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        originalPosition = rectTransform.anchoredPosition;
        originalScale = rectTransform.localScale;
    }

    private void Start()
    {
        PlayEntrance();
    }

    public void PlayEntrance()
    {
        KillAnimations();

        rectTransform.anchoredPosition =
            originalPosition +
            Vector2.up * startOffsetY;

        rectTransform.localScale =
            originalScale * startScale;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            rectTransform.DOAnchorPos(
                originalPosition,
                entranceDuration
            )
            .SetEase(Ease.OutBack)
        );

        sequence.Join(
            rectTransform.DOScale(
                originalScale * overshootScale,
                entranceDuration * 0.75f
            )
            .SetEase(Ease.OutBack)
        );

        sequence.Append(
            rectTransform.DOScale(
                originalScale,
                0.18f
            )
            .SetEase(Ease.OutQuad)
        );

        sequence.OnComplete(() =>
        {
            if (enableIdleAnimation)
                StartIdle();
        });
    }

    private void StartIdle()
    {
        idleTween = rectTransform
            .DOScale(
                originalScale * idleScale,
                idleDuration
            )
            .SetEase(Ease.InOutSine)
            .SetLoops(
                -1,
                LoopType.Yoyo
            );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        idleTween?.Pause();

        rectTransform
            .DOScale(
                originalScale * pressScale,
                pressDuration
            )
            .SetEase(Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform
            .DOScale(
                originalScale * overshootScale,
                0.12f
            )
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                if (enableIdleAnimation)
                    StartIdle();
            });
    }

    public void Replay()
    {
        PlayEntrance();
    }

    private void KillAnimations()
    {
        idleTween?.Kill();

        rectTransform.DOKill();
    }

    private void OnDisable()
    {
        KillAnimations();
    }

    private void OnDestroy()
    {
        KillAnimations();
    }
}