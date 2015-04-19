using Assets.Scripts;
using UnityEngine;

public class BreadPiece : MonoBehaviourBase
{
    public bool HitFloor;

    public AudioClip thrownClip;
    public AudioClip hitFloorClip;

    public AudioSource AudioSource;

    public void Start()
    {
        thrownClip = Resources.Load<AudioClip>("Sounds/Throwing/BreadThrow");
        hitFloorClip = Resources.Load<AudioClip>("Sounds/Throwing/BreadLand");
        AudioSource = GetComponent<AudioSource>();

        AudioSource.PlayOneShot(thrownClip, 1f);
    }

    // When it hits the floor ducks will path to it
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            if (!HitFloor)
            {
                DuckManager.Instance.CommandDuck(gameObject);
                AudioSource.PlayOneShot(hitFloorClip, .5f);
            }
            
            // play hit floor sfx
            HitFloor = true;
        }
    }



}
