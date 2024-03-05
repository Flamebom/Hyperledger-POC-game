using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : PassiveItem
{
    public HeartContainerItem heartItem;
    public override void AddtoPlayer(PlayerStats playerStats)
    {
        PlayerHealth playerHealth = PlayerReference.player.GetComponent<PlayerHealth>();
        playerStats.maxHealth += heartItem.HeartIncrease*2;
        playerHealth.Heal(heartItem.HeartIncrease * 2);
    }
}
