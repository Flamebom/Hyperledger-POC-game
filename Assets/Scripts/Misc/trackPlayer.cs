using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackPlayer : MonoBehaviour
{
    public GameObject player; 
    void Start()
    {
        trackPlayerObject();  
    }
   private void trackPlayerObject() {
        player = PlayerReference.player;
        Invoke("trackPlayerObject", 1);

    }
    
}
