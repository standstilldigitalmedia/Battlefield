using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpring : BaseInventoryPanel
{
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
        LeftPanel.MainspringClicked += InitPanel;
    }

    public override void BaseInitPanel()
    {
        
    }

    public override void BaseClearPanel()
    {
        
    }
}
