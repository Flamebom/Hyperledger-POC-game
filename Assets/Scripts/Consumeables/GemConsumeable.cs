using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemConsumeable : Consumeable
{
    public override void AddtoPlayer(PlayerStats playerStats)
    {
        playerStats.gems++;
    }
}
