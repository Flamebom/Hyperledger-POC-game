using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
public class PlayerHealth : MonoBehaviour
{
    private SpriteLibraryAsset UI;
    private int HeartLimit;
    private PlayerAudio playerAudio;
    private GameObject[] PlayerUI;
    private Image[] hearts;
    private Sprite FullHeart;
    private Sprite HalfHeart;
    private Sprite EmptyHeart;
    private PlayerStats playerStats;
    private PlayerHelper playerHelper;

    void Start()
    {
        IntializeHealth();
        playerAudio = GetComponent<PlayerAudio>();
    }
    void Update()
    {
        updateHealth();
    }
    public int getPlayerHealth() {
        return playerStats.playerHealth;
    }
    void updateHealth()
    {
        playerStats.playerHealth = playerStats.playerHealth > HeartLimit * 2 ? HeartLimit * 2 : playerStats.playerHealth;
        for (int i = 0; i < HeartLimit; i++)
        {
            if (playerStats.playerHealth >= (i + 1) * 2)
            {
                hearts[i].sprite = FullHeart;
            }
            else if (playerStats.playerHealth == (i + 1) * 2 - 1)
            {
                hearts[i].sprite = HalfHeart;

            }
            else
            {
                hearts[i].sprite = EmptyHeart;

            }
            hearts[i].enabled = i < playerStats.maxHealth / 2 + playerStats.maxHealth % 2 ? true : false;
        }
    }
    void IntializeHealth()
    {
        PlayerUI = GameObject.FindGameObjectsWithTag("PlayerHealth");
        playerStats = GetComponent<PlayerStats>();
        playerHelper = GetComponent<PlayerHelper>();
        HeartLimit = playerStats.HealthLimit / 2 + playerStats.HealthLimit % 2;
        hearts = new Image[HeartLimit];
        for (int i = 0; i < HeartLimit; i++)
        {
            hearts[i] = PlayerUI[i].GetComponent<Image>();
        }
        FullHeart = hearts[0].sprite;
        HalfHeart = hearts[1].sprite;
        EmptyHeart = hearts[2].sprite;
    }
    public void LoseHealth(int damage)
    {

        playerAudio.PlaySound("PlayerDamage");
        if (!playerStats.isStaggered)
        {
            playerStats.playerHealth -= damage;
        }
        else
        {
            playerStats.playerHealth -= 2 * damage;
            playerStats.isStaggered = false;
            GetComponent<PlayerPosture>().StopStaggerRecovery();
        }
        if (playerStats.playerHealth == 1)
        {
            playerStats.criticalHealth = true;
            playerHelper.ActivateFuryItems();
        }
        else if (playerStats.playerHealth <= 0)
        {
            //  SaveHandler.GetInstance().Load();
            SceneManager.LoadScene("MainMenu");
        }


    }
    public void Heal(int heal)
    {
        playerStats.playerHealth += heal;
        playerStats.playerHealth = playerStats.playerHealth > playerStats.maxHealth ? playerStats.maxHealth : playerStats.playerHealth;
        playerHelper.DeactivateFuryItems();
    }
}
