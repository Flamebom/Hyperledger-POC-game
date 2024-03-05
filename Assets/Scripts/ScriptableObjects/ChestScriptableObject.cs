using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Chest")]
public class ChestScriptableObject : ScriptableObject
{
    public int rareity;
    public Sprite sprite;
}
