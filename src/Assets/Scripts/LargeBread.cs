using UnityEngine;
using System.Collections;

public class LargeBread : MonoBehaviour
{

    private ThrowingArm arm;
    private Rigidbody2D _physics;

    public float Speed = 100f;

    public void Start()
    {
        arm = FindObjectOfType<ThrowingArm>();
        _physics = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(arm._previousMousePos).SetZ(0);

        Vector2 direction = (targetPos - transform.position).normalized;
        var distance = (targetPos - transform.position).magnitude;

        _physics.AddForce(direction * (distance * Speed));
    }
}
