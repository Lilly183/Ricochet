using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //===================
    // Singleton Pattern:
    //===================

    public static UIManager uiInstance;

    //=======================================
    // Makes Reference(s) to Other Object(s):
    //=======================================

    //Text scoreText;
    public Text timeText;

    //====================
    // Member Variable(s):
    //====================

    public Color timeTextColor = new(1.0f, 1.0f, 0.0f, 1.0f);
    private string timeTextColorHex;

    //public Color timeTextColorA = new(0, 1, 0);
    //public Color timeTextColorB = new(1, 0, 0);

    // The value of timeLimit is used (via staticTimeLimit) as a denominator in the UIManager
    // script for timeText to lerp between timeTextColorA and timeTextColorB. It cannot be zero.
    // Its value must be clamped to at least 1.

    // Start is called before the first frame update
    void Start()
    {
        if (uiInstance == null)
        {
            uiInstance = this;
        }

        timeTextColorHex = ColorUtility.ToHtmlStringRGBA(timeTextColor);

        RefreshUI();
    }

    public void RefreshUI()
    {
        //string timeTextColorHex = ColorUtility.ToHtmlStringRGB(Color.Lerp(timeTextColorA, timeTextColorB, (1 - (Timer.timeRemaining / (float)Timer.staticTimeLimit))));

        timeText.text = $"Time: <color=#{timeTextColorHex}>" + Timer.timeRemaining + "</color>";
        //scoreText.text = "Score: " + score;
    }
}
