using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New NPC", menuName = "NPC/GenericNPC")]
public class NPCScriptableObject : ScriptableObject
{
    public new string name;
    public Sprite sprite;
}
