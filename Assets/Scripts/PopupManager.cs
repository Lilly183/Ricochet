using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager popupManagerInstance;

    public List<Popup> popups;

    // Start is called before the first frame update
    private void Start()
    {
        if (popupManagerInstance == null)
        {
            popupManagerInstance = this;
        }
    }
}
