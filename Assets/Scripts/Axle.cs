using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axle : MonoBehaviour {
    
    GameManager gm;
    public float rotationSpeed;

    void Start() {

        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update() {

        if(gm.isGameOver) {
            return;
        }

        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if(gm.forMobile) {

            if(Input.touchCount > 0) {

                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began) {
                    rotationSpeed = -rotationSpeed;
                }
            }
        } else {

            if(Input.GetKeyDown("space")) {
                rotationSpeed = -rotationSpeed;
            }
        }
    }
}
