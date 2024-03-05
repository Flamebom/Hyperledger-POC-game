using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemyProjectileMovement : MonoBehaviour
{
    public float speed;
    private float time;
    private Transform player;
    private Rigidbody2D rb;
    private PlayerStats playerStats;
    private PlayerPosture playerPosture;
    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        time = 7.0f;
        speed = 5;
        rb = GetComponent<Rigidbody2D>();
        player = PlayerReference.player.transform;
        playerHealth = PlayerReference.player.GetComponent<PlayerHealth>();
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        playerPosture = PlayerReference.player.GetComponent<PlayerPosture>();
        Vector2 direction = player.transform.position - transform.position;
        rb.velocity = (new Vector2(direction.x, direction.y).normalized) * speed;

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0.0f)
        {
            DestroyProjectile();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8) { return; }
        DestroyProjectile();
        if (other.CompareTag("Player"))
        {
            playerHealth.LoseHealth(1);
        }
        else if (other.CompareTag("Parry"))
        {
            if (playerStats.LongParrying)
            {
                playerPosture.UpdatePosture(50);
            }
        }


    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
