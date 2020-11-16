using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private GameObject enemy;
    public GameObject powerup;
    public float interval;
    public bool isGameOver;
    GameObject[] spikePlayerPrefabs;
    GameObject[] ballPlayerPrefabs;
    GameObject[] enemies;
    GameObject[] powerups;
    public AudioSource[] soundEffects;
    public AudioSource scoreIncSoundEffect;
    public AudioSource gameOverSoundEffect;
    public AudioSource gameStateChangeSoundEffect;
    public Canvas GameOverCanvas;
    public bool forMobile;

    void Start() {

        ballPlayerPrefabs = GameObject.FindGameObjectsWithTag("ball-player");
        spikePlayerPrefabs = GameObject.FindGameObjectsWithTag("spike-player");

        foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
            spikePlayerPrefab.SetActive(false);
        }

        isGameStateDefending = true;

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        InvokeRepeating("InstantiateEnemyAndPowerup", 1, interval);

        soundEffects = GetComponents<AudioSource>();
        scoreIncSoundEffect = soundEffects[0];
        gameOverSoundEffect = soundEffects[1];
        gameStateChangeSoundEffect = soundEffects[2];

        GameOverCanvas.gameObject.SetActive(false);
    }

    void InstantiateEnemyAndPowerup() {

        if(isGameStateDefending) {

            foreach (GameObject ballPlayerPrefab in ballPlayerPrefabs) {
                ballPlayerPrefab.SetActive(true);
            }
            foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
                spikePlayerPrefab.SetActive(false);
            }

            enemy = spike;

        } else if(!isGameStateDefending) {

            foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
                spikePlayerPrefab.SetActive(true);
            }
            foreach (GameObject ballPlayerPrefab in ballPlayerPrefabs) {
                ballPlayerPrefab.SetActive(false);
            }

            enemy = ball;
        }

        float enemyPositionX = Random.Range(-screenBounds.x, screenBounds.x);
        float enemyPositionY = Random.Range(-screenBounds.y, screenBounds.y);

        int range = Random.Range(1, 5);

        switch(range) {

            case 1:

                if(Random.Range(0, 14) == Random.Range(0, 14)) {
                    Instantiate(powerup);
                    powerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else {
                Instantiate(enemy);
                enemy.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                }

                break;

            case 2:

                if(Random.Range(0, 14) == Random.Range(0, 14)) {
                    Instantiate(powerup);
                    powerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else {
                Instantiate(enemy);
                enemy.transform.position = new Vector2(screenBounds.x, enemyPositionY);
                }

                break;

            case 3:

                if (Random.Range(0, 14) == Random.Range(0, 14)) {
                    Instantiate(powerup);
                    powerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else {
                Instantiate(enemy);
                enemy.transform.position = new Vector2(enemyPositionX, -screenBounds.y);
                }

                break;

            case 4:

                if (Random.Range(0, 14) == Random.Range(0, 14)) {
                    Instantiate(powerup);
                    powerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else {
                Instantiate(enemy);
                enemy.transform.position = new Vector2(-screenBounds.x, enemyPositionY);
                }

                break;
        }
    }

    void Update() {

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        powerups = GameObject.FindGameObjectsWithTag("powerup");

        if(isGameOver) {

            CancelInvoke();
            GameOverCanvas.gameObject.SetActive(true);
            return;
        }

        // if(Input.GetKeyDown(KeyCode.A)) {
        //     isGameStateDefending = !isGameStateDefending;
            
        //     foreach (GameObject enemy in enemies) {
        //         Destroy(enemy);
        //     }
        // }

        if(score == 0) {
            scoreText.text = "";
        }  else {
            scoreText.text = score.ToString();
        }
    }

    public void changeGameState() {

        isGameStateDefending = !isGameStateDefending;
        gameStateChangeSoundEffect.Play();

        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }

        foreach (GameObject powerup in powerups) {
            Destroy(powerup);
        }
    } 

    public void playScoreIncSoundEffect() {
        scoreIncSoundEffect.Play();
    }

    public void playGameOverSoundEffect() {
        gameOverSoundEffect.Play();
    }
}

// Instruction on Playing Screen