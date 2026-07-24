using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button levelBtn;
    [SerializeField] private Button settingBtn;

    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform levelPanel;
    [SerializeField] private RectTransform settingPanel;

    void OnEnable()
    {
        playBtn?.onClick.AddListener(() => PlayButtonClicked());
        levelBtn?.onClick.AddListener(() => LevelPanelOpen());
        settingBtn?.onClick.AddListener(() => SettingPanel());
    }

    void OnDisable()
    {
        playBtn.onClick.RemoveAllListeners();
        levelBtn.onClick.RemoveAllListeners();
        settingBtn.onClick.RemoveAllListeners();
    }

    private void SetAllPanelFalse()
    {
        mainPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(false);
    }
    private void SettingPanel()
    {
        SetAllPanelFalse();
        settingPanel.gameObject.SetActive(true);
    }

    private void LevelPanelOpen()
    {
        SetAllPanelFalse();
        levelPanel.gameObject.SetActive(true);
    }

    private void PlayButtonClicked()
    {
        SetAllPanelFalse();
        SceneManager.LoadScene(2);
    }
}
