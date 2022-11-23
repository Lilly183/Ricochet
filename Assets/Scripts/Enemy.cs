using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    //=======================
    // Required Component(s):
    //=======================

    Rigidbody2D rb = null;

    //====================
    // Member Variable(s):
    //====================

    public float baseSpeed = 5;
    public float hitBonusSpeed = 1;
    public float maxBonusSpeed = 5;

    private int hitCount = 0;

    private Vector3 lastVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(Launch), 1.0f);
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
        ///////////////////////////////////////////////////////////////////////////////////////
        // CHANGE: Increase hitCount only when collision is with another enemy or player bumper
        ///////////////////////////////////////////////////////////////////////////////////////

        if ((hitCount * hitBonusSpeed) < maxBonusSpeed)
        {
            ++hitCount;
            //Debug.Log("<color=yellow>Increasing HitCount!</color>");
        }

        Bounce(Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal));
    }
}
