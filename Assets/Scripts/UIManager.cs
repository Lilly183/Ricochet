using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //===================
    // Singleton Pattern:
    //===================

    public static UIManager uiInstance = null;

    //=================================
    // Reference(s) to Other Object(s):
    //=================================

    public Text scoreText;
    public Text timeText;

    //====================
    // Member Variable(s):
    //====================

    private void Awake()
    {
        if (uiInstance == null)
        {
            uiInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
    }

    //=================================
    // Refresh All UI (Score and Time):
    //=================================

    public void RefreshUI()
    {
        RefreshTimeUI();
        RefreshScoreUI();
    }

    //==================
    // Refresh Score UI:
    //==================

    public void RefreshScoreUI()
    {
        if (Player.playerObj != null)
        {
            scoreText.text = Player.playerObj.Score.ToString();
        }
    }

    //=================
    // Refresh Time UI:
    //=================

    public void RefreshTimeUI()
    {
        timeText.text = Timer.timeRemaining.ToString();
    }
}
