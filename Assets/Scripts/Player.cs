using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    //Rigidbody2D rb = null;

    /*
    ==================
    Singleton Pattern:
    ==================

    playerObj is declared static to make it accessible to other scripts:

        - UIManager: 
            Access to the player's score is needed for the RefreshScoreUI() method.
        - Enemy: 
            Access to the player's damage is needed to change the health of an enemy that has collided 
            with the player's paddle.
        - ChangeLevel
            Access to the player's score is needed to set the PlayerPrefs in the SaveSettings() method.
    */
    
    public static Player playerObj = null;

    //====================
    // Member Variable(s):
    //====================

    public float spriteAngleOffset = 0.0f;

    private float inputX;
    private float inputY;
    
    public float speedPerSecond = 5.0f;
    public float orbitalRadius = 1.0f;

    private Vector3 mouseWorldPosition = Vector3.zero;
    private Vector3 seekDirection = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;

    public int damage = 10;
    public int boostDamage = 999;
    public float boostDamageDuration = 8.0f;
    public Color boostDamageColorEffect = new(0.1960784f, 1.0f, 0.01568628f, 0.5f);

    private bool hasEnergy = false;

    [HideInInspector]
    static public List<SpriteRenderer> playerBackgroundSpriteRenderers = new();
    readonly List<Color> normalBackgroundColors = new();

    private int score;
    public int Score
    {
        get { return score; }

        set
        {
            score = value;

            UIManager.uiInstance.RefreshScoreUI();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();

        if (playerObj == null)
        {
            playerObj = this;
        }

        Score = PlayerPrefs.GetInt("Score", 0);

        GetNormalBackgroundColors();
    }

    private void GetNormalBackgroundColors()
    {
        foreach (var sr in playerBackgroundSpriteRenderers)
        {
            normalBackgroundColors.Add(sr.color);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Cancel") == 1)
        {
            ChangeLevel.GoToMainMenu();
        }

        // Note the use of GetAxisRaw here (not just GetAxis).

        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        // Logic used to pivot the child object around the parent object at a fixed distance.

        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        seekDirection = mouseWorldPosition - transform.position;
        seekDirection.Normalize();

        if (transform.childCount > 1)
        {
            Transform child = transform.GetChild(1);

            child.position = transform.position + (seekDirection * orbitalRadius);

            // Controls the rotation of the child object:

            child.right = seekDirection;
            child.Rotate(new Vector3(0, 0, spriteAngleOffset));
        }

        // Logic used to move the player (Remember to normalize vectors!)

        moveDirection = new Vector3(inputX, inputY).normalized;
        transform.Translate(speedPerSecond * Time.deltaTime * moveDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Coins" or "Energy":
            {
                Pickup pickupObj = collision.gameObject.GetComponent<Pickup>();

                if (pickupObj)
                {
                    if (collision.gameObject.CompareTag("Coins") || hasEnergy == false)
                    {
                        pickupObj.Equip();

                        if (collision.gameObject.CompareTag("Energy"))
                        {
                            StartCoroutine(BoostDamage());
                        }

                        else
                        {
                            Score += 100;
                        }

                        Destroy(collision.gameObject);
                    }
                }

                break;
            }
            default:
                break;
        }
    }

    IEnumerator BoostDamage()
    {
        hasEnergy = true;

        // Store the player's original damage so that we can revert it back later:
        int normalDamage = damage;

        // playerBackgroundSpriteRenderers is a List for storing the sprite renderer
        // components for all of the gameObjects comprising the player's background.
        //Queue<Color> normalColors = new();

        foreach (var sr in playerBackgroundSpriteRenderers)
        {
            //normalColors.Enqueue(sr.color);
            sr.color = boostDamageColorEffect;
        }

        // Set the player's damage to boostDamage:
        damage = boostDamage;

        // Let the player's damage remain at boostDamage for the length of boostDamageDuration:
        yield return new WaitForSeconds(boostDamageDuration);

        // Return the player's damage back to normal levels (whatever it was before):
        damage = normalDamage;

        //foreach (var sr in playerBackgroundSpriteRenderers)
        //{
        //    sr.color = normalColors.Dequeue();
        //}

        for (int i = 0; i < playerBackgroundSpriteRenderers.Count; i++)
        {
            playerBackgroundSpriteRenderers[i].color = normalBackgroundColors[i];
        }

        hasEnergy = false;
    }
}



