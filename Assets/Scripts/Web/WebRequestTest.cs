using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using UnityEngine.Networking;
using System;

public static class WebRequestTest
{
    const string cookie = "connect.sid=s%3Ae9CFBB1ocUVFxWKpU2r-oCkp0ElJ_N-R.WyQ2IG1VVnEYEq3F%2BldhioQydVjodYZVL1Fl5XfDi7o";
    const string authorizationKey = "Basic c2FuZHkuYWdnYXJ3YWwuYXBwc0BnbWFpbC5jb206YzUwMjIxZWUtNzkyMy00MDgyLTg3MmYtZWYxMTEyMDE5Y2Y5LTBjMjhiYWZkLTMxNGMtNGFiYS1hMmNlLTM5MjZmY2MxYzdhZA==";
    static public MoneyOutput[] AllMoney;
    static public WebOutput[] AllNFTs;
    static public Dictionary<string, string> WalletNameDictionary = new Dictionary<string, string>();
    static public Dictionary<string, string> WalletAddressDictionary = new Dictionary<string, string>();
    static HttpClient client;
    public static void intializeClient()
    {
        client = new HttpClient();
    }
    async public static void updateWalletKeys()
    {
        var walletrequest = new HttpRequestMessage(HttpMethod.Get, "https://asset-platform.hyperledger.eap.kaleido.io/api/v1/wallets/new-game-wallet/keys");
        walletrequest.Headers.Add("Authorization", authorizationKey);
        walletrequest.Headers.Add("Cookie", cookie);
        var response = await client.SendAsync(walletrequest);
        string responseString = await response.Content.ReadAsStringAsync();
        // Debug.Log(responseString);
        responseString = fixJson(responseString);
        WalletOutput[] AllWallets = JsonHelper.FromJson<WalletOutput>(responseString);
        initalizeWalletDictionary(AllWallets);
    }
    private static void initalizeWalletDictionary(WalletOutput[] wallets)
    {
        WalletNameDictionary.Add("", "unknown");
        WalletAddressDictionary.Add("unknown", "");
        foreach (WalletOutput wallet in wallets)
        {
            WalletNameDictionary.Add(wallet.address, wallet.name);
            WalletAddressDictionary.Add(wallet.name, wallet.address);
        }

    }
    async public static void updateMoney()
    {
        var moneyreqeuest = new HttpRequestMessage(HttpMethod.Get, "https://asset-platform.hyperledger.eap.kaleido.io/api/v1/namespaces/hl-gaming-poc/tokens/balances?limit=25");
        moneyreqeuest.Headers.Add("Authorization", authorizationKey);
        moneyreqeuest.Headers.Add("Cookie", cookie);
        var response = await client.SendAsync(moneyreqeuest);
        string responseString = await response.Content.ReadAsStringAsync();
        responseString = fixJson(responseString);
        AllMoney = JsonHelper.FromJson<MoneyOutput>(responseString);

    }
    async public static void updateAssetMatrix()
    {
        List<WebOutput> output = new List<WebOutput>();
        string[] requests = { "https://asset-platform.hyperledger.eap.kaleido.io/api/v1/nfts/hl-gaming-poc/collections/8e86f9e3-d5ce-46c9-93c6-89d21cbfd71c/nfts", "https://asset-platform.hyperledger.eap.kaleido.io/api/v1/nfts/hl-gaming-poc/collections/357b2efb-4475-4db2-b38e-4c63b14ce8c9/nfts" };
        for (int i = 0; i < requests.Length; i++)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requests[i]);
            request.Headers.Add("Authorization", authorizationKey);
            request.Headers.Add("Cookie", cookie);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
         //   Debug.Log(responseString);
            responseString = fixJson(responseString);
            output.AddRange(JsonHelper.FromJson<WebOutput>(responseString));
        }
        AllNFTs = output.ToArray();
    }
    public static void PatchAssetMatrix()
    {

        AssetNFTS[] loadedAssets = JsonHelper.FromJson<AssetNFTS>(SaveSystem.LoadAssets());
        for (int i = 0; i < loadedAssets.Length; i++)
        {

            if (!(loadedAssets[i].allocatedToName.Equals(AllNFTs[i].allocatedTo)))
            {
                patchHelper(String.Format("https://asset-platform.hyperledger.eap.kaleido.io/api/v1/nfts/hl-gaming-poc/collections/{1}/nfts/{0}", loadedAssets[i].onChainAssetID, AllNFTs[i].collectionId), String.Format("{{ \"allocatedTo\":\"{0}\",\"metadata\": {{}}}}", loadedAssets[i].allocatedToAddress));
            }


        }
    }
    public static void ResetAssetMatrix()
    {
        AssetNFTS[] loadedAssets = new AssetNFTS[AllNFTs.Length];
        for (int i = 0; i < loadedAssets.Length; i++) {
            loadedAssets[i] = new AssetNFTS(AllNFTs[i],i);
        }
        for (int i = 0; i < loadedAssets.Length; i++)
        { patchHelper(String.Format("https://asset-platform.hyperledger.eap.kaleido.io/api/v1/nfts/hl-gaming-poc/collections/{1}/nfts/{0}", loadedAssets[i].onChainAssetID, AllNFTs[i].collectionId), String.Format("{{ \"allocatedTo\":\"{0}\",\"metadata\": {{}}}}", loadedAssets[i].mintedToAddress)); }
    }

    private static void patchHelper(String uri, String body)
    {
        UnityWebRequest request = UnityWebRequest.Put(uri, body);
        request.method = "PATCH";
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", authorizationKey);
        request.SetRequestHeader("Cookie", cookie);
        request.SendWebRequest();
    }
    private static string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}

