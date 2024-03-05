using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : Consumeable
{
    public override void AddtoPlayer(PlayerStats playerStats)
    {
        playerStats.playerHealth = playerStats.maxHealth;
    }
}
