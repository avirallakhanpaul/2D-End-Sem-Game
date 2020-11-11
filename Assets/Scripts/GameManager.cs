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
    public int score = 0;
    public Text scoreText;
    public GameObject spike;
    public GameObject ball;
    private GameObject enemy;
    public float interval;
    public bool isGameOver;
    GameObject[] ballPlayerPrefabs;
    GameObject[] spikePlayerPrefabs; 
    // GameObject[] gamePrefabs;

    void Start() {

        ballPlayerPrefabs = GameObject.FindGameObjectsWithTag("ball-player");
        spikePlayerPrefabs = GameObject.FindGameObjectsWithTag("spike-player");
        
        isGameStateDefending = true;

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        InvokeRepeating("InstantiateEnemy", 1, interval);
    }

    void InstantiateEnemy() {

        if(isGameStateDefending) {

            foreach (GameObject ballPlayerPrefab in ballPlayerPrefabs) {
                ballPlayerPrefab.SetActive(true);
            }
            
            foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
                spikePlayerPrefab.SetActive(false);
            }

            enemy = spike;

        } else if(!isGameStateDefending) {

            Debug.Log("Attack Mode!");

            foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
                spikePlayerPrefab.SetActive(true);
            }

            foreach (GameObject ballPlayerPrefab in ballPlayerPrefabs) {
                ballPlayerPrefab.SetActive(false);
            }

            enemy = ball;
        }

        int range = Random.Range(1, 5);
        float enemyPositionX = Random.Range(-screenBounds.x, screenBounds.x);
        float enemyPositionY = Random.Range(-screenBounds.y, screenBounds.y);

        switch(range) {

            case 1:
                Instantiate(enemy);
                enemy.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                break;

            case 2:
                Instantiate(enemy);
                enemy.transform.position = new Vector2(screenBounds.x, enemyPositionY);
                break;

            case 3:
                Instantiate(enemy);
                enemy.transform.position = new Vector2(enemyPositionX, -screenBounds.y);
                break;

            case 4:
                Instantiate(enemy);
                enemy.transform.position = new Vector2(-screenBounds.x, enemyPositionY);
                break;
        }
    }

    void Update() {

        scoreText.text = score.ToString();

        if(Input.GetKeyDown(KeyCode.A)) {
            isGameStateDefending = !isGameStateDefending;
        }
    }
}

// Destroy all the game objects except the player when switching between the states
// Increase the movement speed of the enemies after fixed intervals of time by a fixed amount
// Add OnTriggerEnter to spike player
// Change the color of the spike player from white to something else
// Add Powerups(speed boost, state change)