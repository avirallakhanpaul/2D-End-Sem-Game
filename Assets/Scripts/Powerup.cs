using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public Transform axleMidPoint;
    public float currentSpeed;
    public GameManager gm;

    void Start() {

        gm = GameObject.FindObjectOfType<GameManager>();
        axleMidPoint = GameObject.FindGameObjectWithTag("axle-mid-point").transform;
        currentSpeed = 5.0f;
    }

    void Update() {

        if(gm.isGameOver) {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, axleMidPoint.position, currentSpeed * Time.deltaTime);
        transform.Rotate(0, 0, 60 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D obj) {

        if(obj.CompareTag("axle")) {
            Destroy(gameObject);
            gm.changeGameState();
        }
    }
}
