using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button playbtn;
    [SerializeField] private Transform gamePanel;
    [SerializeField] private LevelListData levelListData;

    void OnEnable()
    {
        playbtn?.onClick.AddListener(() => StartGame(PlayerDataManager.Instance.data.CurrentLevel));
    }

    private void StartGame(int level)
    {
        gamePanel.gameObject.SetActive(true);
        gamePanel.GetComponent<Game>().SetLevelData(levelListData.levelDataSOs[level]);

    }
}
