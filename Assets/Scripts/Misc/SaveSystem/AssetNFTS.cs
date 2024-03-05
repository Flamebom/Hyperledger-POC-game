using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AssetNFTS
{
    public int localAssetID;
    public string assetType, assetName, mintedToName, allocatedToName, mintedToAddress, allocatedToAddress, onChainAssetID;
    public AssetNFTS(int localAssetID, string assetType, string assetName, string currentOwner, string newOwner, string currentOwneraddress, string newOwneradress, string onChainAssetID)
    {
        this.onChainAssetID = onChainAssetID;
        this.assetType = assetType;
        this.assetName = assetName;
        this.mintedToName = currentOwner;
        this.allocatedToName = newOwner;
        this.mintedToAddress = currentOwneraddress;
        this.allocatedToAddress = newOwneradress;
        this.localAssetID = localAssetID;

    }
    public AssetNFTS(WebOutput webOutput, int idtoSave) : this(idtoSave, webOutput.collectionId, webOutput.name, WebRequestTest.WalletNameDictionary[webOutput.mintedTo], WebRequestTest.WalletNameDictionary[webOutput.allocatedTo], webOutput.mintedTo, webOutput.allocatedTo, webOutput.id) { }

}


