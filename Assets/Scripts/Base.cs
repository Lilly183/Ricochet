using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Base : MonoBehaviour
{
    //====================
    // Member Variable(s):
    //====================

    Health hp = null;

    //===========
    // Health UI:
    //===========

    Text healthText;

    // Start is called before the first frame update
    private void Start()
    {
        hp = GetComponent<Health>();
        healthText = GetComponentInChildren<Text>();

        RefreshBaseUI();
    }

    //==================
    // On Trigger Enter:
    //==================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemies":
            {
                // Get the "Enemy" component.
                Enemy en = collision.gameObject.GetComponent<Enemy>();

                if (en != null)
                {
                    // Change the health of the base by the enemy's damage.
                    bool isDestroyed = hp.ChangeHealth(-en.damageDealtToBase);

                    // Refresh the UI.
                    RefreshBaseUI();

                    // Check if base was destroyed.
                    if (isDestroyed)
                    {
                        // If yes, the player has lost.
                        TransitionManager.transitionManagerInstance.transitionScreen.Transition(TransitionScreen.GameState.Lose);
                    }
                }

                break;
            }
            default:
            {
                break;
            }
        }
    }

    //=================
    // Refresh Base UI:
    //=================

    void RefreshBaseUI()
    {
        healthText.text = hp.CalcHealthPercentage() * 100 + "%";
    }
}
