using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Axle : MonoBehaviour {
    
    GameManager gm;
    public float rotationSpeed;
    public AudioSource directionChangeSoundEffect;
    public bool isMainScene;
    public GameObject spikeTop;
    public GameObject spikeBottom;
    public GameObject ballTop;
    public GameObject ballBottom;
    public Vector2 axleScaleChange;
    public Vector2 spikeScaleChange;
    public Vector2 ballScaleChange;
    public MenuManager menuManager;

    void Start() {

        menuManager = GameObject.Find("Menu Manager").GetComponent<MenuManager>();

        axleScaleChange = new Vector2(0.13f, 10.0f);
        spikeScaleChange = new Vector2(0.9781532f, 0.01222692f);
        ballScaleChange = new Vector2(1.0f, 0.0125f);

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

        if(isMainScene && gm.score > 55) {
            if(!gm.isGameStateDefending) {
                gameObject.transform.localScale = axleScaleChange;
                spikeTop.transform.localScale = spikeScaleChange;
                spikeBottom.transform.localScale = spikeScaleChange;
            }
        }

        if(isMainScene && gm.isGameStateDefending) {
            gameObject.transform.localScale = new Vector2(0.1f, 8.0f);
            ballTop.transform.localScale = ballScaleChange;
            ballBottom.transform.localScale = ballScaleChange;
        }

        // X: 0.9781532 Y: 0.01222692 Small Spike

        // X: 1.25 Y: 0.015625 Normal Spike

        // X: 0.78 Y: 0.00975 Small Ball

        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if(isMainScene) {

            if(gm.forMobile) {

                if(!menuManager.isGamePaused) {

                    if(Input.touchCount > 0) {

                        Touch touch = Input.GetTouch(0);

                        if(touch.phase == TouchPhase.Began) {
                            rotationSpeed = -rotationSpeed;
                            directionChangeSoundEffect.Play();
                        }
                    }
                }

            } else {

                if(Input.GetKeyDown("space")) {

                    if (!menuManager.isGamePaused) {

                        rotationSpeed = -rotationSpeed;
                        directionChangeSoundEffect.Play();
                    }
                }
            }
        }
    }
}