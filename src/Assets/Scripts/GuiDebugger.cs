using UnityEngine;
using System.Collections;

public class GuiDebugger : MonoBehaviour
{
    private GuiManager _manager;

    public void Start()
    {
        _manager = GuiManager.Instance;
    }

    public void StartRoundClick()
    {
        _manager.InitRound();
    }

    public void EndRoundClick()
    {
        _manager.RoundOver();
    }
}
