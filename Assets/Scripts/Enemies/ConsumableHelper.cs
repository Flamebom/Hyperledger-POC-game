using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableHelper : MonoBehaviour
{
    private GameObject[] allGems;
    private GameObject[] allHealth;
    void Start()
    {
        allGems = Resources.LoadAll<GameObject>("Prefab/Items/Consumables/Gems");
        allHealth = Resources.LoadAll<GameObject>("Prefab/Items/Consumables/Health");
    }
    public GameObject createGem(int index)
    {
        return allGems[index - 1];
    }
    public GameObject createHealth(int index)
    {
        return allHealth[index - 1];
    }
}
