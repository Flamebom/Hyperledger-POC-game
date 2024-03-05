using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    bool isShooting;
    [SerializeField] GameObject projectile;
    ShootingEnemyScriptableObject shootingEnemy;
    new void Start()
    {
        isShooting = false;
        base.Start();
        shootingEnemy = (ShootingEnemyScriptableObject)enemy;
    }
    private void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        if (rb.velocity.x != Vector2.zero.x) {
            Invoke("notTakingKnockback", 0.2f);
        }
        if (!isShooting)
        { StartCoroutine(Shoot()); }

    }
    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(projectile, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(shootingEnemy.timeBetweenShots);
        isShooting = false;
    }
}

