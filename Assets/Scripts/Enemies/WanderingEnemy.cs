using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEnemy : Enemy
{
    bool canTurn = true;


    private void Update()
    {
        if (!takingKnockback)
        {
            if (isFacingRight())
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
    }
    private bool isFacingRight()
    {
        return transform.localScale.x > 0;
    }/*
    private void OnTriggerEnter2D(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x) * 2, transform.localScale.y);
        }
    }*/
    private void OnTriggerExit2D(Collider2D collider)
    {
        // turn off to make enemy turn when collide with player
        if (!collider.CompareTag("Player"))
        {
            if (canTurn)
            {
                Invoke("turnCooldown", 0.5f);
                canTurn = false;
                transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x) * 2, transform.localScale.y);
            }
        }
    }

    void turnCooldown()
    {
        canTurn = true;
    }
}
