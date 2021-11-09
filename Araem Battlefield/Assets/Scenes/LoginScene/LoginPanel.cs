using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Util;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public delegate void SwitchToSignUpAction();
    public static event SwitchToSignUpAction SwitchToSignupClicked;
    
    InputField loginUsernameInput;
    InputField loginPasswordInput;
    Toggle rememberMeToggle;
    Button forgotPasswordButton;
    Text loginErrorText;
    Button loginButton;
    Button createAccountButton;
    Button loginCloseButton;
    GameObject connectingPanel;

    void ValidateForm(bool validation)
    {
        bool activeButton = false;
        if (validation)
        {
            if (InputValidator.ValidateUsername(loginUsernameInput.text, loginErrorText))
            {
                if (InputValidator.ValidatePassword(loginPasswordInput.text, loginErrorText))
                {
                    loginErrorText.text = "";
                    activeButton = true;                            
                }
            }
        }
        loginButton.interactable = activeButton;
    }

    void ValidateUsername(string s)
    {
        ValidateForm(InputValidator.ValidateUsername(loginUsernameInput.text, loginErrorText));
    }

    void ValidatePassword(string s)
    {
        ValidateForm(InputValidator.ValidatePassword(loginPasswordInput.text, loginErrorText));
    }

    void OnForgotPasswordButtonClick()
    {

    }

    void ActivateSignUpPanel()
    {
        SwitchToSignupClicked?.Invoke();
    }

    void OnLoginButtonClick()
    {
        connectingPanel = Instantiate(Resources.Load("LoadingPanel"), GameObject.Find("Canvas").transform) as GameObject;
        connectingPanel.GetComponent<LoadingPanel>().SetText("Connecting...");
        connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.0f);

        ConfigData cfg = new ConfigData();
        cfg.Host = SFSConnection.Instance.host;
        cfg.Port = SFSConnection.Instance.port;
        cfg.Zone = SFSConnection.Instance.zone;
        Debug.Log("connection to " + SFSConnection.Instance.zone);

        SFSConnection.Instance.sfs = new SmartFox();

        SetSFSListeners();

        SFSConnection.Instance.sfs.Connect(cfg);
    }

    void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.25f);
            connectingPanel.GetComponent<LoadingPanel>().SetText("Connected. Logging in.");
            SFSConnection.Instance.sfs.Send(new LoginRequest(loginUsernameInput.text, PasswordUtil.MD5Password(loginPasswordInput.text)));
        }
        else
        {
            loginErrorText.text = "There was a connection problem. Please check your internet connection and try again.";
            Disconnect();
            DestroyConnectingPanel();
        }
    }

    void OnLogin(BaseEvent evt)
    {
        if (rememberMeToggle.isOn)
        {
            PlayerPrefs.SetString("username", loginUsernameInput.text);
            PlayerPrefs.SetString("token", loginPasswordInput.text);
        }
        connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.50f);
        connectingPanel.GetComponent<LoadingPanel>().SetText("Logged In. Waiting for room.");
        //SFSConnection.Instance.sfs.Send(new JoinRoomRequest("PreLobby"));
    }

    void OnLoginError(BaseEvent evt)
    {
        loginErrorText.gameObject.SetActive(true);
        loginErrorText.text = "Login failed: " + (string)evt.Params["errorMessage"];
        Disconnect();
        DestroyConnectingPanel();
    }

    void OnRoomJoin(BaseEvent evt)
    {
        Debug.Log("Room Joined");
        //ISFSObject objOut = new SFSObject();
        //SFSConnection.Instance.sfs.Send(new ExtensionRequest("i", objOut, SFSConnection.Instance.sfs.LastJoinedRoom));
        connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.75f);
        connectingPanel.GetComponent<LoadingPanel>().SetText("PreLobby joined.");
    }

    void OnRoomJoinError(BaseEvent evt)
    {
        DestroyConnectingPanel();
        loginErrorText.text = "Room join failed: " + (string)evt.Params["errorMessage"];
    }

    void OnExtensionResponse(BaseEvent evt)
    {
        DestroyConnectingPanel();
        string cmd = (string)evt.Params["cmd"];
        ISFSObject dataObject = (SFSObject)evt.Params["params"];

        if (cmd == "i")
        {
            //Player.Instance.inventoryObject = dataObject;
            connectingPanel.GetComponent<LoadingPanel>().UpdateProgress(0.95f);
            DestroyConnectingPanel();
            connectingPanel = Instantiate(Resources.Load("LoadingPanel"), GameObject.Find("Canvas").transform) as GameObject;
            StartCoroutine(connectingPanel.GetComponent<LoadingPanel>().AsynchronousLoad("LobbyScene"));
        }
    }

    void FindInputs()
    {
        loginUsernameInput = GameObject.Find("LoginUsernameInput").GetComponent<InputField>();
        loginPasswordInput = GameObject.Find("LoginPasswordInput").GetComponent<InputField>();
        rememberMeToggle = GameObject.Find("RememberMeToggle").GetComponent<Toggle>();
        forgotPasswordButton = GameObject.Find("ForgotPasswordButton").GetComponent<Button>();
        loginErrorText = GameObject.Find("LoginErrorText").GetComponent<Text>();
        loginButton = GameObject.Find("LoginButton").GetComponent<Button>();
        createAccountButton = GameObject.Find("CreateAccountButton").GetComponent<Button>();
        loginCloseButton = GameObject.Find("LoginCloseButton").GetComponent<Button>();
    }

    void LoseInputs()
    {
        loginUsernameInput = null;
        loginPasswordInput = null;
        rememberMeToggle = null;
        forgotPasswordButton = null;
        loginErrorText = null;
        loginButton = null;
        createAccountButton = null;
        loginCloseButton = null;
        
        connectingPanel = null;
    }

    void ClearInputs()
    {
        loginUsernameInput.text = "";
        loginPasswordInput.text = "";
        rememberMeToggle.isOn = false;

        loginButton.interactable = false;
    }

    void OnQuitButtonClick()
    {
        GameManager.Instance.OnQuitButtonClick();
    }

    void SetSFSListeners()
    {
        if (SFSConnection.Instance.sfs != null)
        {
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
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
            SFSConnection.Instance.sfs.RemoveEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
            SFSConnection.Instance.sfs.RemoveEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
            SFSConnection.Instance.sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
        }
    }

    void SetInputListeners()
    {
        loginUsernameInput.onValueChanged.AddListener(ValidateUsername);
        loginPasswordInput.onValueChanged.AddListener(ValidatePassword);
        forgotPasswordButton.onClick.AddListener(OnForgotPasswordButtonClick);
        loginButton.onClick.AddListener(OnLoginButtonClick);
        createAccountButton.onClick.AddListener(ActivateSignUpPanel);
        loginCloseButton.onClick.AddListener(OnQuitButtonClick);
    }

    void UnsetInputListeners()
    {
        loginUsernameInput.onValueChanged.RemoveListener(ValidateUsername);
        loginPasswordInput.onValueChanged.RemoveListener(ValidatePassword);
        forgotPasswordButton.onClick.RemoveListener(OnForgotPasswordButtonClick);
        loginButton.onClick.RemoveListener(OnLoginButtonClick);
        createAccountButton.onClick.RemoveListener(ActivateSignUpPanel);
        loginCloseButton.onClick.RemoveListener(OnQuitButtonClick);
    }

    public void InitPanel()
    {
        FindInputs();
        ClearInputs();
        SetInputListeners();
        loginErrorText.text = "";
        if (PlayerPrefs.HasKey("username"))
        {
            loginUsernameInput.text = PlayerPrefs.GetString("username");
            loginPasswordInput.text = PlayerPrefs.GetString("token");
        }
    }

    public void ClosePanel()
    {
        loginErrorText.text = "";
        UnsetInputListeners();
        ClearInputs();
        LoseInputs();
        UnsetSFSListeners();
        gameObject.SetActive(false);
    }

    void DestroyConnectingPanel()
    {
        if (connectingPanel != null)
        {
            Destroy(connectingPanel);
        }
    }

    void Disconnect()
    {
        SFSConnection.Instance.sfs.RemoveAllEventListeners();
        DestroyConnectingPanel();
        if (SFSConnection.Instance.sfs != null && SFSConnection.Instance.sfs.IsConnected)
        {
            SFSConnection.Instance.sfs.Disconnect();
        }
    }
}