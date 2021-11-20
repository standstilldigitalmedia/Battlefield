using Sfs2X;
using Sfs2X.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFSConnection : MonoBehaviour
{
    public static SFSConnection Instance { get; private set; }

    public SmartFox sfs;
    public string host = "127.0.0.1";
    public int port = 9933;
    public string zone = "Battlefield";

    void OnConnectionLost(BaseEvent evt)
    {
        sfs.RemoveAllEventListeners();
        sfs = null;
        string[] questionArray = { "The connection to the server has been lost. Please check your internet connection and try again" };
        GameObject modalPanel = Instantiate(Resources.Load("ModalPanel"), GameObject.Find("Canvas").transform) as GameObject;
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails { sentences = questionArray, title = "Connection Lost" };
        modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Ok", action = GameManager.Instance.LoadLoginScene };
        modalPanel.GetComponent<ModalPanel>().NewChoice(modalPanelDetails);
    }

    public void SetSFSListeners()
    {
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
    }

    void OnApplicationQuit()
    {
        if (sfs != null && sfs.IsConnected)
        {
            sfs.RemoveAllEventListeners();
            sfs.Disconnect();
        } 
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (sfs != null)
            sfs.ProcessEvents();
    }
}
