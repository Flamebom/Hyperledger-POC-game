using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosture : MonoBehaviour
{
    private PlayerStats playerStats;
    [SerializeField] private float staggerTime =2f;
    public float TimeTillPostureDecrease=0;
    void Start()
    {
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
    }
    public void Parry(int postureDamage) {
        if (playerStats.LongParrying) {
            UpdatePosture(postureDamage);
        }
    }
    public void UpdatePosture(int posture)
    {
        playerStats.Posture -= posture;
        TimeTillPostureDecrease = Time.time;
    }
    void Update()
    {
        if (playerStats.Posture < 0) {
            playerStats.Posture = 100;
            playerStats.isStaggered = true;
            StartCoroutine(stopStagger());
        }
        decreasePosture();
    }
    public void StopStaggerRecovery() {
        StopCoroutine(stopStagger());
    }
    private IEnumerator stopStagger() {
        yield return new WaitForSeconds(staggerTime);
        playerStats.isStaggered = false;

    }
    private void decreasePosture() {
        if (Time.time > 2f + TimeTillPostureDecrease) {
            playerStats.Posture += playerStats.PostureRecovery;
            playerStats.Posture = playerStats.Posture>playerStats.MaxPosture ? playerStats.MaxPosture : playerStats.Posture;
        }
    }
}
