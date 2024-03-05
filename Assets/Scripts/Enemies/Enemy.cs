using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageble
{

    public SpriteRenderer sprite;
    private bool takeKnockback;
    public bool takingKnockback = false;
    public int health;
    public float speed;
    private PlayerHealth playerHealth;
    private PlayerStats playerStats;
    private PlayerPosture playerPosture;
    public Rigidbody2D rb;
    private AudioClip audioClip;
    [SerializeField] protected EnemyScriptableObject enemy;

    protected void Start()
    {
        audioClip = Resources.Load<AudioClip>(enemy.SFXResourceLocation);
        sprite = GetComponent<SpriteRenderer>();
        initiate();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = PlayerReference.player.GetComponent<PlayerHealth>();
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        playerPosture = PlayerReference.player.GetComponent<PlayerPosture>();
    }
    private void initiate()
    {
        speed = enemy.speed;
        sprite.sprite = enemy.sprite;
        health = enemy.health;
        takeKnockback = enemy.takeKnockback;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy.hasContactDamage)
        {
            if (collision.collider.CompareTag("Player"))
            {
                playerHealth.LoseHealth(enemy.baseDamage);
            }
            else if (collision.collider.CompareTag("Parry"))
            {
                playerPosture.Parry(enemy.baseDamage * enemy.postureDamage);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip);
        health -= damage;
        if (health <= 0)
        {

            lastBreath();

            Destroy(gameObject);
        }
    }
    public void TakeKnockback(Vector2 Direction, float knockbackforce)
    {
        if (takeKnockback)
        {
            takingKnockback = true;
            rb.velocity = Direction * knockbackforce;
            Invoke("notTakingKnockback", 0.2f);
        }
    }
    protected void notTakingKnockback()
    {
        takingKnockback = false;
        rb.velocity = Vector2.zero;
    }
    public virtual void lastBreath()
    {
        dropConsumables();
    }
    public virtual void dropConsumables()
    {
        int rand = Random.Range(1, 4);
        if (rand == 1)
        {
            Instantiate(GameObject.FindGameObjectWithTag("Helper").GetComponent<ConsumableHelper>().createHealth(enemy.healthDrop), transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(GameObject.FindGameObjectWithTag("Helper").GetComponent<ConsumableHelper>().createGem(enemy.gemDrop), transform.position, Quaternion.identity);
        }
    }
}
