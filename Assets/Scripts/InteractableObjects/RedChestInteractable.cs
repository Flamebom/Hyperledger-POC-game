using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedChestInteractable : ChestInteractable
{
    public override void openChest()
    {
        Instantiate(itemList.create(4), transform.position, Quaternion.identity);
    }
}
