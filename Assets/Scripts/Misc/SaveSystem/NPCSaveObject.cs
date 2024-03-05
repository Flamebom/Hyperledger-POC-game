using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCSaveObject
{
    public int gems;
    public int storeKeys;
    public NPCSaveObject(int gems, int storeKeys) {
        this.gems = gems;
        this.storeKeys = storeKeys;
    }
}
