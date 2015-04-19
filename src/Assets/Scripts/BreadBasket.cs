using UnityEngine;
using System.Collections;

public class BreadBasket : MonoBehaviourBase
{
    private SpriteRenderer _spriteRenderer;


    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Show()
    {
        _spriteRenderer.enabled = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _spriteRenderer.enabled = false;
        gameObject.SetActive(false);
    }
}
