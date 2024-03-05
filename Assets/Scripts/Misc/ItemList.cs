using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemList : MonoBehaviour
{
    private GameObject[] AllCTierItems;
    private GameObject[] AllDTierItems;
    private void Start()
    {
        AllCTierItems = Resources.LoadAll<GameObject>("Prefab/Items/HeartContainers");
        AllDTierItems = Resources.LoadAll<GameObject>("Prefab/Items/AllDTierItems");
    }
    public GameObject create(int rareity)
    {
        int num;
        switch (rareity)
        {
            case 5:
                num = Random.Range(0, AllDTierItems.Length);
                return AllDTierItems[num];
            case 4:

                num = Random.Range(0, AllCTierItems.Length);
                return AllCTierItems[num];



        }
        return null;
    }
    public GameObject[] listofItems(int rareity)
    {
        switch (rareity)
        {
            case 5:
                return AllDTierItems;
            case 4:
                return AllCTierItems;
        }
        return null;
    }
}