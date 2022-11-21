using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void ContinueGame()
    {
        //if (PlayerPrefs.HasKey("CurrentProgress"))
        //{
        //    ChangeLevel.SwitchLevel(targetLevel: PlayerPrefs.GetString("CurrentProgress"));
        //}

        //else
        //{
        //    ChangeLevel.AdvanceToNextLevel();
        //}
    }

    public void NewGame()
    {
        //PlayerPrefs.DeleteAll();
        ChangeLevel.AdvanceToNextLevel();
    }

    public void Instructions()
    {
        Popup instructionsPopup = PopupManager.popupManagerInstance.popups.Find(obj => obj.name == "Instructions Popup");

        if (instructionsPopup)
        {
            instructionsPopup.Enable();
        }

        else
        {
            Debug.Log("<color=red>No Match...</color>");
        }
    }

    public void Credits()
    {
        Popup creditsPopup = PopupManager.popupManagerInstance.popups.Find(obj => obj.name == "Credits Popup");

        if (creditsPopup)
        {
            creditsPopup.Enable();
        }

        else
        {
            Debug.Log("<color=red>No Match...</color>");
        }
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}