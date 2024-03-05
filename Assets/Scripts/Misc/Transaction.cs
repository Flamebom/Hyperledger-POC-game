using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Transaction : MonoBehaviour
{
    PlayerStats playerStats;
    void Start()
    {
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
    }
    public bool buyStoryKey(int gemCost)
    {

        if (playerStats.gems >= gemCost && StoryKey.npcIndex <=2)
        {

            if (bool.Parse(DialogueManager.GetInstance().GetVariableState(String.Format("npc{0}Trust",StoryKey.npcIndex+1)).ToString()))
            {
                StoryKey.storyKeys[StoryKey.npcIndex] = "player-key";
            }
            else { StoryKey.fakeKeys[StoryKey.npcIndex] = "player-key"; }
            playerStats.gems -= gemCost;
            StoryKey.npcIndex++;
            return true;

        }
        else
        {
            return false;
        }
    }
}
