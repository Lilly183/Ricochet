using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public static void SwitchLevel(bool restartCurrentLevel = false, string targetLevel = "")
    {
        if (restartCurrentLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
            SceneManager.LoadScene(targetLevel);
        }
    }

    readonly static int endSceneIndex = SceneManager.sceneCountInBuildSettings - 1;

    public static void AdvanceToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        string nextLevel = (nextSceneIndex <= endSceneIndex) ? System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(nextSceneIndex)) : "";

        if (!string.IsNullOrEmpty(nextLevel))
        {
            // If nextLevel assignment is successful, PlayerPrefs for current progress
            // need to be set so that the player can continue from this point when
            // clicking "Continue" from the main menu.

            //PlayerPrefs.SetString("CurrentProgress", nextLevel);
            //PlayerPrefs.Save();
            SwitchLevel(targetLevel: nextLevel);
        }

        else
        {
            GoToMainMenu();
        }
    }

    public static void GoToMainMenu()
    {
        SwitchLevel(targetLevel: "MainMenu");
    }

    //public static void SaveSettings()
    //{
    //    Player playerObject = FindObjectOfType<Player>();

    //    if (playerObject != null)
    //    {
    //        PlayerPrefs.SetInt("Score", playerObject.Score);
    //        PlayerPrefs.Save();
    //    }
    //}
}
