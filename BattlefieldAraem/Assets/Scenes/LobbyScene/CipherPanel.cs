using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CipherPanel : BaseInventoryPanel
{
    [SerializeField] GameObject runePanelPrefab;
    [SerializeField] GameObject inputPanelPrefab;
    [SerializeField] GameObject runePrefab;
    [SerializeField] GameObject inputPrefab;
    //[SerializeField] Transform cipherPanelTransform;
    public List<GameObject> spawned = new List<GameObject>();

    int FindLastSpace(char[] lastString)
    {
        int lastSpace = 0;
        for(int a = 0; a < lastString.Length; a++)
        {
            if(a < 20 && lastString[a] == ' ')
            {
                lastSpace = a;
            }
        }
        return lastSpace;
    }

    string CreateStringWithBuffer(string fullString, int lastSpace)
    {
        if(lastSpace == 0)
        {
            lastSpace = fullString.Length;
        }
        char[] fullCharArray = fullString.ToCharArray();
        string returnString = "";
        int buffer = (int)Mathf.Floor((20 - lastSpace) / 2);
        for (int a = 0; a < buffer; a++)
        {
            returnString += " ";
        }
        for (int b = 0; b < lastSpace; b++)
        {
            returnString += fullCharArray[b];
        }
        for (int c = 0; c < buffer; c++)
        {
            returnString += " ";
        }
        return returnString;
    }

    List<string> BreakCharIntoRows(string fullString)
    {
        List<string> stringList = new List<string>();
        string leftoverString = fullString.Substring(0,fullString.Length);

        while(leftoverString.Length > 0)
        {
            int lastSpace = FindLastSpace(leftoverString.ToCharArray());
            
            string tempString;
            if(lastSpace == 0)
            {
                tempString = leftoverString.Substring(0,leftoverString.Length);
                leftoverString = "";
            }
            else
            {
                
                tempString = leftoverString.Substring(0, lastSpace);
                leftoverString = leftoverString.Substring(tempString.Length + 1);                
            }
            tempString = CreateStringWithBuffer(tempString, lastSpace);
            stringList.Add(tempString);
        }
        return stringList;
    }

    void CreateRows(List<string> stringList)
    {
        foreach (string objString in stringList)
        {
            GameObject tempRunePanel = Instantiate(runePanelPrefab, panel.transform);
            GameObject tempInputPanel = Instantiate(inputPanelPrefab, panel.transform);
            spawned.Add(tempRunePanel);
            spawned.Add(tempInputPanel);
            char[] stringArray = objString.ToCharArray();
            for(int a = 0; a < objString.Length; a++)
            {
                GameObject tempRune = Instantiate(runePrefab, tempRunePanel.transform);
                GameObject tempInput= Instantiate(inputPrefab, tempInputPanel.transform);
                spawned.Add(tempRune);
                spawned.Add(tempInput);

                tempRune.transform.GetChild(0).GetComponent<TMP_Text>().text = stringArray[a].ToString();
                tempInput.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "";

                if (stringArray[a] == ' ')
                {
                    Color transparent = new Color(0, 0, 0, 0);
                    tempRune.GetComponent<Image>().color = transparent;
                    tempInput.GetComponent<InputField>().image.GetComponent<Image>().color = transparent;
                }
                else
                {
                    tempRune.transform.GetChild(0).GetComponent<TMP_Text>().text = stringArray[a].ToString();                    
                }
            }
        }
    }

    void ClearSpawned()
    {
        foreach (GameObject go in spawned)
        {
            if (go.gameObject)
            {
                Destroy(go);
            }
        }
        spawned.Clear();
    }

    public override void SetListeners()
    {

    }

    public override void UnSetListeners()
    {

    }

    public override void RaceChange()
    {
        
    }

    public override void BaseStart()
    {
        LeftPanel.CipherButtonClick += InitPanel;
    }

    public override void BaseInitPanel()
    {
        string testString = "THISISATESTTO SEE IF I CAN GET THIS THING TO WORK PROPERLY";
        char[] testCharArray = testString.ToCharArray();
        List<string> stringList = BreakCharIntoRows(testString);
        CreateRows(stringList);
    }

    public override void BaseClearPanel()
    {
        ClearSpawned();
    }
}
