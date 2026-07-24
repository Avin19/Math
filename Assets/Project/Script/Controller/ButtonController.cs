using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Transform unlocked;
    [SerializeField] private int starEarned;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private Transform[] starTransfrom;

    [SerializeField] private LevelDataSO levelDataSO;

    [SerializeField] private Button onButtonClicked;

    void Start()
    {
        ButtonSetup();
    }
    void OnEnable()
    {
        onButtonClicked?.onClick.AddListener(() => LevelButtonClicked());
    }

    private void LevelButtonClicked()
    {
        PlayerDataManager.Instance.data.selectedLevel = levelDataSO.Level - 1;
        SceneManager.LoadScene(2);
    }

    private void ButtonSetup()
    {
        levelNumber.text = levelDataSO.Level.ToString();
        starEarned = levelDataSO.starEarned;
        StarCalculation();
    }
    public void SetLevelDataSO(LevelDataSO _levelDataSO)
    {
        levelDataSO = _levelDataSO;

        ButtonSetup();
    }
    public void LevelUnlocked(bool status)
    {
        unlocked.gameObject.SetActive(status);
    }
    private void StarCalculation()
    {
        for (int i = 0; i < starEarned; i++)
        {
            starTransfrom[i].gameObject.SetActive(true);
        }
    }
}
