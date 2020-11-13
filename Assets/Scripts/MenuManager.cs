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
    void Start() {
        isMute = false;
    }

    void Update() {

        if (!isMute) {
            volumeButtonIcon.sprite = volumeIcon;
        }
        else if (isMute) {
            volumeButtonIcon.sprite = muteIcon;
        }
    }

    public void startGame() {
        SceneManager.LoadScene("Main");
    }

    public void quitGame() {

        Debug.Log("App Quit");
        Application.Quit();
    }

    public void toggleGameSound() {

        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }
}
