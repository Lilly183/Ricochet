using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Base : MonoBehaviour
{
    Health hp = null;

    Text healthText;

    private void Start()
    {
        hp = GetComponent<Health>();
        healthText = GetComponentInChildren<Text>();

        RefreshUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            // Get the enemy's component.
            Enemy en = collision.gameObject.GetComponent<Enemy>();

            if (en != null)
            {
                // Change the health of the base by the enemy's damage.
                bool isDestroyed = hp.ChangeHealth(-en.damageDealtToBase);

                // Refresh the UI.
                RefreshUI();

                // Check if base was destroyed.
                if (isDestroyed)
                {
                    // If yes, the player has lost.
                    TransitionManager.transitionManagerInstance.transitionScreen.Transition(TransitionScreen.GameState.Lose);
                }

            }
        }
    }

    void RefreshUI()
    {
        healthText.text = hp.CalcHealthPercentage() * 100 + "%";
    }
}
