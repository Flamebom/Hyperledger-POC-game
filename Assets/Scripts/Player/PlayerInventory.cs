using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<WeaponItem> WeaponInventory;
    public List<ItemScriptableObject> PassiveInventory;

    private void Start()
    { 
        WeaponInventory = new List<WeaponItem>();
        PassiveInventory = new List<ItemScriptableObject>();
    }
}
