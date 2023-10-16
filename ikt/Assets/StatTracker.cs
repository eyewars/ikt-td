using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] float explosionRadiusLS = 0f;
    [SerializeField] int costLS = 100;
    [SerializeField] int[] upgradeCostsLS = {100, 200, 300, 400};
    [SerializeField] string[] upgradeDescriptionsLS = {"Increase damage per shot.", "Increase attack range.", "Apply a stacking damage over time effect to the target. The effect does not refresh on attack.", "Significantly increase attack speed, and the damage over time effects ticks much faster.", "Fully upgraded."};

    [SerializeField] float damagePC = 2f;
    [SerializeField] float rangePC = 2.8f;
    [SerializeField] float attackSpeedPC = 1.5f;
    [SerializeField] float projectileSpeedPC = 12f;
    [SerializeField] float explosionRadiusPC = 1.8f;
    [SerializeField] int costPC = 150;
    [SerializeField] int[] upgradeCostsPC = {150, 250, 350, 450};
    [SerializeField] string[] upgradeDescriptionsPC = {"Increase attack range.", "Explosions radius is bigger.", "Deploy landmines that explode when stepped on.", "The explosion deals more damage the more enemies it hits.", "Fully upgraded."};

    [SerializeField] float damageCC = 1f;
    [SerializeField] float rangeCC = 3.5f;
    [SerializeField] float attackSpeedCC = 1.6f;
    [SerializeField] float projectileSpeedCC = 14f;
    [SerializeField] float explosionRadiusCC = 0f;
    [SerializeField] int costCC = 125;
    [SerializeField] int[] upgradeCostsCC = {125, 225, 325, 425};
    [SerializeField] string[] upgradeDescriptionsCC = {"Increase attack speed.", "Slows even more.", "Nearby enemies also get slowed.", "Freezes enemies for a short duration, and slows them after they thaw out.", "Fully upgraded."};

    [SerializeField] float damageEG = 0f;
    [SerializeField] float rangeEG = 0f;
    [SerializeField] float attackSpeedEG = 2f;
    [SerializeField] float projectileSpeedEG = 0f;
    [SerializeField] float explosionRadiusEG = 0f;
    [SerializeField] int costEG = 200;
    [SerializeField] int[] upgradeCostsEG = {200, 300, 400, 500};
    [SerializeField] string[] upgradeDescriptionsEG = {"Generates more energy.", "Generate a large amount of energy at the end of the round.", "Generates energy faster.", "Generates more energy based on how much energy you already have.", "Fully upgraded."};

    void Start(){
        updateText();
    }

    public void updateText(){
        tokenText.text = tokens.ToString();
        healthText.text = playerHealth.ToString();
        waveText.text = "Round: " + wave.ToString();
    }

    public void takeDamage(int dmg){
        playerHealth -= dmg;

        if (playerHealth <= 0) {
            SceneManager.LoadScene(2);
        }
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
            case 2: 
                return damageCC;
            case 3: 
                return damageEG;
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
            case 2: 
                return rangeCC;
            case 3: 
                return rangeEG;
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
            case 2: 
                return attackSpeedCC;
            case 3: 
                return attackSpeedEG;
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
            case 2: 
                return projectileSpeedCC;
            case 3: 
                return projectileSpeedEG;
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
            case 2: 
                return costCC;
            case 3: 
                return costEG;
            default: 
                return costLS;
        }
    }

    public string getDescription(int index){
        switch (index){
            case 0:
                return "Basic tower that shoots laser.";
            case 1: 
                return "Slow tower that shoots plasma.";
            case 2: 
                return "Slows enemies with freezing attacks.";
            case 3: 
                return "Generates energy.";
            default: 
                return "DEFAULT, DU ADDA IKKE INDEX";
        }
    }

    public int[] getUpgradeCost(int index){
        switch (index){
            case 0:
                return upgradeCostsLS;
            case 1: 
                return upgradeCostsPC;
            case 2: 
                return upgradeCostsCC;
            case 3: 
                return upgradeCostsEG;
            default: 
                return upgradeCostsLS;
        }
    }

    public string[] getUpgradeDescription(int index){
        switch (index){
            case 0:
                return upgradeDescriptionsLS;
            case 1: 
                return upgradeDescriptionsPC;
            case 2: 
                return upgradeDescriptionsCC;
            case 3: 
                return upgradeDescriptionsEG;
            default: 
                return upgradeDescriptionsLS;
        }
    }

    public float getExplosionRadius(int index){
        switch (index){
            case 0:
                return explosionRadiusLS;
            case 1: 
                return explosionRadiusPC;
            case 2: 
                return explosionRadiusCC;
            case 3: 
                return explosionRadiusEG;
            default: 
                return explosionRadiusLS;
        }
    }
}
