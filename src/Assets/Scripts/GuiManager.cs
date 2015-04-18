using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuiManager : Singleton<GuiManager>
{
    private IGuiSet FinalStatus;
    private IGuiSet MainStatus;

    public void Awake()
    {
        FinalStatus = FindObjectOfType<FinalGuiSet>();
        if (FinalStatus == null)
        {
            Debug.LogError("FinalStatus not found");
        }

        MainStatus = FindObjectOfType<MainGuiSet>();
        if (MainStatus == null)
        {
            Debug.LogError("MainStatus not found");
        }        
    }

    public void InitRound()
    {
        FinalStatus.Disable();
        MainStatus.Enable();
    }

    public void RoundOver()
    {
        FinalStatus.Enable();
        MainStatus.Disable();
    }


}