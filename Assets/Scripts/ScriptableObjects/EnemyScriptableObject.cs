using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Enemy", menuName = "Enemy/GenericEnemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public int health;
    public bool hasContactDamage;
    public int baseDamage;
    public float speed;
    public int postureDamage;
    public int gemDrop;
    public int healthDrop;
    public bool takeKnockback;
    public string SFXResourceLocation;
}
