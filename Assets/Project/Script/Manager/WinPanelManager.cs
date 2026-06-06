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
        throw new NotImplementedException();
    }

    private void RetryButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void HomeButtonCLicked()
    {
        throw new NotImplementedException();
    }
}
