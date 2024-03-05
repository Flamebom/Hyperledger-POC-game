using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemSaveObject 
{
    public int gems;
    public int storeKeys;
    public List<string> inventoryK = new List<string>();
    public List<int> inventoryV = new List<int>();
    public SystemSaveObject(int gems, int keys, Dictionary<string,int> items) {
        foreach (KeyValuePair<string, int> item in items)
        {
            inventoryK.Add(item.Key);
            inventoryV.Add(item.Value);
        }
        storeKeys = keys;
        this.gems = gems;
    }

}
