using UnityEngine;
using System.Collections.Generic;
public static class PersistentData
{
    public static int playerHealth = 6;
    public static int maxHealth = 6;
    public static Dictionary<string, int> PassiveInventory = new Dictionary<string, int>();
    public static Dictionary<string, int> WeaponInventory = new Dictionary<string, int>();
    public static int gems = 0;
    public static Vector3 lastPosition = new Vector3();
    public static int keys = 0;

    public static void loadFromSaveObject(SaveObject s)
    {
        playerHealth = s.playerHealth;
        maxHealth = s.playermaxHealth;
        for (int i = 0; i < s.playerSaveObject.inventoryK.Count; i++) {
            PassiveInventory.Add(s.playerSaveObject.inventoryK[i], s.playerSaveObject.inventoryV[i]);
        }
        gems = s.playerSaveObject.gems;
        lastPosition = new Vector2(s.PlayerX, s.PlayerY);
    }
    public static void loadToPlayer()
    {
        PlayerStats playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        playerStats.PassiveInventory = new Dictionary<string, int>();
        if (PassiveInventory != null)
        {
            ItemList itemList = GameObject.FindGameObjectWithTag("ItemList").GetComponent<ItemList>();


            foreach (KeyValuePair<string, int> entry in PassiveInventory)
            {
                GameObject[] items = itemList.listofItems(entry.Value);
                for (int i = 0; i < items.Length; i++)
                {


                    if (entry.Key.Equals((items[i].GetComponent<Item>().item.name)))
                    {
                        items[i].GetComponent<Item>().Load();

                    }
                }



            }
            playerStats.gems = gems;
            playerStats.playerHealth = playerHealth;
            playerStats.keys = keys;
            playerStats.transform.position = new Vector2(lastPosition.x, lastPosition.y);
        }

    }
}
