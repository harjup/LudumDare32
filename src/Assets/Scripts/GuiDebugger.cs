using UnityEngine;
using System.Collections;

public class GuiDebugger : MonoBehaviour
{
    private MainManager _manager;
    private DollarStore _dollarStore;

    public void Start()
    {
        _manager = MainManager.Instance;
        _dollarStore = DollarStore.Instance;
    }

    public void StartRoundClick()
    {
        _manager.InitRound();
    }

    public void EndRoundClick()
    {
        _manager.EndRound();
    }

    public void AddDollarClick()
    {
        _dollarStore.AddDollars(5);
    }
}
