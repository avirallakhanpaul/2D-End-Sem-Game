using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public bool isMute = false;
    public Sprite volumeIcon;
    public Sprite muteIcon;
    public Image volumeIconOfStartAndOverMenu;
    public Image volumeIconOfPlayingCanvas;
    public Text score;
    public AudioSource clickSoundEffect;
    public bool isMainScene;
    GameManager gm;

    void Start() {

        gm = GameObject.FindObjectOfType<GameManager>();
        clickSoundEffect = GetComponent<AudioSource>();
    }

    void Update() {

        score.text = gm.score.ToString();

        if (StartMenuManager.isMute || AudioListener.volume == 0) {
            volumeIconOfStartAndOverMenu.sprite = muteIcon;
            volumeIconOfPlayingCanvas.sprite = muteIcon;
        } else {
            volumeIconOfStartAndOverMenu.sprite = volumeIcon;
            volumeIconOfPlayingCanvas.sprite = volumeIcon;
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

            volumeIconOfStartAndOverMenu.sprite = muteIcon;
            volumeIconOfPlayingCanvas.sprite = muteIcon;
            AudioListener.volume = 0;
        } else {

            volumeIconOfStartAndOverMenu.sprite = volumeIcon;
            volumeIconOfPlayingCanvas.sprite = volumeIcon;
            AudioListener.volume = 0.5f;
        }
    }
}
