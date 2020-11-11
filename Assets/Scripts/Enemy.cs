using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform axleMidPoint;
    public float movementSpeed;
    public float speedIncValue;
    public float temp;
    public GameManager gm;

    void Start() {

        gm = FindObjectOfType<GameManager>();
        axleMidPoint = GameObject.FindGameObjectWithTag("axle-mid-point").transform;  
        temp = movementSpeed;
    }

    void Update() {

        // Debug.Log(movementSpeed);

        // if(gm.score >= 5) {

        //     if(!(movementSpeed > temp)) {
        //         temp = movementSpeed;
        //         increaseSpeed();
        //     }
        // }
        
        transform.position = Vector3.MoveTowards(transform.position, axleMidPoint.position, movementSpeed * Time.deltaTime);
        transform.Rotate(0, 0, 60 * Time.deltaTime);
    }

    void increaseSpeed() {
        movementSpeed += speedIncValue;
    }

    void OnTriggerEnter2D(Collider2D obj) {

        if(obj.CompareTag("axle")) {

            Destroy(gameObject);
            gm.score++;
        } else if(obj.CompareTag("ball-player")) {
            Destroy(gameObject);
            gm.isGameOver = true;
        }
    }
}
