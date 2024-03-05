using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DoorInteractable : Interactable
{
    bool isOpen = false;
    PlayerStats playerStats;
    private void Start()
    {
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
    }
    public override void Interact()
    {
        if (isOpen) {
            if (StoryKey.doorIndex != 3)
            {
                SaveHandler.GetInstance().Save();
                PersistentData.loadFromSaveObject(SaveHandler.GetInstance().getSaveObject());
                PersistentData.lastPosition = new Vector2(0,120);
                SceneManager.LoadScene(String.Format("Level{0}", StoryKey.doorIndex + 1));
            }
            else {
                SceneManager.LoadScene("WinScreen");
            }
        }
        if ((StoryKey.storyKeys[StoryKey.doorIndex]).Equals("player-key"))
        {
           // SaveHandler.GetInstance().ave();
            isOpen = true;
            StoryKey.storyKeys[StoryKey.doorIndex] = "game-engine";
            StoryKey.doorIndex++;
            GetComponent<Animator>().SetBool("isOpen", isOpen);
        }
    }
}
