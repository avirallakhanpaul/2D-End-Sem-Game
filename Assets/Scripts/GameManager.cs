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
    private GameObject spikeEnemy;
    public float interval;
    public bool isGameOver;
    GameObject[] spikePlayerPrefabs;
    GameObject[] ballPlayerPrefabs;
    GameObject[] enemies;
    public bool isMute;

    void Start() {

        ballPlayerPrefabs = GameObject.FindGameObjectsWithTag("ball-player");
        spikePlayerPrefabs = GameObject.FindGameObjectsWithTag("spike-player");

        foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
            spikePlayerPrefab.SetActive(false);
        }

        isGameStateDefending = true;

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        InvokeRepeating("InstantiateEnemy", 1, interval);

        isMute = false;
    }

    void InstantiateEnemy() {

        if(isGameStateDefending) {

            foreach (GameObject ballPlayerPrefab in ballPlayerPrefabs) {
                ballPlayerPrefab.SetActive(true);
            }
            foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
                spikePlayerPrefab.SetActive(false);
            }

            spikeEnemy = spike;

        } else if(!isGameStateDefending) {

            foreach (GameObject spikePlayerPrefab in spikePlayerPrefabs) {
                spikePlayerPrefab.SetActive(true);
            }
            foreach (GameObject ballPlayerPrefab in ballPlayerPrefabs) {
                ballPlayerPrefab.SetActive(false);
            }

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

        enemies = GameObject.FindGameObjectsWithTag("enemy");

        if(isGameOver) {
            CancelInvoke();
            return;
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            isGameStateDefending = !isGameStateDefending;
            
            foreach (GameObject enemy in enemies) {
                Destroy(enemy);
            }
        }

        if(score == 0) {
            scoreText.text = "";
        } else {
            scoreText.text = score.ToString();
        }
    }

    public void startGame() {
        SceneManager.LoadScene("Main");
    }

    public void quitGame() {
        Application.Quit();
    }

    public void toggleGameSound() {
        
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }
}
