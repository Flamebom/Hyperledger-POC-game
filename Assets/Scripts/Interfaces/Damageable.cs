using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageble 
{
    void TakeDamage(int damage);
    void TakeKnockback(Vector2 direction, float speed) { 
    }
}
