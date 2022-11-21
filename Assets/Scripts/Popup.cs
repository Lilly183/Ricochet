using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            gameObject.SetActive(false);
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
