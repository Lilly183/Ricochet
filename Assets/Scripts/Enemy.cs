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
    
    [HideInInspector]
    public AudioSource audioSource;

    public AudioClip collisionSFX1;
    public AudioClip collisionSFX2;
    public AudioClip deathSFX;

    [HideInInspector]
    public TrailRenderer tr;

    //===========
    // Prefab(s):
    //===========

    public GameObject prefabOnDeath;
    
    //====================
    // Member Variable(s):
    //====================

    public float launchDelay = 1.0f;
    public float baseSpeed = 5;
    public float hitBonusSpeed = 1;
    public float maxBonusSpeed = 5;
    
    public int damageDealtToBase = 5;
    public int damageTakenPerBounce = 5;

    //=========
    // Pickups:
    //=========

    [Range(0.0f, 1.0f)]
    public float pickupDropRate;
    public List<GameObject> pickupPrefabs = new();
   
    private int hitCount = 0;
    private Vector3 lastVelocity = Vector3.zero;
    private Slider healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = GetComponent<Health>();

        audioSource = GetComponentInChildren<AudioSource>();
        healthBar = GetComponentInChildren<Slider>();
        tr = GetComponentInChildren<TrailRenderer>();

        Invoke(nameof(Launch), launchDelay);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }
    
    private void Launch()
    {
        Bounce(Random.insideUnitCircle.normalized);
    }

    private void Bounce(Vector2 direction)
    {
        float ballSpeed = baseSpeed + (hitCount * hitBonusSpeed);
        direction = direction.normalized;
        rb.velocity = direction * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            //case "Pickups" or "Base":
            //{
            //    return;
            //}
            case "Player":
            {
                /*
                =========================
                Enemy Collides w/ Player: 
                =========================

                If the enemy collides with the player and is killed as a result of the player's damage,
                ChangeHealth() will return true; it will also handle everything that should happen whenever
                an enemy dies (check the Health class's Die() method for more information). If the enemy has
                been killed, nothing further needs to be done. We can return. Otherwise, a sound effect will 
                play, and break will allow execution to proceed. The slider acting as the enemy's healthbar 
                will be updated, hitCount will be updated if applicable, and the enemy's velocity will be 
                changed via the Bounce() method.
                */

                if (hp.ChangeHealth(-Player.playerObj.damage))
                {
                    return;
                }

                audioSource.PlayOneShot(collisionSFX2, 0.75f);

                break;
            }
            default:
            {
                /*
                ========================
                Enemy Collides w/ Other:
                ========================

                If an enemy has collided with an object that isn't tagged as a pickup, a base, or the 
                player (which typically means it's the environment), instead of losing an amount of 
                health that's dictated by the other object's damage (e.g., the player), change the 
                enemy's health by the value of damageTakenPerBounce. Everything else is the same as 
                above.
                */

                if (hp.ChangeHealth(-damageTakenPerBounce))
                {
                    return;
                }

                audioSource.PlayOneShot(collisionSFX1, 0.5f);

                break;
            }
        }

        healthBar.value = hp.CalcHealthPercentage();

        if ((hitCount * hitBonusSpeed) < maxBonusSpeed)
        {
            ++hitCount;
        }

        Bounce(Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Base":
            {
                // Kill the enemy.
                hp.ChangeHealth(-hp.currentHealth);
                break;
            }
            default:
            {
                break;
            }
        }
    }
}