using System;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// UPDATE THE wIN PANEL 
//HOME FUCTIONALITY ,
//RetryAttribute fUNCTIONALITY WITTH ADS
// NEXT LEVEL SHOW ADS AFTER 2 LEVEL  
public class WinPanelManager : MonoBehaviour
{
    [Header("WinPanelData")]
    [SerializeField] private Sprite winBanner;

    [Header("LossPanelData")]
    [SerializeField] private Sprite lossBanner;
    [SerializeField] private TextMeshProUGUI coinTxt, timerTxt, starTxt;
    [SerializeField] private Button homebtn, retrybtn, nextlevelBtn;
    [SerializeField] private Image banner;
    [SerializeField] private Transform gamePanel;
    [SerializeField] private LevelListData levelListData;

    public void Init(string _cointext, string _timertxt, string _starTxy, bool status)
    {
        coinTxt.text = "Reward \n +" + _cointext;
        timerTxt.text = "TIME LEFT \n" + _timertxt;
        starTxt.text = "SATR EARNED \n" + _starTxy;
        if (status)
        {
            banner.sprite = winBanner;
            coinTxt.text = "Reward \n +" + _cointext;
        }
        else
        {
            banner.sprite = lossBanner;
            coinTxt.text = "Reward \n -" + _cointext;
        }
    }
    void Start()
    {
        // AdMobManager.Instance.ShowBanner();
    }

    void OnEnable()
    {
        homebtn?.onClick.AddListener(HomeButtonCLicked);
        retrybtn?.onClick.AddListener(RetryButtonClicked);
        nextlevelBtn?.onClick.AddListener(NextlevelBtnClicked);
    }

    void OnDisable()
    {
        homebtn.onClick.RemoveAllListeners();
        retrybtn.onClick.RemoveAllListeners();
        nextlevelBtn.onClick.RemoveAllListeners();
    }

    private void NextlevelBtnClicked()
    {
        AdMobManager.Instance.ShowNativeAdvanced();
        gamePanel.gameObject.SetActive(true);
        //gamePanel.GetComponent<Game>().SetLevelData(levelListData.levelDataSOs[PlayerDataManager.Instance.data.CurrentLevel - 1]);
        gameObject.SetActive(false);

    }

    private void RetryButtonClicked()
    {
        AdMobManager.Instance.TryShowInterstitial();
        gamePanel.gameObject.SetActive(true);
        //gamePanel.GetComponent<Game>().SetLevelData(levelListData.levelDataSOs[PlayerDataManager.Instance.data.CurrentLevel - 2]);
        gameObject.SetActive(false);
    }

    private void HomeButtonCLicked()
    {


        AdMobManager.Instance.ShowRewarded(() => UnityEngine.SceneManagement.SceneManager.LoadScene(1));
    }
}
