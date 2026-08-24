using UnityEngine;
using DG.Tweening;

public class GameplayUIAnimation : MonoBehaviour
{
    [Header("Main Elements")]
    [SerializeField] private RectTransform header;
    [SerializeField] private RectTransform questionPanel;
    [SerializeField] private RectTransform questionText;
    [SerializeField] private RectTransform answerArea;
    [SerializeField] private RectTransform keypad;

    [Header("Keypad Buttons")]
    [SerializeField] private RectTransform[] keypadButtons;

    [Header("Animation")]
    [SerializeField] private float headerDuration = 0.3f;
    [SerializeField] private float questionDuration = 0.45f;
    [SerializeField] private float keypadStagger = 0.04f;

    private Vector2 headerPosition;
    private Vector2 questionPosition;
    private Vector2 answerPosition;
    private Vector2 keypadPosition;

    private void Awake()
    {
        headerPosition = header.anchoredPosition;
        questionPosition = questionPanel.anchoredPosition;
        answerPosition = answerArea.anchoredPosition;
        keypadPosition = keypad.anchoredPosition;
    }

    private void Start()
    {
        PlayEntrance();
    }

    public void PlayEntrance()
    {
        DOTween.Kill(gameObject);

        // HEADER
        header.anchoredPosition =
            headerPosition + Vector2.up * 100f;

        header
            .DOAnchorPos(
                headerPosition,
                headerDuration
            )
            .SetEase(Ease.OutCubic);


        // QUESTION PANEL
        questionPanel.anchoredPosition =
            questionPosition + Vector2.down * 80f;

        questionPanel.localScale =
            Vector3.one * 0.85f;

        Sequence questionSequence =
            DOTween.Sequence();

        questionSequence.Append(
            questionPanel.DOAnchorPos(
                questionPosition,
                questionDuration
            )
            .SetEase(Ease.OutCubic)
        );

        questionSequence.Join(
            questionPanel.DOScale(
                1f,
                questionDuration
            )
            .SetEase(Ease.OutBack)
        );


        // QUESTION TEXT
        questionText.localScale =
            Vector3.one * 0.7f;

        questionText
            .DOScale(
                1f,
                0.35f
            )
            .SetEase(Ease.OutBack)
            .SetDelay(0.15f);


        // ANSWER AREA
        answerArea.anchoredPosition =
            answerPosition + Vector2.down * 50f;

        answerArea.localScale =
            Vector3.one * 0.9f;

        Sequence answerSequence =
            DOTween.Sequence();

        answerSequence.AppendInterval(0.25f);

        answerSequence.Append(
            answerArea.DOAnchorPos(
                answerPosition,
                0.3f
            )
            .SetEase(Ease.OutCubic)
        );

        answerSequence.Join(
            answerArea.DOScale(
                1f,
                0.3f
            )
            .SetEase(Ease.OutBack)
        );


        // KEYPAD
        keypad.anchoredPosition =
            keypadPosition + Vector2.down * 80f;

        keypad
            .DOAnchorPos(
                keypadPosition,
                0.4f
            )
            .SetEase(Ease.OutCubic)
            .SetDelay(0.35f);


        AnimateKeypad();
    }

    private void AnimateKeypad()
    {
        for (int i = 0; i < keypadButtons.Length; i++)
        {
            if (keypadButtons[i] == null)
                continue;

            RectTransform button =
                keypadButtons[i];

            button.localScale =
                Vector3.one * 0.7f;

            button
                .DOScale(
                    1f,
                    0.25f
                )
                .SetEase(Ease.OutBack)
                .SetDelay(
                    0.4f +
                    i * keypadStagger
                );
        }
    }

    public void CorrectAnswer()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            questionPanel
                .DOScale(1.03f, 0.12f)
        );

        sequence.Append(
            questionPanel
                .DOScale(1f, 0.18f)
        );
    }

    public void WrongAnswer()
    {
        questionPanel
            .DOShakeAnchorPos(
                0.35f,
                new Vector2(15f, 0f),
                12,
                90f
            );
    }

    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}