using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axle : MonoBehaviour {
    public float rotationSpeed;

    void Start() {

    }

    void Update() {

        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown("space")) {

            rotationSpeed = -rotationSpeed;
        }
    }
}
