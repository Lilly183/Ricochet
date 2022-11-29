using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If enemy collides, deduct health...
        // Also killed the enemy entirely.

        Debug.Log("<color=orange>The base has been hit!</color>");
    }
}
