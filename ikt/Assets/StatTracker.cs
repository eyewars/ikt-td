using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatTracker : MonoBehaviour{

    public static StatTracker instance;

    void Awake(){
        if (instance != null){
            Debug.LogError("BRO DET ER MER ENN EN STATRACKER!!!!!!!!!!!!!!!!!!");
            return;
        }
        instance = this;
    }
    
    public int tokens = 100;
    public int playerHealth = 100;
    public int score = 0;
    public int wave = 0;

    [SerializeField] TextMeshProUGUI tokenText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI waveText;


    void Start(){
        updateText();
    }

    public void updateText(){
        tokenText.text = "Tokens: " + tokens.ToString();
        healthText.text = "Health: " + playerHealth.ToString();
        waveText.text = "Round: " + wave.ToString();
    }

    public void takeDamage(int dmg){
        playerHealth -= dmg;
    }

    public void increaseWave(){
        wave++;
    }

    public int getTokens(){
        return tokens;
    }

    public int getHealth(){
        return playerHealth;
    }

    public int getScore(){
        return score;
    }

    public int getWave(){
        return wave;
    }
}
