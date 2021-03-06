using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignUpPanel : BasePanel
{
    public delegate void SwitchToLoginAction();
    public static event SwitchToLoginAction SwitchToLoginClicked;

    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField signUpUsernameInput;
    [SerializeField] TMP_InputField signUpPasswordInput;
    [SerializeField] TMP_InputField rePasswordInput;
    [SerializeField] Toggle termsToggle;
    [SerializeField] TMP_Text signUpErrorText;
    [SerializeField] Button signUpButton;
    [SerializeField] Button backToLoginButton;
    [SerializeField] Button signUpCloseButton;
    GameObject connectingPanel;
    Canvas myCanvas;
    void ClearInputs()
    {
        emailInput.text = "";
        signUpUsernameInput.text = "";
        signUpPasswordInput.text = "";
        rePasswordInput.text = "";

        termsToggle.isOn = false;
        signUpButton.interactable = false;
    }

    void DestroyConnectingPanel()
    {
        if (connectingPanel != null)
        {
            Destroy(connectingPanel);
        }
    }


    #region InputValidation
    void ValidateForm(bool validation)
    {
        bool activeButton = false;
        if (validation)
        {
            if (InputValidator.ValidateUsername(signUpUsernameInput.text, signUpErrorText))
            {
                if (InputValidator.ValidatePassword(signUpPasswordInput.text, signUpErrorText))
                {
                    if (InputValidator.ValidateRePassword(signUpPasswordInput.text, rePasswordInput.text, signUpErrorText))
                    {
                        if (InputValidator.ValidateEmail(emailInput.text, signUpErrorText))
                        {
                            if (InputValidator.ValidateTermsToggle(termsToggle, signUpErrorText))
                            {
                                signUpErrorText.text = "";
                                activeButton = true;
                            }
                        }
                    }
                }
            }
        }
        signUpButton.interactable = activeButton;
    }

    void ValidateUsername(string s)
    {
        ValidateForm(InputValidator.ValidateUsername(signUpUsernameInput.text, signUpErrorText));
    }

    void ValidatePassword(string s)
    {
        ValidateForm(InputValidator.ValidatePassword(signUpPasswordInput.text, signUpErrorText));
    }

    void ValidateRePassword(string s)
    {
        ValidateForm(InputValidator.ValidateRePassword(signUpPasswordInput.text, rePasswordInput.text, signUpErrorText));
    }

    void ValidateEmail(string s)
    {
        ValidateForm(InputValidator.ValidateEmail(emailInput.text, signUpErrorText));
    }

    void ValidateTermsToggle(bool s)
    {
        ValidateForm(InputValidator.ValidateTermsToggle(termsToggle, signUpErrorText));
    }
    #endregion

    #region Smartfox
    void Disconnect()
    {
        SFSConnection.Instance.sfs.RemoveAllEventListeners();
        DestroyConnectingPanel();
        if (SFSConnection.Instance.sfs != null && SFSConnection.Instance.sfs.IsConnected)
        {
            SFSConnection.Instance.sfs.Disconnect();
        }
    }

    private void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.25f);
            SFSConnection.Instance.sfs.Send(new LoginRequest("", "", SFSConnection.Instance.zone));
        }
        else
        {
            signUpErrorText.text = "There was a connection problem. Please check your internet connection and try again.";
            Disconnect();
            DestroyConnectingPanel();
        }
    }

    void OnLogin(BaseEvent evt)
    {
        connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.60f);
        ISFSObject objOut = new SFSObject();
        objOut.PutUtfString("username", signUpUsernameInput.text);
        objOut.PutUtfString("password", signUpPasswordInput.text);
        objOut.PutUtfString("r", rePasswordInput.text);
        objOut.PutUtfString("email", emailInput.text);
        SFSConnection.Instance.sfs.Send(new ExtensionRequest("$SignUp.Submit", objOut));
    }

    private void OnLoginError(BaseEvent evt)
    {
        signUpErrorText.text = "Login failed: " + (string)evt.Params["errorMessage"];
        Disconnect();
        DestroyConnectingPanel();
    }

    void OnExtensionResponse(BaseEvent evt)
    {
        DestroyConnectingPanel();
        string cmd = (string)evt.Params["cmd"];
        ISFSObject dataObject = (SFSObject)evt.Params["params"];

        if (cmd == "$SignUp.Submit")
        {
            SFSConnection.Instance.sfs.RemoveAllEventListeners();
            connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.99f);
            DestroyConnectingPanel();

            if (dataObject.ContainsKey("success"))
            {
                rePasswordInput.text = "";
                emailInput.text = "";

                string[] questionArray = { "Your account was successfully created. You may use it to login now." };
                GameObject modalPanel = Instantiate(Resources.Load("ModalPanel"), GameObject.Find("Canvas").transform) as GameObject;
                ModalPanelDetails modalPanelDetails = new ModalPanelDetails { sentences = questionArray, title = "Account Created" };
                modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Ok", action = OnSwitchToLoginClicked };
                modalPanel.GetComponent<ModalPanel>().NewChoice(modalPanelDetails);
            }
            else if (dataObject.ContainsKey("errorMessage"))
            {
                string[] questionArray = { dataObject.GetUtfString("errorMessage") };
                GameObject modalPanel = Instantiate(Resources.Load("ModalPanel"), GameObject.Find("Canvas").transform) as GameObject;
                ModalPanelDetails modalPanelDetails = new ModalPanelDetails { sentences = questionArray, title = "Account Creation Error" };
                modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Ok", action = GameManager.Instance.Ok };
                modalPanel.GetComponent<ModalPanel>().NewChoice(modalPanelDetails);
            }
            else
            {
                string[] questionArray = { "There was a problem with your account registration. Please try again." };
                GameObject modalPanel = Instantiate(Resources.Load("ModalPanel"), GameObject.Find("Canvas").transform) as GameObject;
                ModalPanelDetails modalPanelDetails = new ModalPanelDetails { sentences = questionArray, title = "Account Creation Error" };
                modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Ok", action = GameManager.Instance.Ok };
                modalPanel.GetComponent<ModalPanel>().NewChoice(modalPanelDetails);
            }
        }
    }
    #endregion

    #region Listeners
    void SetSFSListeners()
    {
        if (SFSConnection.Instance.sfs != null)
        {
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);

            SFSConnection.Instance.SetSFSListeners();
        }
    }

    void UnsetSFSListeners()
    {
        if (SFSConnection.Instance.sfs != null)
        {
            SFSConnection.Instance.sfs.RemoveEventListener(SFSEvent.CONNECTION, OnConnection);
            SFSConnection.Instance.sfs.RemoveEventListener(SFSEvent.LOGIN, OnLogin);
            SFSConnection.Instance.sfs.RemoveEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            SFSConnection.Instance.sfs.RemoveEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
        }
    }
    #endregion
    void OnSignUpButtonClick()
    {
        connectingPanel = Instantiate(Resources.Load("LoadingPanel"), GameObject.Find("Canvas").transform) as GameObject;
        connectingPanel.GetComponent<LoadingPanel>().SetText("CONNECTING...");
        connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.0f);

        ConfigData cfg = new ConfigData();
        cfg.Host = SFSConnection.Instance.host;
        cfg.Port = SFSConnection.Instance.port;
        cfg.Zone = SFSConnection.Instance.zone;

        SFSConnection.Instance.sfs = new SmartFox();

        SetSFSListeners();

        SFSConnection.Instance.sfs.Connect(cfg);
    }

    

    void OnQuitButtonClick()
    {
        GameManager.Instance.OnQuitButtonClick();
    }

    void OnSwitchToSigunpButtonClicked()
    {
        InitPanel();
    }

    void OnSwitchToLoginClicked()
    {
        SwitchToLoginClicked?.Invoke();
        ClearPanel();
    }

    public override void SetListeners()
    {
        emailInput.onValueChanged.AddListener(ValidateEmail);
        signUpUsernameInput.onValueChanged.AddListener(ValidateUsername);
        signUpPasswordInput.onValueChanged.AddListener(ValidatePassword);
        rePasswordInput.onValueChanged.AddListener(ValidateRePassword);

        termsToggle.onValueChanged.AddListener(ValidateTermsToggle);
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
        backToLoginButton.onClick.AddListener(OnSwitchToLoginClicked);

        signUpCloseButton.onClick.AddListener(OnQuitButtonClick);
    }

    public override void UnSetListeners()
    {
        emailInput.onValueChanged.RemoveListener(ValidateEmail);
        signUpUsernameInput.onValueChanged.RemoveListener(ValidateUsername);
        signUpPasswordInput.onValueChanged.RemoveListener(ValidatePassword);
        rePasswordInput.onValueChanged.RemoveListener(ValidateRePassword);

        termsToggle.onValueChanged.RemoveListener(ValidateTermsToggle);
        signUpButton.onClick.RemoveListener(OnSignUpButtonClick);
        backToLoginButton.onClick.RemoveListener(OnSwitchToLoginClicked);
        signUpCloseButton.onClick.RemoveListener(OnQuitButtonClick);
    }

    public override void OverideClearPanel()
    {
        signUpErrorText.text = "";
        signUpErrorText.gameObject.SetActive(false);
        ClearInputs();
        UnsetSFSListeners();
    }

    public override void OverrideInitPanel()
    {
        ClearInputs();
        signUpErrorText.text = "";
        signUpErrorText.gameObject.SetActive(false);
    }

    public override void OverrideStart()
    {
        LoginPanel.SwitchToSignupClicked += OnSwitchToSigunpButtonClicked;
    }
}