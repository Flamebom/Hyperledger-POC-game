using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemyScript : MonoBehaviour
{
    public int health = 10;
    private float timeBtwShots;
    public float atkSpeed = 2;

    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = atkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = atkSpeed;
        } else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public void DamageEnemy(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
