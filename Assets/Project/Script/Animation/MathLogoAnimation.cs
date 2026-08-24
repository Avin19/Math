using UnityEngine;
using DG.Tweening;

public class MathLogoAnimation : MonoBehaviour
{

    [SerializeField] private Letter[] letters;

    [Header("Entrance")]
    [SerializeField] private float startOffsetY = -120f;
    [SerializeField] private float duration = 0.35f;
    [SerializeField] private float stagger = 0.06f;

    [Header("Scale")]
    [SerializeField] private float startScale = 0.4f;
    [SerializeField] private float overshootScale = 1.12f;

    private Vector2[] originalPositions;

    private void Start()
    {
        originalPositions = new Vector2[letters.Length];

        for (int i = 0; i < letters.Length; i++)
        {
            originalPositions[i] =
                letters[i].transform.anchoredPosition;
        }

        PlayIntro();
    }

    public void PlayIntro()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < letters.Length; i++)
        {
            RectTransform letter = letters[i].transform;

            Vector2 targetPosition =
                originalPositions[i];

            Vector2 startPosition =
                targetPosition + Vector2.up * startOffsetY;

            letter.anchoredPosition = startPosition;

            letter.localScale =
                Vector3.one * startScale;

            letter.localRotation =
                Quaternion.Euler(
                    0,
                    0,
                    Random.Range(-15f, 15f)
                );

            sequence.Insert(
                i * stagger,

                letter.DOAnchorPos(
                    targetPosition,
                    duration
                )
                .SetEase(Ease.OutBack)
            );

            sequence.Insert(
                i * stagger,

                letter.DOScale(
                    overshootScale,
                    duration * 0.7f
                )
                .SetEase(Ease.OutBack)
            );

            sequence.Insert(
                i * stagger + duration * 0.7f,

                letter.DOScale(
                    1f,
                    duration * 0.3f
                )
                .SetEase(Ease.OutQuad)
            );

            sequence.Insert(
                i * stagger,

                letter.DORotate(
                    Vector3.zero,
                    duration
                )
                .SetEase(Ease.OutBack)
            );
        }

        sequence.OnComplete(StartIdleAnimation);
    }

    private void StartIdleAnimation()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            RectTransform letter =
                letters[i].transform;

            float delay =
                Random.Range(0f, 0.5f);

            letter
                .DOAnchorPosY(
                    letter.anchoredPosition.y + 5f,
                    1.2f
                )
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetDelay(delay);
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}