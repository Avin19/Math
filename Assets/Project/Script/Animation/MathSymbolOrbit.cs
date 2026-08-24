using UnityEngine;
using DG.Tweening;

public class MathSymbolOrbit : MonoBehaviour
{
    [Header("Orbit")]
    [SerializeField] private RectTransform center;
    [SerializeField] private float duration = 8f;
    [SerializeField] private float startAngle = 0f;

    [Header("Animation")]
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float rotationAmount = 360f;
    [SerializeField] private float scaleAmount = 1.15f;
    [SerializeField] private float radiusX = 220f;
    [SerializeField] private float radiusY = 160f;
    private RectTransform rectTransform;
    private float currentAngle;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        currentAngle = startAngle;
    }

    private void Start()
    {
        StartOrbit();
    }

    private void StartOrbit()
    {
        float direction = clockwise ? -1f : 1f;

        DOTween.To(
            () => currentAngle,
            value =>
            {
                currentAngle = value;
                UpdatePosition();
            },
            currentAngle + (360f * direction),
            duration
        )
        .SetEase(Ease.Linear)
        .SetLoops(-1);

        // Continuous rotation
        transform
            .DORotate(
                new Vector3(0, 0, rotationAmount * direction),
                duration,
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.Linear)
            .SetLoops(-1);

        // Subtle breathing
        transform
            .DOScale(scaleAmount, duration * 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void UpdatePosition()
    {
        float radians = currentAngle * Mathf.Deg2Rad;

        Vector2 position = new Vector2(
            Mathf.Cos(radians) * radiusX,
            Mathf.Sin(radians) * radiusY
        );

        rectTransform.anchoredPosition =
            center.anchoredPosition + position;
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}