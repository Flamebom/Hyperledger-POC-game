using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryChecker : MonoBehaviour
{
    public PlayerStats playerStats;
    BoxCollider2D parryHitbox;
    void Start()
    {
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        parryHitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        parryHitbox.enabled = playerStats.Parrying || playerStats.LongParrying ? true : false;
    }
}
