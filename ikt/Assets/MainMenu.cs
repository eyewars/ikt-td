using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour{
    public Image logo;
    public Button playButton;
    public Button quitButton;
    public Button infoButton;
    public Slider musicVolume;
    public Slider effectVolume;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI effectText;

    public Button backButton;
    public GameObject infoPageEmpty;

    public void playGame() {
        StatTracker.resetWave();
        SceneManager.LoadScene(1);
    }

    public void quitGame() {
        Debug.Log("Game is closing");
        Application.Quit();
    }

    public void infoPage(){
        logo.enabled = false;
        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        infoButton.gameObject.SetActive(false);
        musicVolume.gameObject.SetActive(false);
        effectVolume.gameObject.SetActive(false);
        musicText.enabled = false;
        effectText.enabled = false;

        backButton.gameObject.SetActive(true);
        infoPageEmpty.SetActive(true);
    }

    public void backToMain(){
        logo.enabled = true;
        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        infoButton.gameObject.SetActive(true);
        musicVolume.gameObject.SetActive(true);
        effectVolume.gameObject.SetActive(true);
        musicText.enabled = true;
        effectText.enabled = true;

        backButton.gameObject.SetActive(false);
        infoPageEmpty.SetActive(false);
    }
}