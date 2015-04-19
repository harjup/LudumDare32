using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BreadThrow : MonoBehaviourBase
{
    private GameObject _breadPiece;
    private Vector3 _throwPosition;
    private Animator _animator;
    public void Start()
    {
        _breadPiece = Resources.Load<GameObject>("Prefabs/Actors/BreadPiece");
        _throwPosition = transform.position;
        _animator = GetComponent<Animator>();
    }


    public void ThrowTheBread(Vector2 force)
    {
        var bread = Instantiate(_breadPiece, _throwPosition, transform.rotation) as GameObject;
        var breadRigidbody = bread.GetComponent<Rigidbody2D>();

        breadRigidbody.velocity = force;
        Debug.Log(breadRigidbody.velocity);

        _animator.SetTrigger("Throw");
    }

    public void GetTheMoney()
    {
        _animator.SetTrigger("Success");
    }
}
