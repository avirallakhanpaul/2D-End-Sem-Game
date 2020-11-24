using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform axleMidPoint;
    public float currentSpeed;
    public float maxSpeed;
    private float accelerationTime;
    public GameManager gm;
    public AudioSource scoreIncSoundEffect;
    public bool speedBool = true;
    void Start() {

        gm = GameManager.FindObjectOfType<GameManager>();
        axleMidPoint = GameObject.FindGameObjectWithTag("axle-mid-point").transform;
        scoreIncSoundEffect = GetComponent<AudioSource>();
        currentSpeed = 5.0f;
        maxSpeed = 7.0f;
    }

    void Update() {

        if (gm.isGameOver) {
            Destroy(gameObject);
            return;
        }

        if(gm.gameTime >= 30.0f && gm.gameTime < 55.0f) {  // score > 15 and < 45 | gameTime >= 25 && gameTime > 45
            if(!(currentSpeed >= maxSpeed)) {
                currentSpeed += 2.0f;
            }
        } else if(gm.gameTime >= 55.0f) {  // score >= 45

            if(speedBool) {
                speedBool = false;
                currentSpeed = maxSpeed;
                maxSpeed = 11.0f;
            }

            if(!(currentSpeed >= maxSpeed)) {
                currentSpeed += 4.0f;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, axleMidPoint.position, currentSpeed * Time.deltaTime);
        transform.Rotate(0, 0, 60 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D obj) {

        if(gm.isGameStateDefending) {
        
            if(obj.CompareTag("axle")) {

                Destroy(gameObject);
                gm.score++;
                gm.playScoreIncSoundEffect();
            } else if(obj.CompareTag("ball-player")) {

                Destroy(gameObject);
                gm.isGameOver = true;
                gm.playGameOverSoundEffect();
            }

        } else if(!(gm.isGameStateDefending)) {

            if(obj.CompareTag("axle")) {

                Destroy(gameObject);
                gm.isGameOver = true;
                gm.playGameOverSoundEffect();
            } else if(obj.CompareTag("spike-player")) {

                Destroy(gameObject);
                gm.score++;
                gm.playScoreIncSoundEffect();
            }
        }

    }
}
