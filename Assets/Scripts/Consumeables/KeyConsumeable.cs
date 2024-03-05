    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyConsumeable : Consumeable
{
    public override void AddtoPlayer(PlayerStats playerStats)
    {
        playerStats.keys++;
    }
}
