using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField]
    private TextMeshProUGUI timerTxt;
    [SerializeField] private LevelDataSO leveldata;
    [SerializeField] private LevelListData levelList;
    [SerializeField] private TextMeshProUGUI leveldisplayText;

    [SerializeField] private Button enterBtn;
    [SerializeField] private Button[] numberBtns = new Button[10];
    [SerializeField] private Button dotBtns;
    [SerializeField] private Button clearBtn;
    [SerializeField] private Button backBtn;
    private float timer;
    private float timepasssed;
    [SerializeField] private Slider slider;
    private string answer;
    private bool stoptimer = false;
    [SerializeField] private Transform winPanel;

    void Start()
    {
        leveldata = levelList.levelDataSOs[PlayerDataManager.Instance.data.selectedLevel];
        SetLevelData(leveldata);
    }

    public void SetLevelData(LevelDataSO _levelData)
    {

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
        backBtn?.onClick.AddListener(() => BackToMainMenu());

        ClearTHeINputBox();

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void ClearTHeINputBox()
    {
        answerText.text = "";
        answer = "";
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
        if (string.IsNullOrEmpty(answer)) return;
        AdMobManager.Instance.TryShowInterstitial();
        if (string.Equals(answer, leveldata.Answer))
        {
            //Correct Answer
            PlayerDataManager.Instance.data.CurrentLevel++;
            PlayerDataManager.Instance.data.Coins += 100;
            WinCurrentLevel();

        }
        else
        {
            PlayerDataManager.Instance.data.Coins -= 100;
            LossCurrentLevel();
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
        timepasssed += Time.deltaTime;
        timerTxt.text = TimeSpan.FromSeconds(timepasssed).ToString(@"m\:ss");

        timer = Mathf.Clamp(timer, 0, leveldata.timelimit);

        // Normalized slider value (0 → 1)
        slider.value = timer / leveldata.timelimit;

        // if (timer <= 0)
        // {
        //     timer = 0;
        //     stoptimer = true;

        //     OnTimeOver();
        // }
    }

    private void OnTimeOver()
    {
        LossCurrentLevel();
    }
    private void WinCurrentLevel()
    {

        winPanel.gameObject.SetActive(true);
        TimeSpan timeString = TimeSpan.FromSeconds(timepasssed);
        winPanel.gameObject.GetComponent<WinPanelManager>().Init(100.ToString(), timeString.ToString(@"m\:ss"), leveldata.starEarned.ToString(), true);
    }
    private void LossCurrentLevel()
    {
        winPanel.gameObject.SetActive(true);
        TimeSpan timeString = TimeSpan.FromSeconds(timepasssed);
        winPanel.gameObject.GetComponent<WinPanelManager>().Init(100.ToString(), timeString.ToString(@"m\:ss"), leveldata.starEarned.ToString(), false);
    }

}
