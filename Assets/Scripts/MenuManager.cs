using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public bool isMute = false;
    public Sprite volumeIcon;
    public Sprite muteIcon;
    public Sprite pauseIcon;
    public Sprite playIcon;
    public Image volumeIconOfStartAndOverMenu;
    public Image volumeIconOfPlayingCanvas;
    public Image pauseButtonImage;
    public Text score;
    public AudioSource clickSoundEffect;
    public bool isMainScene;
    public bool isGamePaused = false;
    GameManager gm;
    AdsManager adm;

    void Start() {
        gm = GameObject.FindObjectOfType<GameManager>();
        adm = GameObject.FindObjectOfType<AdsManager>();
        clickSoundEffect = GetComponent<AudioSource>();
    }

    void Update() {

        score.text = gm.score.ToString();

        if (isGamePaused) {
            pauseButtonImage.transform.localScale = new Vector2(2.0f, 2.0f);
            pauseButtonImage.sprite = playIcon;
            Time.timeScale = 0;
        } else {
            pauseButtonImage.transform.localScale = new Vector2(1.0f, 1.0f);
            pauseButtonImage.sprite = pauseIcon;
            Time.timeScale = 1;
        }

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
        adm.showInterstitialAd();
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

    public void togglePauseGame() {
        isGamePaused = !isGamePaused;
    }

    public void goToHome() {
        SceneManager.LoadScene("StartMenu");
    }
}
