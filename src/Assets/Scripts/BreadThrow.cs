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

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var mousePos = Input.mousePosition;
            var targetPos = Camera.main.ScreenToWorldPoint(mousePos).SetZ(0);
            
            var bread = Instantiate(_breadPiece, _throwPosition, transform.rotation) as GameObject;
            var breadRigidbody = bread.GetComponent<Rigidbody2D>();
            breadRigidbody.velocity = (targetPos - _throwPosition).normalized * 10f;
            Debug.Log(breadRigidbody.velocity);
        }
    }
}
