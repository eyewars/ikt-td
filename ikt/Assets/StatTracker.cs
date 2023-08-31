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
    
    [SerializeField] private int tokens = 100;
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private int score = 0;
    [SerializeField] private int wave = 0;

    [SerializeField] TextMeshProUGUI tokenText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] float damageLS = 1f;
    [SerializeField] float rangeLS = 3.8f;
    [SerializeField] float attackSpeedLS = 0.8f;
    [SerializeField] float projectileSpeedLS = 20f;
    [SerializeField] int costLS = 100;

    [SerializeField] float damagePC = 2f;
    [SerializeField] float rangePC = 2.8f;
    [SerializeField] float attackSpeedPC = 1.5f;
    [SerializeField] float projectileSpeedPC = 12f;
    [SerializeField] int costPC = 150;


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

    public void changeTokens(int value){
        tokens += value;
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

    public float getDamage(int index){
        switch (index){
            case 0:
                return damageLS;
            case 1: 
                return damagePC;
            default: 
                return damageLS;
        }
    }

    public float getRange(int index){
        switch (index){
            case 0:
                return rangeLS;
            case 1: 
                return rangePC;
            default: 
                return rangeLS;
        }
    }

    public float getAttackSpeed(int index){
        switch (index){
            case 0:
                return attackSpeedLS;
            case 1: 
                return attackSpeedPC;
            default: 
                return attackSpeedLS;
        }
    }

    public float getProjectileSpeed(int index){
        switch (index){
            case 0:
                return projectileSpeedLS;
            case 1: 
                return projectileSpeedPC;
            default: 
                return projectileSpeedLS;
        }
    }

    public int getCost(int index){
        switch (index){
            case 0:
                return costLS;
            case 1: 
                return costPC;
            default: 
                return costLS;
        }
    }
}
