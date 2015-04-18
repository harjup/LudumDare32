using System;
using System.Diagnostics;
using DG.Tweening;
using Debug = UnityEngine.Debug;

public interface IGuiSet
{
    void Disable();
    void Enable();
}

public class MainGuiSet : MonoBehaviourBase, IGuiSet
{
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}