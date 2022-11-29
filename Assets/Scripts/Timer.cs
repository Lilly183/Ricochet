using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Declared public to make accessible to the editor.
    public int timeLimit = 60;

    // Declared static to make accessible to other scripts (i.e., UIManager)
    //static public int staticTimeLimit;
    static public int timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        //staticTimeLimit = timeLimit;
        StartCoroutine(Countdown());
    }

    private void Awake()
    {
        timeRemaining = timeLimit;
    }

    void OnValidate()
    {
        timeLimit = Mathf.Max(timeLimit, 1);
    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            --timeRemaining;
            
            // Use ChangeLevel's ActiveSceneIsLastLevel method to determine whether the TransitionScreen should be set to a
            // clear state or a win state.

            if (timeRemaining <= 0)
            {
                if (ChangeLevel.ActiveSceneIsLastLevel())
                {    
                    TransitionManager.transitionManagerInstance.transitionScreen.Transition(TransitionScreen.GameState.Win);
                }

                else
                {
                    TransitionManager.transitionManagerInstance.transitionScreen.Transition(TransitionScreen.GameState.Clear);
                }
            }

            // Update the Timer in the UI:

            UIManager.uiInstance.RefreshUI();
        }
    }
}