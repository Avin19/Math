using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private LevelDataSO leveldata;

    [SerializeField] private Button enterBtn;
    [SerializeField] private Button[] numberBtns = new Button[10];


    public void SetLevelData(LevelDataSO _levelData)
    {
        leveldata = _levelData;
    }
}
