using UnityEngine;
using System.Collections;

public class LargeBread : MonoBehaviour
{

    private ThrowingArm arm;
    private Rigidbody2D _physics;
    private SpriteRenderer _spriteRenderer;

    public float Speed = 100f;

    public void Start()
    {
        arm = FindObjectOfType<ThrowingArm>();
        _physics = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(arm._previousMousePos).SetZ(0);

        Vector2 direction = (targetPos - transform.position).normalized;
        var distance = (targetPos - transform.position).magnitude;

        _physics.AddForce(direction * (distance * Speed));
    }


    public void Show()
    {
        _spriteRenderer.enabled = true;
    }

    public void Hide()
    {
        _spriteRenderer.enabled = false;
    }
}
