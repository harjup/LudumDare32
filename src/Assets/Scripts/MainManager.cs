﻿using Assets.Scripts;
using UnityEngine;

public class MainManager : Singleton<MainManager>
{
    private DollarStore _dollarStore;
    private GuiManager _guiManager;
    private CivilianManager _civilianManager;
    //private DuckManager _duckManager;

    void Start()
    {
        // Initialize
        _dollarStore = DollarStore.Instance;
        _guiManager = GuiManager.Instance;
        _civilianManager = CivilianManager.Instance;
        //_duckManager = DuckManager.Instance;

        InitRound();
    }

    public void InitRound()
    {
        ResetDollarCount();

        _guiManager.InitRound();

        // Clean up any civilians
        // Spawn npcs into scene
        _civilianManager.SpawnCivilians();


        // Lets' not care about this for now
        // Clean up any ducks
        // Spawn ducks into scene
    }

    public void EndRound()
    {
        _guiManager.RoundOver();
    }

    private void ResetDollarCount()
    {
        _dollarStore.Reset();   
    }

    public void MakeAllFlee()
    {
        foreach (var civilian in FindObjectsOfType<Civilian>())
        {
            if (civilian.CurrentState != Civilian.State.Mugged)
                civilian.GetComponent<Civilian>().CurrentState = Civilian.State.Fleeing;
        }
    }
}
