using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenFuryItem : PassiveItem
{
    public override void AddtoPlayer(PlayerStats playerStats)
    {
        playerStats.PostureRecovery += 0.005f;
    }
}
