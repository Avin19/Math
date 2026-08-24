using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MathKeyButtonAnimation : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private float pressedScale = 0.90f;
    [SerializeField] private float duration = 0.07f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();

        transform
            .DOScale(
                originalScale * pressedScale,
                duration
            )
            .SetEase(Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();

        transform
            .DOScale(
                originalScale * 1.05f,
                0.08f
            )
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                transform.DOScale(
                    originalScale,
                    0.08f
                );
            });
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.localScale = originalScale;
    }
}