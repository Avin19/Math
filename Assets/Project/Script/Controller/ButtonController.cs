using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Transform unlocked;
    [SerializeField] private int starEarned;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private Transform[] starTransfrom;





    [SerializeField] private LevelDataSO levelDataSO;
    void OnEnable()
    {

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
        StarCalculation();
    }
    public void SetLevelDataSO(LevelDataSO _levelDataSO)
    {
        levelDataSO = _levelDataSO;

        ButtonSetup();
    }
    private void StarCalculation()
    {
        for (int i = 0; i < starEarned; i++)
        {
            starTransfrom[i].gameObject.SetActive(true);
        }
    }
}
