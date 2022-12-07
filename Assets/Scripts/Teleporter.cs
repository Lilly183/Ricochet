using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //=================================
    // Reference(s) to Other Object(s):
    //=================================

    public Transform targetTeleporter;

    //==================
    // On Trigger Enter:
    //==================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetTeleporter.childCount > 0 && collision.CompareTag("Enemies"))
        {
            Enemy enemyObj = collision.GetComponent<Enemy>();
            Vector3 destination = targetTeleporter.GetChild(0).position;
            collision.gameObject.transform.position = destination;
            enemyObj.tr.Clear();
        }
    }
}
