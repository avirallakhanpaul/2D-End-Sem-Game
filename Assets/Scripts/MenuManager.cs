using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public bool isMute;
    public Sprite volumeIcon;
    public Sprite muteIcon;
    public Image volumeButtonIcon;
    public Text score;
    public AudioSource clickSoundEffect;
    public bool isMainScene;
    public int volumeState;
    public int mute;
    public int unmute;
    GameManager gm;
    void Start() {

        mute = 0;
        unmute = 1;
        isMute = false;

        volumeState = PlayerPrefs.GetInt("volumeState", unmute);

        if(SceneManager.GetActiveScene().name == "Main") {

            isMainScene = true;
            gm = GameObject.FindObjectOfType<GameManager>();
        }

        clickSoundEffect = GetComponent<AudioSource>();
    }

    void Update() {

        // if (volumeState == 1) {
        //     Debug.Log("volume State = 1");
        //     volumeButtonIcon.sprite = volumeIcon;
        //     AudioListener.volume = 0.7f;
        // }
        // else if (volumeState == 0) {
        //     Debug.Log("volume State = 0");
        //     volumeButtonIcon.sprite = muteIcon;
        //     AudioListener.volume = 0;
        // }

        if(isMainScene) {
            score.text = gm.score.ToString();
        }

    }

    public void startGame() {
        clickSoundEffect.Play();
        SceneManager.LoadScene("Main");
    }

    public void quitGame() {

        clickSoundEffect.Play();
        Application.Quit();
    }

    public void toggleGameSound() {

        clickSoundEffect.Play();
        isMute = !isMute;

        if(isMute) {
            volumeButtonIcon.sprite = muteIcon;
            AudioListener.volume = 0;
        } else {
            volumeButtonIcon.sprite = volumeIcon;
            AudioListener.volume = 0.7f;
        }
 
        // if(isMute) {
        //     PlayerPrefs.SetInt("volumeState", mute);
        // } else {
        //     PlayerPrefs.SetInt("volumeState", unmute);
        // }
    }
}
