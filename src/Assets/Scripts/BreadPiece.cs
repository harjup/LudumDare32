using Assets.Scripts;
using UnityEngine;

public class BreadPiece : MonoBehaviourBase
{
    public bool HitFloor;

    // When it hits the floor ducks will path to it
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            if (!HitFloor)
            {
                DuckManager.Instance.CommandDuck(gameObject);
            }

            HitFloor = true;
        }
    }



}
