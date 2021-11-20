using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button shopButton;
    public Button settingsButton;
    public Button campaignButton;
    public Button battleButton;    
    public Button inventoryButton;
    public Button questsButton;

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

    private void OnEnable()
    {
        SetListeners();
    }
}
