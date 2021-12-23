using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    public GameObject panel;
    public abstract void SetListeners();
    public abstract void UnSetListeners();
    public abstract void OverideClearPanel();
    public abstract void OverrideInitPanel();
    public abstract void OverrideStart();

    public void ClearPanel()
    {
        //UnSetListeners();
        panel.gameObject.SetActive(false); 
        OverideClearPanel();
    }

    public void InitPanel()
    {
        panel.gameObject.SetActive(true);
        OverrideInitPanel();
    }

   void Start()
    {
        SetListeners();
        panel.gameObject.SetActive(false);
        OverrideStart();
    }
}
