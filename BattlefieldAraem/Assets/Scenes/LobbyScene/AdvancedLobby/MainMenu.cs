using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button shopButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button campaignButton;
    [SerializeField] Button battleButton;
    [SerializeField] Button inventoryButton;
    [SerializeField] Button questsButton;

    public delegate void InventoryClick();
    public static event InventoryClick OnInventoryClick;

    void OnCampaignButtonClick()
    {
        
    }

    void OnBattleButtonClick()
    {

    }

    void OnShopButtonClick()
    {

    }

    void OnSettingsButtonClick()
    {

    }

    void OnInventoryButtonClick()
    {
        UnSetListeners();
        OnInventoryClick?.Invoke();
        gameObject.SetActive(false);
    }

    void OnQuestsButtonClick()
    {

    }

    void SetListeners()
    {
        campaignButton.onClick.AddListener(OnCampaignButtonClick);
        battleButton.onClick.AddListener(OnBattleButtonClick);
        shopButton.onClick.AddListener(OnShopButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        inventoryButton.onClick.AddListener(OnInventoryButtonClick);
        questsButton.onClick.AddListener(OnQuestsButtonClick);
    }

    void UnSetListeners()
    {
        campaignButton.onClick.RemoveListener(OnCampaignButtonClick);
        battleButton.onClick.RemoveListener(OnBattleButtonClick);
        shopButton.onClick.RemoveListener(OnShopButtonClick);
        settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        inventoryButton.onClick.RemoveListener(OnInventoryButtonClick);
        questsButton.onClick.RemoveListener(OnQuestsButtonClick);
    }

    public void InitPanel()
    {
        gameObject.SetActive(true);
        SetListeners();
    }

    private void Start()
    {
        InitPanel();        
        LeftPanel.MainMenuClicked += InitPanel;
    }
}
