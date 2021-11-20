using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    public TMP_Text headerText;
    public Image loadingImage;
    public TMP_Text loadingText;
    public TMP_Text quoteText;
    string[] quotes;

    public void SetText(string s)
    {
        if(headerText != null)
        {
            headerText.text = s;
        }
        if(quoteText != null)
        {
            quoteText.text = quotes[Random.Range(0, 5)];
        }
    }

    public void UpdateProgress(float progress)
    {
        if (loadingText != null)
        {
            loadingText.text = (progress * 100) + "%";
        }
        if (loadingImage != null)
        {
            loadingImage.transform.localScale = new Vector3(progress * 1, 1, 1);
        }
    }

    public IEnumerator AsynchronousLoad(string scene)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            yield return new WaitForSeconds(1);
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);

            UpdateProgress(progress);

            // Loading completed
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    private void SetQuoteStrings()
    {
        quotes = new string[6];
        quotes[0] = "Bombs can only be taken by a miner";
        quotes[1] = "Generals can only be taken by a spy";
        quotes[2] = "Spies can be taken by any piece";
        quotes[3] = "Scouts can move any number of spaces in a straight line";
        quotes[4] = "Take the flag, win the game";
        quotes[5] = "I'm running out of quotes";
    }

    private void Awake()
    {
        UpdateProgress(0f);
        SetQuoteStrings();
    }

    private void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}
