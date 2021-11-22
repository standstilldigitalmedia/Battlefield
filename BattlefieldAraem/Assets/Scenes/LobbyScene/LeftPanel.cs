using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftPanel : MonoBehaviour
{
    [SerializeField] CurrencyPanel currencyPanel;
    [SerializeField] Button baseRaceButton;
    [SerializeField] Button daglareshButton;
    [SerializeField] Button shinRaButton;
    [SerializeField] Button hakarButton;
    [SerializeField] Button iritarButton;
    [SerializeField] Button jarethAllianceButton;
    [SerializeField] Button runesButton;
    [SerializeField] Button ciphersButton;
    [SerializeField] Button chrysalisButton;
    [SerializeField] Button mainspringsButton;
    [SerializeField] Button armyButton;
    [SerializeField] Button craftingButton;
    [SerializeField] Button equipmentButton;
    [SerializeField] Button mainMenuButton;

    public delegate void RightPanelInit();
    public static event RightPanelInit RightPanelInited;

    public delegate void BaseRaceClick();
    public static event BaseRaceClick BaseRaceClicked;

    public delegate void DaglareshClick();
    public static event DaglareshClick DaglareshClicked;

    public delegate void ShinRaClick();
    public static event ShinRaClick ShinRaClicked;

    public delegate void HakarClick();
    public static event HakarClick HakarClicked;

    public delegate void IritarClick();
    public static event IritarClick IritarClicked;

    public delegate void JarethAllianceClick();
    public static event JarethAllianceClick JarethAllianceClicked;

    public delegate void ClearAllPanels();
    public static event ClearAllPanels ClearAllPanelsClick;

    public delegate void CiphersClick();
    public static event CiphersClick CipherButtonClick;

    public delegate void MainspringClick();
    public static event MainspringClick MainspringClicked;

    public delegate void ChrysalisClick();
    public static event ChrysalisClick ChrysalisClicked;

    public delegate void MainMenuClick();
    public static event MainMenuClick MainMenuClicked;
    void InitPanel()
    {
        gameObject.SetActive(true);
        RightPanelInited?.Invoke();
    }

    void OnBaseRaceButtonClick()
    {
        BaseRaceClicked?.Invoke();
    }

    void OnDaglareshButtonClick()
    {
        DaglareshClicked?.Invoke();
    }

    void OnShinRaButtonClick()
    {
        ShinRaClicked?.Invoke();
    }

    void OnHakarButtonClick()
    {
        HakarClicked?.Invoke();
    }

    void OnIritarButtonClick()
    {
        IritarClicked?.Invoke();
    }

    void OnJarethAllianceButtonClick()
    {
        JarethAllianceClicked?.Invoke();
    }

    void OnRunesButtonClick()
    {
        ClearAllPanelsClick?.Invoke();
    }

    void OnCiphersButtonClick()
    {
        Debug.Log("ciphers clicked");
        ClearAllPanelsClick?.Invoke();
        CipherButtonClick?.Invoke();
    }

    void OnChrysalisButtonClick()
    {
        Debug.Log("Chrylasis clicked");
        ClearAllPanelsClick?.Invoke();
        ChrysalisClicked?.Invoke();
    }

    void OnMainspringsButtonClick()
    {
        Debug.Log("Mainsrpings clicked");
        ClearAllPanelsClick?.Invoke();
        MainspringClicked?.Invoke();
    }

    void OnArmyButtonClick()
    {
        ClearAllPanelsClick?.Invoke();
    }

    void OnCraftingButtonClick()
    {
        ClearAllPanelsClick?.Invoke();
    }

    void OnEquipmentButtonClick()
    {
        ClearAllPanelsClick?.Invoke();
    }

    void OnMainMenuButtonClick()
    {
        ClearAllPanelsClick?.Invoke();
        //UnSetListners();
        MainMenuClicked?.Invoke();
        gameObject.SetActive(false);
    }

    void SetListners()
    {
        baseRaceButton.onClick.AddListener(OnBaseRaceButtonClick);
        daglareshButton.onClick.AddListener(OnDaglareshButtonClick);
        shinRaButton.onClick.AddListener(OnShinRaButtonClick);
        hakarButton.onClick.AddListener(OnHakarButtonClick);
        iritarButton.onClick.AddListener(OnIritarButtonClick);
        jarethAllianceButton.onClick.AddListener(OnJarethAllianceButtonClick);
        runesButton.onClick.AddListener(OnRunesButtonClick);
        ciphersButton.onClick.AddListener(OnCiphersButtonClick);
        chrysalisButton.onClick.AddListener(OnChrysalisButtonClick);
        mainspringsButton.onClick.AddListener(OnMainspringsButtonClick);
        armyButton.onClick.AddListener(OnArmyButtonClick);
        craftingButton.onClick.AddListener(OnCraftingButtonClick);
        equipmentButton.onClick.AddListener(OnEquipmentButtonClick);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    void UnSetListners()
    {
        baseRaceButton.onClick.RemoveListener(OnBaseRaceButtonClick);
        daglareshButton.onClick.RemoveListener(OnDaglareshButtonClick);
        shinRaButton.onClick.RemoveListener(OnShinRaButtonClick);
        hakarButton.onClick.RemoveListener(OnHakarButtonClick);
        iritarButton.onClick.RemoveListener(OnIritarButtonClick);
        jarethAllianceButton.onClick.RemoveListener(OnJarethAllianceButtonClick);
        runesButton.onClick.RemoveListener(OnRunesButtonClick);
        ciphersButton.onClick.RemoveListener(OnCiphersButtonClick);
        chrysalisButton.onClick.RemoveListener(OnChrysalisButtonClick);
        mainspringsButton.onClick.RemoveListener(OnMainspringsButtonClick);
        armyButton.onClick.RemoveListener(OnArmyButtonClick);
        craftingButton.onClick.RemoveListener(OnCraftingButtonClick);
        equipmentButton.onClick.RemoveListener(OnEquipmentButtonClick);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
    }

    private void Start()
    {
        SetListners();
        MainMenu.OnInventoryClick += InitPanel;
        ClearAllPanelsClick?.Invoke();
        gameObject.SetActive(false);
    }
}
