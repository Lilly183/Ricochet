using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    //=======================
    // Required Component(s):
    //=======================

    Rigidbody2D rb = null;
    Health hp = null;

    //===========
    // Prefab(s):
    //===========

    public GameObject prefabOnDeath;

    //============
    // References:
    //============

    private static Player playerObj = null;

    //====================
    // Member Variable(s):
    //====================

    Slider healthBar;

    public GameObject pickupPrefab;

    [Range(0.0f, 1.0f)]
    public float pickupDropRate;

    public float launchDelay = 1.0f;
    public float baseSpeed = 5;
    public float hitBonusSpeed = 1;
    public float maxBonusSpeed = 5;
    public int collisionDamage = 5;

    private int hitCount = 0;

    private Vector3 lastVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = GetComponent<Health>();
        healthBar = GetComponentInChildren<Slider>();

        if (playerObj == null)
        {
            playerObj = FindObjectOfType<Player>();
        }

        Invoke(nameof(Launch), launchDelay);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void Bounce(Vector2 direction)
    {
        float ballSpeed = baseSpeed + (hitCount * hitBonusSpeed);
        direction = direction.normalized;
        rb.velocity = direction * ballSpeed;
    }

    private void Launch()
    {
        Bounce(Random.insideUnitCircle.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isKilled;

        switch(collision.gameObject.tag)
        {
            case "Player":
            {
                isKilled = hp.ChangeHealth(-playerObj.damage);
                break;
            }
            case "Base":
            {
                isKilled = hp.ChangeHealth(-hp.currentHealth);
                // Damage the base!
                break;
            }
            // TO DO: Add Pickups...
            default:
            {
                isKilled = hp.ChangeHealth(-collisionDamage);
                break;
            }
        }

        healthBar.value = hp.CalcHealthPercentage();

        if (isKilled)
        {
            // Increase the player's score (Make static reference to player).

            --EnemyManager.currentEnemies;

            // Maybe drop a pickup for the player to collect:

            if (Random.Range(0.0f, 1.0f) < pickupDropRate)
            {

                Debug.Log("<color=blue>DROP!</color>");
                //Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            }

            GameObject explosion = Instantiate(prefabOnDeath, transform.position, Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f)));
            explosion.transform.localScale *= Random.Range(1.0f, 1.35f);
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        // CHANGE: Increase hitCount only when collision is with another enemy or player bumper
        ///////////////////////////////////////////////////////////////////////////////////////

        if ((hitCount * hitBonusSpeed) < maxBonusSpeed)
        {
            ++hitCount;
        }

        Bounce(Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal));
    }
}
