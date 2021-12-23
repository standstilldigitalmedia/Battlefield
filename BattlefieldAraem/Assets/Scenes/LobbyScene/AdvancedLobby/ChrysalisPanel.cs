using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrysalisPanel : BaseInventoryPanel
{
    public override void RaceChange()
    {
        
    }

    public override void SetListeners()
    {
        
    }

    public override void UnSetListeners()
    {
        
    }

    public override void BaseStart()
    {
        Debug.Log("Chrylasisi base start");
        LeftPanel.ChrysalisClicked += InitPanel;
    }

    public override void BaseInitPanel()
    {
        
    }

    public override void BaseClearPanel()
    {
        
    }
}
