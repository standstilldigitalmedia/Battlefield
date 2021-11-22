using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject spikesImage;

    void ClearPanel()
    {
        panel.gameObject.SetActive(false);
        spikesImage.SetActive(false);
    }

    void InitPanel()
    {
        panel.gameObject.SetActive(true);
        spikesImage.SetActive(true);
    }
    void Start()
    {
        LeftPanel.ClearAllPanelsClick += ClearPanel;
        LeftPanel.ChrysalisClicked += InitPanel;
        LeftPanel.CipherButtonClick += InitPanel;
        LeftPanel.MainspringClicked += InitPanel;
        LeftPanel.RightPanelInited += InitPanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
