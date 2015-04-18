using UnityEngine;

public class FinalGuiSet : MonoBehaviourBase, IGuiSet
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