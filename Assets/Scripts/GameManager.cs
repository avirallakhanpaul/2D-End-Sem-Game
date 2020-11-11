using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Camera MainCamera;
    public Vector2 screenBounds;
    public bool isGameStateDefending; 
    private float screenWidth;
    private float screenHeight;
    public int score;
    public Text scoreText;
    public GameObject spike;
    public GameObject ball;
    private GameObject spikeEnemy;
    public float interval;

    void Start() {

        isGameStateDefending = true;

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        InvokeRepeating("InstantiateEnemy", 1, interval);
    }

    void InstantiateEnemy() {

        if(isGameStateDefending) {
            spikeEnemy = spike;
        } else if(!isGameStateDefending) {
            spikeEnemy = ball;
        }

        int range = Random.Range(1, 5);
        float enemyPositionX = Random.Range(-screenBounds.x, screenBounds.x);
        float enemyPositionY = Random.Range(-screenBounds.y, screenBounds.y);

        switch(range) {

            case 1:
                Instantiate(spikeEnemy);
                spikeEnemy.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                break;

            case 2:
                Instantiate(spikeEnemy);
                spikeEnemy.transform.position = new Vector2(screenBounds.x, enemyPositionY);
                break;

            case 3:
                Instantiate(spikeEnemy);
                spikeEnemy.transform.position = new Vector2(enemyPositionX, -screenBounds.y);
                break;

            case 4:
                Instantiate(spikeEnemy);
                spikeEnemy.transform.position = new Vector2(-screenBounds.x, enemyPositionY);
                break;
        }
    }

    void Update() {

        scoreText.text = score.ToString();
    }
}
