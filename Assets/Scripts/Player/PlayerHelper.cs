using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : MonoBehaviour
{
    private bool furyOn = false;
    private float originalPostureRecovery = 0;
    private PlayerStats playerStats;
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        PersistentData.loadToPlayer();
    }
    public void ActivateFuryItems() {
        if (playerStats.PassiveInventory.ContainsKey("Frozen Fury")) {
            originalPostureRecovery = playerStats.PostureRecovery;
            playerStats.PostureRecovery = originalPostureRecovery * 1.5f;
        }
        furyOn= true;
    }
    public void DeactivateFuryItems()
    {
        if (furyOn)
        {
            if (playerStats.PassiveInventory.ContainsKey("Frozen Fury"))
            { playerStats.PostureRecovery = originalPostureRecovery; }
            furyOn = false;
        }

    }
    public void TeleportLast() {

            transform.position = playerStats.lastPosition;
             //gameObject.SetActive(false);
             //Invoke("activatePlayer", 2.0f);
        

    }
    private void activatePlayer() {
        gameObject.SetActive(true);

    }

    
}
