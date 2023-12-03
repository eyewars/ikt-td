using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

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
    public static float score = 0;
    [SerializeField] private static int wave = 0;

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

    [SerializeField] float damageLSA = 0.5f;
    [SerializeField] float rangeLSA = 0f;
    [SerializeField] float attackSpeedLSA = 0.2f;
    [SerializeField] float projectileSpeedLSA = 0f;
    [SerializeField] float explosionRadiusLSA = 0f;
    [SerializeField] int costLSA = 225;
    [SerializeField] int[] upgradeCostsLSA = {200, 400, 600, 800};
    [SerializeField] string[] upgradeDescriptionsLSA = {"Increase range.", "Increase damage per tick.", "Deal more damage and more damage over time.", "Deal even more damage to enemies affected by crowd control.", "Fully upgraded."};

    [SerializeField] float damagePT = 0.5f;
    [SerializeField] float rangePT = 4.2f;
    [SerializeField] float attackSpeedPT = 0.1f;
    [SerializeField] float projectileSpeedPT = 50f;
    [SerializeField] float explosionRadiusPT = 0f;
    [SerializeField] int costPT = 320;
    [SerializeField] int[] upgradeCostsPT = {350, 450, 750, 950};
    [SerializeField] string[] upgradeDescriptionsPT = {"Time between attacking enemies is shorter.", "Increase damage.", "Deals more damage the longer it attacks.", "Starts at max bonus.", "Fully upgraded."};

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

    [SerializeField] float damageB = 0.3f;
    [SerializeField] float rangeB = 2f;
    [SerializeField] float attackSpeedB = 2.5f;
    [SerializeField] float projectileSpeedB = 0f;
    [SerializeField] float explosionRadiusB = 0f;
    [SerializeField] int costB = 250;
    [SerializeField] int[] upgradeCostsB = {210, 420, 600, 975};
    [SerializeField] string[] upgradeDescriptionsB = {"Increase attack range.", "Increase attack speed.", "Stun lasts longer.", "After the stun is over, a delayed stun is applied that activates shortly after.", "Fully upgraded."};

    [SerializeField] float damageEG = 0f;
    [SerializeField] float rangeEG = 0f;
    [SerializeField] float attackSpeedEG = 2f;
    [SerializeField] float projectileSpeedEG = 0f;
    [SerializeField] float explosionRadiusEG = 0f;
    [SerializeField] int costEG = 200;
    [SerializeField] int[] upgradeCostsEG = {200, 300, 400, 500};
    [SerializeField] string[] upgradeDescriptionsEG = {"Generates more energy.", "Generate a large amount of energy at the end of the round.", "Generates energy faster.", "Generates more energy based on how much energy you already have.", "Fully upgraded."};

    [SerializeField] float damageHT = 0f;
    [SerializeField] float rangeHT = 2.2f;
    [SerializeField] float attackSpeedHT = 1.8f;
    [SerializeField] float projectileSpeedHT = 0f;
    [SerializeField] float explosionRadiusHT = 0f;
    [SerializeField] int costHT = 250;
    [SerializeField] int[] upgradeCostsHT = {250, 450, 600, 900};
    [SerializeField] string[] upgradeDescriptionsHT = {"If an enemy is killed while hacked, they will generate more energy.", "There is a low chance when hacked for enemies to move backwards.", "Hacked enemies do not have any resistances.", "All hacking effects are stronger, and duration is longer.", "Fully upgraded."};

    [SerializeField] List<string> normalTargeting = new List<string>(){"First", "Strong", "Close", "Last"};
    [SerializeField] List<string> armTargeting = new List<string>(){"Up", "Right", "Down", "Left"};
    [SerializeField] List<string> noTargeting = new List<string>(){"None"};

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

    public void changeTokens(int value, float hackingUpgrade1){
        float tokenMultiplier = 1;

        if (hackingUpgrade1 > 0) {
            tokenMultiplier = 1.5f;
        }

        tokens = tokens + (int)(value * tokenMultiplier);
    }

    public int getTokens(){
        return tokens;
    }

    public int getHealth(){
        return playerHealth;
    }

    public float getScore(){
        return score;
    }

    public int getWave(){
        return wave;
    }

    public static void resetWave(){
        wave = 0;
    }

    public static void updateScore(){
        score = 1 * (float)Math.Pow(1.1, wave);
    }

    public float getDamage(int index){
        switch (index){
            case 0:
                return damageLS;
            case 1:
                return damageLSA;
            case 2: 
                return damagePT;
            case 3: 
                return damagePC;
            case 4: 
                return damageCC;
            case 5: 
                return damageB;
            case 6: 
                return damageEG;
            case 7: 
                return damageHT;
            default: 
                return damageLS;
        }
    }

    public float getRange(int index){
        switch (index){
            case 0:
                return rangeLS;
            case 1:
                return rangeLSA;
            case 2: 
                return rangePT;
            case 3: 
                return rangePC;
            case 4: 
                return rangeCC;
            case 5: 
                return rangeB;
            case 6: 
                return rangeEG;
            case 7: 
                return rangeHT;
            default: 
                return rangeLS;
        }
    }

    public float getAttackSpeed(int index){
        switch (index){
            case 0:
                return attackSpeedLS;
            case 1:
                return attackSpeedLSA;
            case 2: 
                return attackSpeedPT;
            case 3: 
                return attackSpeedPC;
            case 4: 
                return attackSpeedCC;
            case 5: 
                return attackSpeedB;
            case 6: 
                return attackSpeedEG;
            case 7: 
                return attackSpeedHT;
            default: 
                return attackSpeedLS;
        }
    }

    public float getProjectileSpeed(int index){
        switch (index){
            case 0:
                return projectileSpeedLS;
            case 1:
                return projectileSpeedLSA;
            case 2: 
                return projectileSpeedPT;
            case 3: 
                return projectileSpeedPC;
            case 4: 
                return projectileSpeedCC;
            case 5: 
                return projectileSpeedB;
            case 6: 
                return projectileSpeedEG;
            case 7: 
                return projectileSpeedHT;
            default: 
                return projectileSpeedLS;
        }
    }

    public int getCost(int index){
        switch (index){
            case 0:
                return costLS;
            case 1:
                return costLSA;
            case 2: 
                return costPT;
            case 3: 
                return costPC;
            case 4: 
                return costCC;
            case 5: 
                return costB;
            case 6: 
                return costEG;
            case 7: 
                return costHT;
            default: 
                return costLS;
        }
    }

    public string getDescription(int index){
        switch (index){
            case 0:
                return "Fires lasers at a single enemy quickly.";
            case 1:
                return "Has a static lightsabre arm that damages all enemies that pass through.";
            case 2: 
                return "Shoots a constant beam of plasma at the target. After the target dies, the tower will need to recharge for a short duration before it can attack again.";
            case 3: 
                return "Slowly shoots exploding plasma blasts that damages multiple enemies at once.";
            case 4: 
                return "Slows enemies with freezing attacks.";
            case 5: 
                return "Stuns all enemies in range for a short duration.";
            case 6: 
                return "Generates energy.";
            case 7: 
                return "Hackes nearby enemies, making them more vulnerable to damage.";
            default: 
                return "DEFAULT, DU ADDA IKKE INDEX";
        }
    }

    public int[] getUpgradeCost(int index){
        switch (index){
            case 0:
                return upgradeCostsLS;
            case 1:
                return upgradeCostsLSA;
            case 2: 
                return upgradeCostsPT;
            case 3: 
                return upgradeCostsPC;
            case 4: 
                return upgradeCostsCC;
            case 5: 
                return upgradeCostsB;
            case 6: 
                return upgradeCostsEG;
            case 7: 
                return upgradeCostsHT;
            default: 
                return upgradeCostsLS;
        }
    }

    public string[] getUpgradeDescription(int index){
        switch (index){
            case 0:
                return upgradeDescriptionsLS;
            case 1:
                return upgradeDescriptionsLSA;
            case 2: 
                return upgradeDescriptionsPT;
            case 3: 
                return upgradeDescriptionsPC;
            case 4: 
                return upgradeDescriptionsCC;
            case 5: 
                return upgradeDescriptionsB;
            case 6: 
                return upgradeDescriptionsEG;
            case 7: 
                return upgradeDescriptionsHT;
            default: 
                return upgradeDescriptionsLS;
        }
    }

    public float getExplosionRadius(int index){
        switch (index){
            case 0:
                return explosionRadiusLS;
            case 1:
                return explosionRadiusLSA;
            case 2: 
                return explosionRadiusPT;
            case 3: 
                return explosionRadiusPC;
            case 4: 
                return explosionRadiusCC;
            case 5: 
                return explosionRadiusB;
            case 6: 
                return explosionRadiusEG;
            case 7: 
                return explosionRadiusHT;
            default: 
                return explosionRadiusLS;
        }
    }

    public List<string> getTargetingOptions(int index){
        switch (index){
            case 0:
                return normalTargeting;
            case 1:
                return armTargeting;
            case 2: 
                return normalTargeting;
            case 3: 
                return normalTargeting;
            case 4: 
                return normalTargeting;
            case 5: 
                return noTargeting;
            case 6: 
                return noTargeting;
            case 7: 
                return noTargeting;
            default: 
                return normalTargeting;
        }
    }
}
