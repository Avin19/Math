using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private LevelDataSO leveldata;
    [SerializeField] private TextMeshProUGUI leveldisplayText;

    [SerializeField] private Button enterBtn;
    [SerializeField] private Button[] numberBtns = new Button[10];
    [SerializeField] private Button dotBtns;
    [SerializeField] private Button clearBtn;
    private float timer;
    [SerializeField] private Slider slider;
    private string answer;
    private bool stoptimer = false;
    [SerializeField] private Transform winPanel;
    [SerializeField] private Transform lossPanel;


    public void SetLevelData(LevelDataSO _levelData)
    {
        Debug.Log("Test");
        leveldata = _levelData;
        Setup();
    }


    void OnEnable()
    {
        answer = "";
        answerText.text = "";
        enterBtn?.onClick.AddListener(CheckAnswer);

        numberBtns[0]?.onClick.AddListener(() => AnswerButtonPressed(1));
        numberBtns[1]?.onClick.AddListener(() => AnswerButtonPressed(2));
        numberBtns[2]?.onClick.AddListener(() => AnswerButtonPressed(3));
        numberBtns[3]?.onClick.AddListener(() => AnswerButtonPressed(4));
        numberBtns[4]?.onClick.AddListener(() => AnswerButtonPressed(5));
        numberBtns[5]?.onClick.AddListener(() => AnswerButtonPressed(6));
        numberBtns[6]?.onClick.AddListener(() => AnswerButtonPressed(7));
        numberBtns[7]?.onClick.AddListener(() => AnswerButtonPressed(8));
        numberBtns[8]?.onClick.AddListener(() => AnswerButtonPressed(9));
        numberBtns[9]?.onClick.AddListener(() => AnswerButtonPressed(0));
        dotBtns?.onClick.AddListener(() => DotButtonClick());
        clearBtn?.onClick.AddListener(() => ClearBtnClick());



    }

    private void ClearBtnClick()
    {
        if (string.IsNullOrEmpty(answer))
            return;

        answer = answer.Substring(0, answer.Length - 1);

        answerText.text = answer;
    }

    private void DotButtonClick()
    {
        if (answer.Contains(".")) return;
        answer = answer + ".";
        answerText.text = answer;

    }

    void OnDisable()
    {
        enterBtn.onClick.RemoveAllListeners();
        numberBtns[0]?.onClick.RemoveAllListeners();
        numberBtns[1]?.onClick.RemoveAllListeners();
        numberBtns[2]?.onClick.RemoveAllListeners();
        numberBtns[3]?.onClick.RemoveAllListeners();
        numberBtns[4]?.onClick.RemoveAllListeners();
        numberBtns[5]?.onClick.RemoveAllListeners();
        numberBtns[6]?.onClick.RemoveAllListeners();
        numberBtns[7]?.onClick.RemoveAllListeners();
        numberBtns[8]?.onClick.RemoveAllListeners();
        numberBtns[9]?.onClick.RemoveAllListeners();

    }
    private void AnswerButtonPressed(int l)
    {
        answer = answer + l.ToString();
        answerText.text = answer;

    }
    private void CheckAnswer()
    {
        if (string.IsNullOrEmpty(answer)) return;
        if (string.Equals(answer, leveldata.Answer))
        {
            //Correct Answer
            PlayerDataManager.Instance.data.CurrentLevel++;
        }
        else
        {
            //Anwers 
        }

        float remainingPercent = timer / leveldata.timelimit;

        if (remainingPercent >= 0.66f)
        {
            leveldata.starEarned = 3;
        }
        else if (remainingPercent >= 0.33f)
        {
            leveldata.starEarned = 2;
        }
        else if (remainingPercent > 0)
        {
            leveldata.starEarned = 1;
        }
        else
        {
            leveldata.starEarned = 0;
        }
    }

    private void Setup()
    {
        questionText.text = leveldata.Question;
        leveldisplayText.text = "LEVEL  " + leveldata.Level;
        timer = leveldata.timelimit;
        slider.maxValue = timer;


    }
    private void Update()
    {
        if (stoptimer)
            return;

        timer -= Time.deltaTime;

        timer = Mathf.Clamp(timer, 0, leveldata.timelimit);

        // Normalized slider value (0 → 1)
        slider.value = timer / leveldata.timelimit;

        if (timer <= 0)
        {
            timer = 0;
            stoptimer = true;

            OnTimeOver();
        }
    }

    private void OnTimeOver()
    {
        throw new NotImplementedException();
    }
    private void WinCurrentLevel()
    {

    }
    private void LossCurrentLevel()
    {

    }
    private void MaxlevelReached()
    {

    }
}
