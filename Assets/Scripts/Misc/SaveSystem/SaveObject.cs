using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{
    public SystemSaveObject playerSaveObject;
    public SystemSaveObject gameEngineSaveObject;
    public int playermaxHealth;
    public int playerHealth;
    public string scene;
    public int keys;
    public float PlayerX;
    public float PlayerY;
    public int doorIndex;
    public int npcIndex;
    public string[] storyKeys;
    public string[] fakeKeys;

    public SaveObject(PlayerStats playerStats)
    {
        npcIndex = StoryKey.npcIndex;
        doorIndex = StoryKey.doorIndex;
        storyKeys = StoryKey.storyKeys;
        fakeKeys = StoryKey.fakeKeys;
        scene = playerStats.gameObject.scene.name;
        playerHealth = playerStats.playerHealth;
        keys = playerStats.keys;
        playermaxHealth = playerStats.maxHealth;
        PlayerX = playerStats.transform.position.x;
        PlayerY = playerStats.transform.position.y;
        playerSaveObject = new SystemSaveObject(playerStats.gems, playerStats.keys, playerStats.PassiveInventory);
        //temp
        gameEngineSaveObject = new SystemSaveObject(1000, 0, playerStats.PassiveInventory);
    }
}
