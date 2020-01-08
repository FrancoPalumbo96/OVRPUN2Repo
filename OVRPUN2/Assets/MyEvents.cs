using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEvents : MonoBehaviour {

    public static MyEvents current;

    
    public event Action playerEnteredRoom;

    public void onPlayerEnteredRoom() {
        if (playerEnteredRoom != null) {
            playerEnteredRoom();
        }
    }
    
    // Start is called before the first frame update
    void Awake() {
        current = this;
    }

   
}
