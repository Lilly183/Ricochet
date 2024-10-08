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

    private void Awake()
    {    
        currentHealth = (int)maxHealth;
    }

    //=============================
    // Calculate Health Percentage:
    //=============================

    public float CalcHealthPercentage()
    {
        return (((float)currentHealth / (float)maxHealth));
    }

    //===============
    // Change Health:
    //===============

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

    //=====
    // Die:
    //=====

    public void Die()
    {
        Enemy enemyObj = this.GetComponent<Enemy>();

        //===================
        // Enemy Death Logic:
        //===================

        if (enemyObj)
        {
            // Drop a pickup for the player to collect:
            if (Random.Range(0.0f, 1.0f) < enemyObj.pickupDropRate)
            {
                Instantiate(enemyObj.pickupPrefabs[Random.Range(0, enemyObj.pickupPrefabs.Count)], transform.position, Quaternion.identity);
            }

            // Spawn explosion:
            GameObject explosion = Instantiate(enemyObj.prefabOnDeath, transform.position, Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f)));
            explosion.transform.localScale *= Random.Range(1.1f, 1.3f);

            // Play explosionSFX:
            enemyObj.audioSource.transform.parent = null;
            enemyObj.audioSource.PlayOneShot(enemyObj.deathSFX);
            enemyObj.audioSource.GetComponent<LifeTime>().StartTimer();

            // Detach the TrailRenderer component. Set it to autodestruct:
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

    //====================
    // Validate maxHealth:
    //====================

    void OnValidate()
    {
        maxHealth = (uint)Mathf.Max(maxHealth, 1);
    }
}