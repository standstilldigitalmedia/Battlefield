using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventButtonDetails
{
    public string buttonTitle;
    public UnityAction action;
}

public class ModalPanelDetails
{
    public string title;
    public Image iconImage;
    public string[] sentences;
    public EventButtonDetails button1Details;
    public EventButtonDetails button2Details;
    public EventButtonDetails button3Details;
}

public class ModalPanel : MonoBehaviour
{
    public TMP_Text titleText;
    public Image iconImage;
    public TMP_Text question;
    public Button button1;
    public Button button2;
    public Button button3;

    public TMP_Text button1Text;
    public TMP_Text button2Text;
    public TMP_Text button3Text;
    
    public GameObject modalPanelObject;

    public string[] sentences;

    Queue<string> sentenceQueue;

    void ClosePanel()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

    public void NewChoice(ModalPanelDetails details)
    {
        modalPanelObject.SetActive(true);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);

        titleText.text = details.title;
        if(details.iconImage)
        {
            iconImage.sprite = details.iconImage.sprite;
        }
        else
        {
            iconImage.gameObject.SetActive(false);
        }
        sentences = details.sentences;
        StartDialogue();

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(details.button1Details.action);
        //button1.onClick.AddListener(ClosePanel);
        button1Text.text = details.button1Details.buttonTitle;
        button1.gameObject.SetActive(true);

        if(details.button2Details != null)
        {
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(details.button2Details.action);
            //button2.onClick.AddListener(ClosePanel);
            button2Text.text = details.button2Details.buttonTitle;
            button2.gameObject.SetActive(true);
        }

        if (details.button3Details != null)
        {
            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(details.button3Details.action);
           // button3.onClick.AddListener(ClosePanel);
            button3Text.text = details.button3Details.buttonTitle;
            button3.gameObject.SetActive(true);
        }

    }

    public void DisplayNextSentence()
    {
        if (sentenceQueue.Count == 0)
        {
            ClosePanel();
            return;
        }

        question.text = sentenceQueue.Dequeue();
    }

    public void StartDialogue()
    {
        sentenceQueue.Clear();
        foreach (string sentence in sentences)
        {
            sentenceQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void OnEnable()
    {
        transform.SetAsLastSibling();
        sentenceQueue = new Queue<string>();
    }
}
