using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameManager gm;

    void Start() {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D obj) {

        if(gm.isGameStateDefending) {
            if(obj.name == "Spike-Enemy(Clone)") {
                // Game Over Sound
                gm.playGameOverSoundEffect();
            }
        } else {
            if(obj.name == "Ball-Enemy(Clone)") {
                // Score Increase Sound
                gm.playScoreIncSoundEffect();
            }
        }
    }
}
