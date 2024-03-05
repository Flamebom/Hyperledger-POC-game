using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public new string name;
    public int rareity;
    public Sprite sprite;
    public string description;
    public string lore;
 
}
