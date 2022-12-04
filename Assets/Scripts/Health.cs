using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //====================
    // Member Variable(s):
    //====================

    public uint maxHealth = 100;

    [HideInInspector]
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = (int)maxHealth;
    }

    public float CalcHealthPercentage()
    {
        return (((float)currentHealth / (float)maxHealth));
    }

    public bool ChangeHealth(int healthAdjustment)
    {
        currentHealth = Mathf.Min((int)maxHealth, currentHealth + healthAdjustment);

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    public void Die()
    {
        Enemy enemyObj = this.GetComponent<Enemy>();

        //===================
        // Enemy Death Logic:
        //===================

        if (enemyObj)
        {
            //==================================================
            // TO DO: Drop a pickup for the player to collect...
            //==================================================

            if (Random.Range(0.0f, 1.0f) < enemyObj.pickupDropRate)
            {
                //Debug.Log("<color=blue>DROP!</color>");
                //Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            }

            // Spawn explosion:
            GameObject explosion = Instantiate(enemyObj.prefabOnDeath, transform.position, Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f)));
            explosion.transform.localScale *= Random.Range(1.1f, 1.3f);

            // Play explosionSFX:
            enemyObj.audioSource.transform.parent = null;
            enemyObj.audioSource.PlayOneShot(enemyObj.deathSFX);
            enemyObj.audioSource.GetComponent<LifeTime>().StartTimer();

            // Detach the TrailRenderer component and set it to autodestruct:
            enemyObj.tr.transform.parent = null;
            enemyObj.tr.autodestruct = true;

            // Decrease the number of current enemies:
            --EnemyManager.currentEnemies;
        }

        //======================
        // Standard Death Logic:
        //======================

        Destroy(gameObject);
    }

    void OnValidate()
    {
        maxHealth = (uint)Mathf.Max(maxHealth, 1);
    }
}