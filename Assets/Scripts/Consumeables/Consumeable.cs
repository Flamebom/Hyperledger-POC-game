using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumeable : MonoBehaviour
{
    private PlayerStats playerStats;
    public ConsumableScriptableObject consumableScriptableObject;
    private AudioClip audioClip;
    public SpriteRenderer sprite;
    void Start()
    {
        audioClip = Resources.Load<AudioClip>(consumableScriptableObject.SFXRersourceLocation);
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = consumableScriptableObject.sprite;
    }
        public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GetComponent<AudioSource>().PlayOneShot(audioClip);
            AddtoPlayer(playerStats);
            sprite.enabled = false;
            StartCoroutine(destroy(gameObject)); 
        }
    }
    private IEnumerator destroy(GameObject gameObject) {
        yield return new WaitForSeconds(audioClip.length);
        Destroy(gameObject);

    }
    public virtual void AddtoPlayer(PlayerStats playerStats)
    {
    }
}
