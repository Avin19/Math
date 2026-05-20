using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button playbtn;
    [SerializeField] private LevelDataManager levelDataManager;

    void OnEnable()
    {
        playbtn?.onClick.AddListener(() => StartGame(PlayerDataManager.Instance.data.CurrentLevel));
    }

    private void StartGame(int level)
    {

    }
}
