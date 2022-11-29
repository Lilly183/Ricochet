using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    //===================
    // Singleton Pattern:
    //===================

    public static TransitionManager transitionManagerInstance;

    //=======================================
    // Makes Reference(s) to Other Object(s):
    //=======================================

    public TransitionScreen transitionScreen;

    private void Start()
    {
        if (transitionManagerInstance == null)
        {
            transitionManagerInstance = this;
        }
    }
}
