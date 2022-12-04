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

        if (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);

            child.position = transform.position + (seekDirection * orbitalRadius);

            // Controls the rotation of the child object:

            child.right = seekDirection;
            child.Rotate(new Vector3(0, 0, spriteAngleOffset));
        }

        // Logic used to move the player (Remember to normalize vectors!)

        moveDirection = new Vector3(inputX, inputY).normalized;
        transform.Translate(speedPerSecond * Time.deltaTime * moveDirection);
    }

    // If the player collides with an object tagged with pickup...
    // Increase the player's score.
    // Maybe have a pickup object function?
}