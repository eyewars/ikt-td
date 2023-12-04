using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    public void playGame() {
        StatTracker.resetWave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame() {
        Debug.Log("Game is closing");
        Application.Quit();
    }
}