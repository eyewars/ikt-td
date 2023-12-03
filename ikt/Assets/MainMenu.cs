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
        Debug.Log("nå sluttet spillet NWWEEEEIE");
        Application.Quit();
    }
}