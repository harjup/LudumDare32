using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class BreadPiece : MonoBehaviourBase
{
    public bool _hitFloor;


    // When it hits the floor ducks will path to it
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            _hitFloor = true;
            DuckManager.Instance.CommandDuck(gameObject);
        }
    }



}
