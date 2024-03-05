using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableWall : MonoBehaviour, Damageble
{
    [SerializeField]private int health;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
  

}
