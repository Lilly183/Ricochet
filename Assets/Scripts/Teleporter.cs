using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetTeleporter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetTeleporter.childCount > 0)
        {
            Vector3 destination = targetTeleporter.GetChild(0).position;
            collision.gameObject.transform.position = destination;
        }
    }
}
