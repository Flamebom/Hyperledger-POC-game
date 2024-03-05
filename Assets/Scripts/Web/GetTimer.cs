using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTimer : MonoBehaviour
{
    public void Start()
    {
    }
    private float timer;
    public void intialize()
    {
        WebRequestTest.intializeClient();
        timer = 60.0f;
        WebRequestTest.updateWalletKeys();
        WebRequestTest.updateAssetMatrix();
        WebRequestTest.updateMoney();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            timer = 60.0f;
            WebRequestTest.updateAssetMatrix();
        }
    }
}
