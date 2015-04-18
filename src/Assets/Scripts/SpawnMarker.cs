using UnityEngine;
using System.Collections;

public class SpawnMarker : MonoBehaviour
{
    public BoundsDirection Bounds;

    public void Start()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.enabled = false;
        }
         
    }
}
