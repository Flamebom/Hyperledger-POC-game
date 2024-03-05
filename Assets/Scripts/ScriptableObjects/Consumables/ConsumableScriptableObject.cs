using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumeable", menuName = "Consumeable")]
public class ConsumableScriptableObject : ScriptableObject
{
    public new string name;
    public string SFXRersourceLocation;
    public Sprite sprite;

}
