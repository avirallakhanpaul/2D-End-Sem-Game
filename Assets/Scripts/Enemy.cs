using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform axleMidPoint;
    public float movementSpeed;
    public GameManager gm;

    void Start() {

        gm = GameManager.FindObjectOfType<GameManager>();
        axleMidPoint = GameObject.FindGameObjectWithTag("axle-mid-point").transform;    
    }

    void Update() {
        
        transform.position = Vector3.MoveTowards(transform.position, axleMidPoint.position, movementSpeed * Time.deltaTime);
        transform.Rotate(0, 0, 60 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D obj) {

        if(gm.isGameStateDefending) {
        
            if(obj.CompareTag("axle")) {

                Destroy(gameObject);
                gm.score++;
            } else if(obj.CompareTag("ball-player")) {

                Destroy(gameObject);
                gm.isGameOver = true;
            }

        } else if(!(gm.isGameStateDefending)) {

            if(obj.CompareTag("axle")) {

                Destroy(gameObject);
                gm.isGameOver = true;
            } else if(obj.CompareTag("spike-player")) {

                Destroy(gameObject);
                gm.score++;
            }
        }

    }
}
