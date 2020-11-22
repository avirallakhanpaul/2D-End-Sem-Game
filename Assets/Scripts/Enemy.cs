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
        maxSpeed = 12.0f;
    }

    void Update() {

        if (gm.isGameOver) {
            Destroy(gameObject);
            return;
        }

        if(gm.score > 15 && gm.score < 45) {
            if(!(currentSpeed >= maxSpeed)) {
                currentSpeed += 2.0f;
            }
        } else if(gm.score >= 45) {

            if(speedBool) {
                currentSpeed = maxSpeed;
                maxSpeed = 16.0f;
                gm.interval = 1.5f;
                speedBool = false;
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
