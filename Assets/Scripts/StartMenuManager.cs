using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

    public static bool isMute = false;
    public bool isPlayingScreenMute;
    public Image volumeButtonImage;
    public Sprite volumeIcon;
    public Sprite muteIcon;
    public Text score;
    public AudioSource clickSoundEffect;

    void Start() {
        clickSoundEffect = GetComponent<AudioSource>();
    }

    void Update() {
        
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

        if (isMute) {

            volumeButtonImage.sprite = muteIcon;
            AudioListener.volume = 0;
        } else {

            volumeButtonImage.sprite = volumeIcon;
            AudioListener.volume = 0.7f;
        }
    }
}
