using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GemSaveObject
{
    public string ownerName;
    public int value;
    public string ownerAddress;
    public GemSaveObject(string ownerName, int value, string ownerAddress)
    {
        this.ownerName = ownerName;
        this.value = value;
        this.ownerAddress = ownerAddress;
    }

}
