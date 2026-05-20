using System;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    [SerializeField] private LevelListData levelListData;
    [SerializeField] private Transform levelButtonpf;
    [SerializeField] private Transform levelHolder;
    [SerializeField] private Transform gamePanel;
    [SerializeField] private Transform levelPanel;



    void Start()
    {
        foreach (LevelDataSO level in levelListData.levelDataSOs)
        {
            Transform created = Instantiate(levelButtonpf, levelHolder);
            created.GetComponent<ButtonController>().SetLevelDataSO(level, gamePanel, levelPanel);
        }
    }

}
