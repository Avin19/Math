using System;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    [SerializeField] private LevelListData levelListData;
    [SerializeField] private Transform levelButtonpf;
    [SerializeField] private Transform levelHolder;





    void Start()
    {
        foreach (LevelDataSO level in levelListData.levelDataSOs)
        {
            Transform created = Instantiate(levelButtonpf, levelHolder);
            created.GetComponent<ButtonController>().SetLevelDataSO(level);

            if (levelListData.levelDataSOs.IndexOf(level) < PlayerDataManager.Instance.data.CurrentLevel)
            {
                created.GetComponent<ButtonController>().LevelUnlocked(false);
                foreach (PlayerLevelData pl in PlayerDataManager.Instance.data.playerLevelData)
                {
                    if (levelListData.levelDataSOs.IndexOf(level) == pl.level - 1)
                    {
                        created.GetComponent<ButtonController>().StarEarned(pl.startEarned);
                    }

                }
            }
            else
            {
                created.GetComponent<ButtonController>().LevelUnlocked(true);
            }
        }
    }



}
