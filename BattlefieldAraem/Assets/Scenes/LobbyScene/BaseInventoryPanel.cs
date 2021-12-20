using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInventoryPanel : BasePanel
{
    int race;
    public abstract void RaceChange();
    public abstract void BaseStart();
    public abstract void BaseInitPanel();
    public abstract void BaseClearPanel();

    void OnBaseRaceClick()
    {
        race = 0;
        if (gameObject.activeSelf)
        {
            RaceChange();
        }
    }

    void OnDaglareshClick()
    {
        race = 1;
        if (gameObject.activeSelf)
        {
            RaceChange();
        }
    }

    void OnShinRaClick()
    {
        race = 2;
        if (gameObject.activeSelf)
        {
            RaceChange();
        }
    }

    void OnHakarClick()
    {
        race = 3;
        if (gameObject.activeSelf)
        {
            RaceChange();
        }
    }

    void OnIritarClick()
    {
        race = 4;
        if (gameObject.activeSelf)
        {
            RaceChange();
        }
    }

    void OnJaraethAllianceClick()
    {
        race = 5;
        if (gameObject.activeSelf)
        {
            RaceChange();
        }
    }

    public override void OverrideInitPanel()
    {
        BaseInitPanel();
    }

    public override void OverideClearPanel()
    {
        BaseClearPanel();
    }

    public override void OverrideStart()
    {
        LeftPanel.ClearAllPanelsClick += ClearPanel;
        LeftPanel.BaseRaceClicked += OnBaseRaceClick;
        LeftPanel.DaglareshClicked += OnDaglareshClick;
        LeftPanel.ShinRaClicked += OnShinRaClick;
        LeftPanel.HakarClicked += OnHakarClick;
        LeftPanel.IritarClicked += OnIritarClick;
        LeftPanel.JarethAllianceClicked += OnJaraethAllianceClick;
        ClearPanel();
        BaseStart();
    }
}
