using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : Item
{
 
    public override void Interact()
    {
        passiveInventory.Add(Instantiate(item));
        playerStats.PassiveInventory.Add(item.name, item.rareity);
        pUIController.addToInventory(passiveInventory.Count - 1);
        base.Interact();
       
        
    }
    public override void Load()
    {   
passiveInventory= PlayerReference.player.GetComponent<PlayerInventory>().PassiveInventory;
    pUIController = GameObject.FindGameObjectWithTag("PlayerInventoryUI").GetComponent<PlayerInventoryUIController>();
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        passiveInventory.Add(Instantiate(item));
        playerStats.PassiveInventory.Add(item.name, item.rareity);
        pUIController.addToInventory(passiveInventory.Count - 1);
    }
}
