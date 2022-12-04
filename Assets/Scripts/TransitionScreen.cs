using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    //=======================================
    // Makes Reference(s) to Other Object(s):
    //=======================================

    public Text transitionScreenText;

    //====================
    // Member Variable(s):
    //====================

    public Color LoseTextColor = new(1.0f, 0.0f, 0.0f, 1.0f);
    public Color ClearTextColor = new(1.0f, 1.0f, 1.0f, 1.0f);
    public Color WinTextColor = new(0.02352941f, 1.0f, 0.003921569f, 1.0f);

    private string LoseTextColorHex;
    private string ClearTextColorHex;
    private string WinTextColorHex;

    private void Awake()
    {
        LoseTextColorHex = ColorUtility.ToHtmlStringRGBA(LoseTextColor);
        ClearTextColorHex = ColorUtility.ToHtmlStringRGBA(ClearTextColor);
        WinTextColorHex = ColorUtility.ToHtmlStringRGBA(WinTextColor);
    }

    //====================
    // Member Variable(s):
    //====================

    public float transitionDelay = 2.5f;
    public enum GameState : ushort { Lose = 0, Clear = 1, Win = 2 };

    public void Transition(GameState condition)
    {
        gameObject.SetActive(true);

        switch (condition)
        {
            case GameState.Lose:
                transitionScreenText.text = $"<color=#{LoseTextColorHex}>Game Over!</color>";
                Invoke(nameof(LoadCurrent), transitionDelay);
                break;
            case GameState.Clear:
                transitionScreenText.text = $"<color=#{ClearTextColorHex}>Stage Cleared!</color>";
                Invoke(nameof(LoadNext), transitionDelay);
                break;
            case GameState.Win:
                transitionScreenText.text = $"<color=#{WinTextColorHex}>You Win!</color>";
                Invoke(nameof(LoadMenu), transitionDelay);
                break;
            default:
                break;
        }
    }

    private void LoadCurrent()
    {
        ChangeLevel.SwitchLevel(restartCurrentLevel: true);
    }

    private void LoadNext()
    {
        ChangeLevel.AdvanceToNextLevel();
    }

    private void LoadMenu()
    {
        ChangeLevel.GoToMainMenu();
    }
}
