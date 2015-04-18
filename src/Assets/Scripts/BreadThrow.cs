using UnityEngine;
using System.Collections;

public class BreadThrow : MonoBehaviourBase
{
    private GameObject _breadPiece;
    private Vector3 _throwPosition;
    public void Start()
    {
        _breadPiece = Resources.Load<GameObject>("Prefabs/Actors/BreadPiece");
        _throwPosition = transform.position;
    }


    public void ThrowTheBread(Vector2 force)
    {
        var bread = Instantiate(_breadPiece, _throwPosition, transform.rotation) as GameObject;
        var breadRigidbody = bread.GetComponent<Rigidbody2D>();

        breadRigidbody.velocity = force;
        Debug.Log(breadRigidbody.velocity);
    }
}
