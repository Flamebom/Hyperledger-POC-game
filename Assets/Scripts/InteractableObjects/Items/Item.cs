using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    private PlayerInventory playerInventory;
    protected List<ItemScriptableObject> passiveInventory;
    protected List<WeaponItem> weaponInventory;

    protected PlayerInventoryUIController pUIController;
    protected PlayerStats playerStats;
    public SpriteRenderer sprite;
    public ItemScriptableObject item;


    void Start()
    {
        pUIController = GameObject.FindGameObjectWithTag("PlayerInventoryUI").GetComponent<PlayerInventoryUIController>();
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        playerInventory = PlayerReference.player.GetComponent<PlayerInventory>();
        weaponInventory = playerInventory.WeaponInventory;
        passiveInventory = playerInventory.PassiveInventory;
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = item.sprite;
        sprite.sortingLayerName = "ItemLayer";
    }


    public override void Interact()
    {
        AddtoPlayer(playerStats);
        Destroy(gameObject);
    }
    public virtual void Load() { 
    }
    public virtual void AddtoPlayer(PlayerStats playerStats) {
    }
}
