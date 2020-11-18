using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Axle : MonoBehaviour {
    
    GameManager gm;
    public float rotationSpeed;
    public AudioSource directionChangeSoundEffect;
    public bool isMainScene;

    void Start() {

        if(SceneManager.GetActiveScene().name == "Main") {

            isMainScene = true;
            gm = GameObject.FindObjectOfType<GameManager>();
            directionChangeSoundEffect = GetComponent<AudioSource>();
        }
    }

    void Update() {

        if(isMainScene) {
            if(gm.isGameOver) {
                return;
            }
        }

        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if(isMainScene) {

            if(gm.forMobile) {

                if(Input.touchCount > 0) {

                    Touch touch = Input.GetTouch(0);

                    if(touch.phase == TouchPhase.Began) {
                        rotationSpeed = -rotationSpeed;
                        directionChangeSoundEffect.Play();
                    }
                }
            } else {

                if(Input.GetKeyDown("space")) {
                    rotationSpeed = -rotationSpeed;
                    directionChangeSoundEffect.Play();
                }
            }
        }
    }

    // void OnTriggerEnter2D(Collider2D obj) {

    //     if(gm.isGameStateDefending) {

    //         if (obj.name == "Spike-Enemy(Clone)") {
    //             // Score Increase Sound
    //             gm.playScoreIncSoundEffect();
    //         } else if(obj.name == "Game State Swap Powerup") {
    //             // Game State Change Sound 
    //         }
    //     } else {

    //         if (obj.name == "Ball-Enemy(Clone)") {
    //             // Game Over Sound
    //             gm.playGameOverSoundEffect();
    //         }
    //     }
    // }
}