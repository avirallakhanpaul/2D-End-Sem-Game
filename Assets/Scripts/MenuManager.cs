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
    GameManager gm;
    void Start() {

        if(SceneManager.GetActiveScene().name == "Main") {

            isMainScene = true;
            gm = GameObject.FindObjectOfType<GameManager>();
        }

        clickSoundEffect = GetComponent<AudioSource>();
        isMute = false;
    }

    void Update() {

        if (!isMute) {
            volumeButtonIcon.sprite = volumeIcon;
        }
        else if (isMute) {
            volumeButtonIcon.sprite = muteIcon;
        }

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
        AudioListener.volume = isMute ? 0 : 0.7f;
    }
}
