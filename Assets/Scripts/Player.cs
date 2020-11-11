using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    GameManager gm;
    void Start() {

        gm = FindObjectOfType<GameManager>();
        
        if(gm.isGameStateDefending) {
            // gameObject
        }
    }

    void Update() {
        
    }
}
