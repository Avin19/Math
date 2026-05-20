using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Transform unlocked;
    [SerializeField] private int starEarned;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private Transform[] starTransfrom;
    private Transform gamePanel;
    private Transform levelPanel;



    private LevelDataSO levelDataSO;
    void OnEnable()
    {
        GetComponent<Button>()?.onClick.AddListener(LoadGamePanel);
    }

    private void LoadGamePanel()
    {
        gamePanel.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(false);
    }

    void Start()
    {
        ButtonSetup();
    }
    private void ButtonSetup()
    {
        unlocked.gameObject.SetActive(!levelDataSO.unlocked);
        levelNumber.text = levelDataSO.Level.ToString();
        starEarned = levelDataSO.starEarned;
        gamePanel.GetComponent<Game>().SetLevelData(levelDataSO);
        StarCalculation();
    }
    public void SetLevelDataSO(LevelDataSO _levelDataSO, Transform _gamePanel, Transform _levelPanel)
    {
        levelDataSO = _levelDataSO;
        gamePanel = _gamePanel;
        levelPanel = _levelPanel;
        ButtonSetup();
    }
    private void StarCalculation()
    {
        for (int i = 0; i < starEarned - 1; i++)
        {
            starTransfrom[i].gameObject.SetActive(true);
        }
    }
}
