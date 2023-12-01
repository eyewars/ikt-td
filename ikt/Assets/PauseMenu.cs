using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    
    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                resume();
            }
            else{
                pause();
            }
        }
    }

    public void resume(){
        pauseMenuUI.SetActive(false);   
        Time.timeScale = 1f;
        isPaused = false;
    }

    void pause(){
        pauseMenuUI.SetActive(true);   
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void loadMenu(){
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void quitGame(){
        Debug.Log("nå sluttet spillet NWWEEEEIE");
        Application.Quit();
    }
}
