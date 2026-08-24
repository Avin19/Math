using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsPanelAnimation : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private RectTransform panel;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Open Animation")]
    [SerializeField] private float openDuration = 0.35f;
    [SerializeField] private float startScale = 0.75f;
    [SerializeField] private float overshootScale = 1.05f;
    [SerializeField] private float startOffsetY = -80f;

    [Header("Close Animation")]
    [SerializeField] private float closeDuration = 0.2f;
    [SerializeField] private float closeScale = 0.8f;

    private Vector2 originalPosition;
    private Vector3 originalScale;

    private void Awake()
    {
        originalPosition = panel.anchoredPosition;
        originalScale = panel.localScale;

        canvasGroup.alpha = 0f;
        panel.localScale = originalScale * startScale;

        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);

        panel.DOKill();
        canvasGroup.DOKill();

        // Reset
        panel.anchoredPosition =
            originalPosition +
            Vector2.down * startOffsetY;

        panel.localScale =
            originalScale * startScale;

        canvasGroup.alpha = 0f;

        Sequence sequence = DOTween.Sequence();

        // Fade in
        sequence.Join(
            canvasGroup.DOFade(
                1f,
                openDuration * 0.7f
            )
        );

        // Move
        sequence.Join(
            panel.DOAnchorPos(
                originalPosition,
                openDuration
            )
            .SetEase(Ease.OutCubic)
        );

        // Scale
        sequence.Join(
            panel.DOScale(
                originalScale * overshootScale,
                openDuration
            )
            .SetEase(Ease.OutBack)
        );

        // Settle
        sequence.Append(
            panel.DOScale(
                originalScale,
                0.12f
            )
            .SetEase(Ease.OutQuad)
        );
    }

    public void Close()
    {
        panel.DOKill();
        canvasGroup.DOKill();

        Sequence sequence = DOTween.Sequence();

        sequence.Join(
            canvasGroup.DOFade(
                0f,
                closeDuration
            )
        );

        sequence.Join(
            panel.DOScale(
                originalScale * closeScale,
                closeDuration
            )
            .SetEase(Ease.InBack)
        );

        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);

            // Restore state for next opening
            panel.anchoredPosition = originalPosition;
            panel.localScale = originalScale;
        });
    }

    private void OnDestroy()
    {
        panel.DOKill();
        canvasGroup.DOKill();
    }
}