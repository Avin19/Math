using UnityEngine;
using DG.Tweening;

public class MathLMainMenuAnimation : MonoBehaviour
{

    [SerializeField] private Letter[] letters;

    [Header("Entrance")]
    [SerializeField] private float startOffsetY = 120f;
    [SerializeField] private float duration = 0.35f;
    [SerializeField] private float stagger = 0.06f;

    [Header("Scale")]
    [SerializeField] private float startScale = 0.4f;
    [SerializeField] private float overshootScale = 1.12f;

    [Header("Rotation")]
    [SerializeField] private float maxStartRotation = 15f;

    [Header("Cycle")]
    [SerializeField] private float holdTime = 3f;
    [SerializeField] private float restartDuration = 0.35f;

    private Vector2[] originalPositions;
    private Sequence animationSequence;

    private void Start()
    {
        CachePositions();
        PlayIntro();
    }

    private void CachePositions()
    {
        originalPositions = new Vector2[letters.Length];

        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i].transform == null)
                continue;

            originalPositions[i] =
                letters[i].transform.anchoredPosition;
        }
    }

    public void PlayIntro()
    {
        KillAnimations();

        animationSequence = DOTween.Sequence();

        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i].transform == null)
                continue;

            RectTransform letter =
                letters[i].transform;

            Vector2 targetPosition =
                originalPositions[i];

            Vector2 startPosition =
                targetPosition +
                Vector2.down * startOffsetY;

            // Starting state
            letter.anchoredPosition = startPosition;

            letter.localScale =
                Vector3.one * startScale;

            letter.localRotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    Random.Range(
                        -maxStartRotation,
                        maxStartRotation
                    )
                );

            float delay = i * stagger;

            // Position
            animationSequence.Insert(
                delay,
                letter.DOAnchorPos(
                    targetPosition,
                    duration
                )
                .SetEase(Ease.OutBack)
            );

            // Scale up
            animationSequence.Insert(
                delay,
                letter.DOScale(
                    overshootScale,
                    duration * 0.7f
                )
                .SetEase(Ease.OutBack)
            );

            // Scale settle
            animationSequence.Insert(
                delay + duration * 0.7f,
                letter.DOScale(
                    1f,
                    duration * 0.3f
                )
                .SetEase(Ease.OutQuad)
            );

            // Rotation
            animationSequence.Insert(
                delay,
                letter.DORotate(
                    Vector3.zero,
                    duration
                )
                .SetEase(Ease.OutBack)
            );
        }

        float introTime =
            (letters.Length - 1) * stagger +
            duration;

        // Hold completed logo
        animationSequence.AppendInterval(holdTime);

        // Restart
        animationSequence.AppendCallback(ResetLetters);

        // Wait before replay
        animationSequence.AppendInterval(0.15f);

        animationSequence.OnComplete(PlayIntro);
    }

    private void ResetLetters()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i].transform == null)
                continue;

            RectTransform letter =
                letters[i].transform;

            letter.anchoredPosition =
                originalPositions[i] +
                Vector2.down * startOffsetY;

            letter.localScale =
                Vector3.one * startScale;

            letter.localRotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    Random.Range(
                        -maxStartRotation,
                        maxStartRotation
                    )
                );
        }
    }

    private void KillAnimations()
    {
        if (animationSequence != null &&
            animationSequence.IsActive())
        {
            animationSequence.Kill();
        }

        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i].transform == null)
                continue;

            letters[i].transform.DOKill();
        }
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

[System.Serializable]
public class Letter
{
    public RectTransform transform;
}