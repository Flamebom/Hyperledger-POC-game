using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    private PlayerStats playerStats;
    private Animator chestAnimator;
    public bool isLocked = true;
    private bool isOpen = false;
    public SpriteRenderer sprite;
    public ChestScriptableObject chest;
    public ItemList itemList;

    public void Start()
    {
        itemList = GameObject.FindGameObjectWithTag("ItemList").GetComponent<ItemList>();
        chestAnimator = GetComponent<Animator>();
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = chest.sprite;
        chestAnimator.SetBool("isLocked", isLocked);
        chestAnimator.SetBool("isOpen", isOpen);
    }
    public override void Interact()
    {
        if (playerStats.keys > 0 && isLocked)
        {
            playerStats.keys--;
            isLocked = false;
            chestAnimator.SetBool("isLocked", isLocked);

        }
        else if (!isLocked && !isOpen) {
            isOpen = true;
            openChest();
            chestAnimator.SetBool("isOpen", isOpen);
        }
    }   
    public virtual void openChest() { 

    }
}
