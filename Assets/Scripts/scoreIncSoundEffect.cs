using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreIncSoundEffect : MonoBehaviour {

    public AudioSource soundEffect;

    void Start() {
        soundEffect = GetComponent<AudioSource>();
    }

    void Update() {

    }

    void OnTriggerEnter2D(Collider2D obj) {

        if (obj.name == "Spike-Enemy(Clone)") {
            soundEffect.Play();
        } else if(obj.name == "Ball-Enemy(Clone)") {
            Debug.Log("Ball hit the center");
        }
    }
}
