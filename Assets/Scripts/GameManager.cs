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
    public Text highScoreTitleText;
    public GameObject defenseModeTextGameObject;
    public GameObject attackModeTextGameObject;
    public Text highScoreText;
    public GameObject spike;
    public GameObject ball;
    private GameObject enemy;
    public GameObject stateChangePowerup;
    public GameObject otherPowerup;
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
    public Canvas InstructionsCanvas;
    public int highScore;
    public bool hasExecuted = false; 
    public float gameTime;
    Axle axleScript;
    public bool forMobile;

    void Start() {

        if(Application.platform == RuntimePlatform.Android) {
            forMobile = true;
        }

        isGameStateDefending = true;

        highScore = PlayerPrefs.GetInt("HighScore");

        showInstructions();

        ballPlayerPrefabs = GameObject.FindGameObjectsWithTag("ball-player");
        spikePlayerPrefabs = GameObject.FindGameObjectsWithTag("spike-player");

        foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
            spikePlayerPrefab.SetActive(false);
        }

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
    
        interval = 2.0f;
        InvokeRepeating("instantiateEnemyAndPowerup", 4, interval);

        axleScript = GameObject.FindGameObjectWithTag("axle").GetComponent<Axle>();
        soundEffects = GetComponents<AudioSource>();
        scoreIncSoundEffect = soundEffects[0];
        gameOverSoundEffect = soundEffects[1];
        gameStateChangeSoundEffect = soundEffects[2];

        GameOverCanvas.gameObject.SetActive(false);
    }

    void instantiateEnemyAndPowerup() {

        float enemyPositionX = Random.Range(-screenBounds.x, screenBounds.x);
        float enemyPositionY = Random.Range(-screenBounds.y, screenBounds.y);

        int range = Random.Range(1, 5);

        switch(range) {

            case 1:

                if(Random.Range(0, 10) == Random.Range(0, 10)) {
                    Instantiate(stateChangePowerup);
                    stateChangePowerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else if(Random.Range(0, 12) == Random.Range(0, 12)) {
                    Instantiate(otherPowerup);
                    otherPowerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else {
                    Instantiate(enemy);
                    enemy.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                }

                break;

            case 2:

                if(Random.Range(0, 10) == Random.Range(0, 10)) {
                    Instantiate(stateChangePowerup);
                    stateChangePowerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else if (Random.Range(0, 12) == Random.Range(0, 12)){
                    Instantiate(otherPowerup);
                    otherPowerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else {
                    Instantiate(enemy);
                    enemy.transform.position = new Vector2(screenBounds.x, enemyPositionY);
                }

                break;

            case 3:

                if (Random.Range(0, 10) == Random.Range(0, 10)) {
                    Instantiate(stateChangePowerup);
                    stateChangePowerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else if (Random.Range(0, 12) == Random.Range(0, 12)) {
                    Instantiate(otherPowerup);
                    otherPowerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else {
                Instantiate(enemy);
                enemy.transform.position = new Vector2(enemyPositionX, -screenBounds.y);
                }

                break;

            case 4:

                if (Random.Range(0, 10) == Random.Range(0, 10)) {
                    Instantiate(stateChangePowerup);
                    stateChangePowerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else if (Random.Range(0, 12) == Random.Range(0, 12)) {
                    Instantiate(otherPowerup);
                    otherPowerup.transform.position = new Vector2(enemyPositionX, screenBounds.y);
                } else {
                Instantiate(enemy);
                enemy.transform.position = new Vector2(-screenBounds.x, enemyPositionY);
                }

                break;
        }
    }

    void Update() {

        gameTime = Time.timeSinceLevelLoad;
        Debug.Log(gameTime);

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        powerups = GameObject.FindGameObjectsWithTag("powerup");

        if(isGameOver) {

            CancelInvoke();
            GameOverCanvas.gameObject.SetActive(true);

            if(score > highScore) {

                PlayerPrefs.SetInt("HighScore", score);
                highScoreTitleText.text = "NEW HIGH SCORE";
                highScoreText.text = score.ToString();
            } else {

                highScoreTitleText.text = "HIGH SCORE";
                highScoreText.text = highScore.ToString();
            }

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

        if(isGameStateDefending) { // Defend 

            enemy = spike;

            foreach (GameObject ballPlayerPrefab in ballPlayerPrefabs) {
                ballPlayerPrefab.SetActive(true);
            }
            foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
                spikePlayerPrefab.SetActive(false);
            }
        } else { // Attack

            enemy = ball;

            foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
                spikePlayerPrefab.SetActive(true);
            }
            foreach (GameObject ballPlayerPrefab in ballPlayerPrefabs) {
                ballPlayerPrefab.SetActive(false);
            }
        }

        if(gameTime >= 70.0f && !hasExecuted) { // Common for both Defend and Attack Mode | gm.score >= 25

            hasExecuted = true;
            CancelInvoke();
            interval = 1.5f;
            InvokeRepeating("instantiateEnemyAndPowerup", 0, interval);
        }
    }

    void showInstructions() {

        if (isGameStateDefending) {

            defenseModeTextGameObject.SetActive(true);
            StartCoroutine(deactivateGameObject(defenseModeTextGameObject));
        } else {

            attackModeTextGameObject.SetActive(true);
            StartCoroutine(deactivateGameObject(attackModeTextGameObject));
        }
    }

    public void changeGameState(string powerupName) {

        gameStateChangeSoundEffect.Play();

        if(powerupName.Contains("State")) {

            isGameStateDefending = !isGameStateDefending;
            showInstructions();

            foreach (GameObject enemy in enemies) {
                Destroy(enemy);
            }

            foreach (GameObject powerup in powerups) {
                Destroy(powerup);
            }
        } else {

            foreach (GameObject powerup in powerups) {
                Destroy(powerup);
            }

            StartCoroutine(speedUpAxleSpeed());
        }
    } 

    IEnumerator deactivateGameObject(GameObject obj) {

        yield return new WaitForSeconds(2.5f);
        obj.SetActive(false);
    }

    IEnumerator speedUpAxleSpeed() {

        axleScript.rotationSpeed = 500.0f;
        yield return new WaitForSeconds(2);
        axleScript.rotationSpeed = 180.0f;
    }

    public void playScoreIncSoundEffect() {
        scoreIncSoundEffect.Play();
    }

    public void playGameOverSoundEffect() {
        gameOverSoundEffect.Play();
    }
}