using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollision : MonoBehaviour
{
    PlayerHealth playerHealth;
    PlayerHelper playerHelper;
    void Start()
    {
        playerHealth = PlayerReference.player.GetComponent<PlayerHealth>();
        playerHelper = PlayerReference.player.GetComponent<PlayerHelper>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        if (collision.collider.CompareTag("Player"))
        {
            if (playerHealth.getPlayerHealth() != 1) {
                playerHelper.TeleportLast();
            }
            playerHealth.LoseHealth(1);


        }
    }
}
