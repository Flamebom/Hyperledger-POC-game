using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveHandler : MonoBehaviour
{
    public static bool loading;
    private PlayerStats playerStats;
    private static SaveHandler instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one static save handler");
        }
        instance = this;
        SaveSystem.Init();
    }
    public void Save()
    {
        string jsonSave = JsonUtility.ToJson(new SaveObject(playerStats));
        List<AssetNFTS> assetSave = new List<AssetNFTS>();
        WebOutput[] toSave = WebRequestTest.AllNFTs;
        int idtoSave = 0;
        foreach (WebOutput webOutput in toSave)
        {

            assetSave.Add(new AssetNFTS(webOutput, idtoSave));
            idtoSave++;

        }
        
        foreach (AssetNFTS asset in assetSave)
        {

            if (asset.localAssetID >= 0 && asset.localAssetID <= 2)
            {
                int index = asset.assetName[asset.assetName.Length - 1] - '1';
                asset.allocatedToName = StoryKey.storyKeys[index];
                asset.allocatedToAddress = WebRequestTest.WalletAddressDictionary[StoryKey.storyKeys[index]];
            }
            if (asset.localAssetID >= 3 && asset.localAssetID <= 6)
            {
                int index = asset.assetName[asset.assetName.Length - 1] - '1';
                asset.allocatedToName = StoryKey.fakeKeys[index];
                asset.allocatedToAddress = WebRequestTest.WalletAddressDictionary[StoryKey.fakeKeys[index]];

            }
        }
        string assetsSave = JsonHelper.ToJson(assetSave.ToArray());
        GemSaveObject[] gemSave = new GemSaveObject[5];
        gemSave[0] = new GemSaveObject("player-key", 0, WebRequestTest.WalletAddressDictionary["player-key"]);
        gemSave[1] = new GemSaveObject("npc-1", 0, WebRequestTest.WalletAddressDictionary["npc-1"]);
        gemSave[2] = new GemSaveObject("npc-2", 0, WebRequestTest.WalletAddressDictionary["npc-2"]);
        gemSave[3] = new GemSaveObject("npc-3", 0, WebRequestTest.WalletAddressDictionary["npc-3"]);
        gemSave[4] = new GemSaveObject("game-engine-key", 0, WebRequestTest.WalletAddressDictionary["game-engine-key"]);
        for (int i = 0; i < WebRequestTest.AllMoney.Length; i++)
        {
            int gemSaveIndex = 0;
            //Use try catch statement for wallet addrtess not in key
            switch (WebRequestTest.WalletNameDictionary[WebRequestTest.AllMoney[i].key])
            {
                case "player-key":
                    gemSaveIndex = 0;
                    break;
                case "npc-1":
                    gemSaveIndex = 1;
                    break;
                case "npc-2":
                    gemSaveIndex = 2;
                    break;
                case "npc-3":
                    gemSaveIndex = 3;
                    break;
                case "game-engine":
                    gemSaveIndex = 4;
                    break;

            }
            gemSave[gemSaveIndex].value++;
            //gemSave[gemSaveIndex].value += int.Parse(WebRequestTest.AllMoney[i].balance);

        }

        string gameMoney = JsonHelper.ToJson(gemSave);

        SaveSystem.Save(jsonSave, assetsSave, gameMoney);
        WebRequestTest.PatchAssetMatrix();

    }
    private void Start()
    {
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
    }
    public void Load()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
            LoadtoStats(loadedObject);

        }
    }
    public static SaveHandler GetInstance()
    {
        return instance;
    }
    private void LoadtoStats(SaveObject savedObject)
    {
        StoryKey.doorIndex = savedObject.doorIndex;
        StoryKey.npcIndex = savedObject.npcIndex;
        StoryKey.fakeKeys = savedObject.fakeKeys;
        StoryKey.storyKeys = savedObject.storyKeys;

        PersistentData.loadFromSaveObject(savedObject);
        SceneManager.LoadScene(savedObject.scene);




    }
    public SaveObject getSaveObject()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
            {

                return loadedObject;
            }

        }
        return null;
    }
}


