using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PostureBarScript : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        SetMaxPosture(playerStats.MaxPosture) ;
        SetPosture(playerStats.Posture);
    }
    public void SetPosture(float posture) {
        slider.value = slider.maxValue - posture;
    }
    public void SetMaxPosture(float maxposture) {
        slider.maxValue = maxposture;
    }

     void Update()
    {
        SetPosture(playerStats.Posture);
    }
}
