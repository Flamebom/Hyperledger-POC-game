using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTierChest : ChestInteractable
{
    public override void openChest()
    {
        Instantiate(itemList.create(5), transform.position, Quaternion.identity);
    }
}
