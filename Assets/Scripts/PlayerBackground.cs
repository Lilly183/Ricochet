using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerBackground : MonoBehaviour
{
    SpriteRenderer sr = null;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sr)
        {
            Player.playerBackgroundSpriteRenderers.Add(sr);
        }
    }
}
