using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour{
    public void playAgain() {
        SceneManager.LoadScene(1);
    }

    public void backToMenu() {
        SceneManager.LoadScene(0);
    }

    public void quitGame() {
        Debug.Log("nå sluttet spillet NWWEEEEIE");
        Application.Quit();
    }
}