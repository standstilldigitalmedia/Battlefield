using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public void Ok()
    { }

    public void OnQuitButtonClick()
    {
        /*ModalPanelDetails modalPanelDetails = new ModalPanelDetails { question = "Are you sure you exit the game?", title = "Quit?" };
        modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Yes", action = OnApplicationQuit };
        modalPanelDetails.button2Details = new EventButtonDetails { buttonTitle = "No", action = Ok };
        GameObject modalPanel = Instantiate(Resources.Load("ModalPanel"), GameObject.Find("Canvas").transform) as GameObject;
        modalPanel.GetComponent<ModalPanel>().NewChoice(modalPanelDetails);*/
    }

    private void Awake()
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

    public void LoadLoginScene()
    {
        GameObject loadingPanel = Instantiate(Resources.Load("LoadingPanel"), GameObject.Find("Canvas").transform) as GameObject;
        StartCoroutine(loadingPanel.GetComponent<LoadingPanel>().AsynchronousLoad("LoginScene"));
    }
}
