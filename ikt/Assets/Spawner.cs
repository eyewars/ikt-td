using System;
using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
//using System.Globalization;
using Random = UnityEngine.Random;
using System.Linq;

public class Spawner : MonoBehaviour{
    
    float timer = 0f;
    float maxTimer = 1f;
    float sendWaveTimer = 0f;
    float maxSendWaveTimer = 10f;
    int iLoop = 0;
    int jLoop = 0;
    bool waveIsOver = true;

    public GameObject normalRegular;
    public GameObject normalFast;
    public GameObject normalSlow;
    public GameObject normalBoss;
    public GameObject laserRegular;
    public GameObject laserFast;
    public GameObject laserSlow;
    public GameObject laserBoss;
    public GameObject plasmaRegular;
    public GameObject plasmaFast;
    public GameObject plasmaSlow;
    public GameObject plasmaBoss;

    private string[] enemyTypes = {
        "normalRegular", "normalFast", "normalSlow", 
        "laserRegular", "laserFast", "laserSlow",
        "plasmaRegular", "plasmaFast", "plasmaSlow",
    };

    private string[] enemyBossTypes = {"normalBoss", "laserBoss", "plasmaBoss"};

    bool hasGottenEndOfRoundTokens = false;

    public static List<GameObject> enemies = new List<GameObject>();

    List<List<List<string>>> enemyWaves2 = new List<List<List<string>>>{
        new List<List<string>>{
            new List<string> { "normalRegular", "2", "1.0" },
            new List<string> { "normalFast", "5", "0.5" },
            new List<string> { "normalSlow", "2", "2.0" },
        },
        new List<List<string>>{
            new List<string> { "plasmaFast", "3", "0.7" },
            new List<string> { "normalSlow", "1", "3" },
            new List<string> { "laserRegular", "4", "0.8" },
        },
        new List<List<string>>{
            new List<string> { "laserRegular", "3", "1" },
            new List<string> { "laserFast", "4", "1.2" },
            new List<string> { "laserRegular", "1", "1" },
            new List<string> { "laserSlow", "3", "1" },
            new List<string> { "laserFast", "2", "0.2" },
        },
        new List<List<string>>{
            new List<string> { "plasmaFast", "7", "0.3" },
            new List<string> { "plasmaSlow", "4", "1" },
            new List<string> { "plasmaRegular", "1", "2" },
            new List<string> { "plasmaFast", "3", "0.2" },
            new List<string> { "plasmaRegular", "2", "1" },
        },
        new List<List<string>>{
            new List<string> { "normalBoss", "1", "1" },
            new List<string> { "plasmaFast", "3", "0.5" },
            new List<string> { "laserRegular", "2", "0.8" },
            new List<string> { "normalFast", "5", "1.3" },
        },
    };

    List<List<List<string>>> enemyWaves = new List<List<List<string>>>{};

    [SerializeField] private Image roundTimerImg;
    [SerializeField] private TextMeshProUGUI roundTimerText;

    void spawnEnemy(GameObject enemy){
        enemies.Add((GameObject)Instantiate(enemy, this.transform));
        enemies[enemies.Count - 1].transform.position = new Vector3(0, 1, 1);
    }

    void sendWave(int wave){
        iLoop = 0;
        jLoop = 0;
        maxTimer = (float)Convert.ToDouble(enemyWaves[StatTracker.instance.getWave() - 1][iLoop][2]);
        timer = maxTimer;
        sendWaveTimer = 0f;
    }

    public void startWave(){
        hasGottenEndOfRoundTokens = false;
        waveIsOver = false;

        int tokenAmount = (int)Math.Round((10 - sendWaveTimer) * 10);
        StatTracker.instance.changeTokens(tokenAmount);

        StatTracker.instance.increaseWave();
        sendWave(StatTracker.instance.getWave());
        StatTracker.instance.updateText();
    }

    void Start(){
        for (int i = 0; i < 501; i++){
            //float tempPowerLevel = 5 * (((float)i / 2) + 1);
            //int powerLevel = (int)tempPowerLevel;
            int powerLevel = 5 + i;
            //Debug.Log(powerLevel);
            int maxPerEnemy = powerLevel;
            int numberOfEnemyTypes = Random.Range(1, (int)Math.Sqrt(powerLevel + 1) + 1);
            string[] enemyTypesToUse = new string[numberOfEnemyTypes];
            for (int enemyTypeIndex = 0; enemyTypeIndex < numberOfEnemyTypes; enemyTypeIndex++){
                enemyTypesToUse[enemyTypeIndex] = enemyTypes[Random.Range(0, enemyTypes.Length)];
                //Debug.Log(enemyTypesToUse[enemyTypeIndex]);
            }

            void getRandomEnemies(int max, int total, int len){
                List<List<string>> tempList = new List<List<string>>{};
                List<float> tempNumbers = new List<float>{};
                float sum = 0f;
                do{
                    tempNumbers.Clear();
                    for (int j = 0; j < numberOfEnemyTypes; j++){
                        tempNumbers.Add(Random.Range(0.0f, 1.0f));
                    }

                    sum = tempNumbers.Sum();
                    float scale = (total - len) / sum;
                    for (int j = 0; j < tempNumbers.Count; j++){
                        tempNumbers[j] = (float)Math.Round(tempNumbers[j] * scale) + 1;
                    }
                    sum = tempNumbers.Sum();
                }
                while(sum != total);

                if ((i + 1) % 5 == 0){
                    if (i == 4){
                        List<String> tempEnemy = new List<string>{"normalBoss", "1", "1"};
                        tempList.Add(tempEnemy); 
                    }
                    else{
                        string randomBoss = enemyBossTypes[Random.Range(0, enemyBossTypes.Length)];
                        List<String> tempEnemy = new List<string>{randomBoss, "1", "1"};
                        tempList.Add(tempEnemy);   
                    }
                    
                }
                
                for (int j = 0; j < len; j++){
                    List<String> tempEnemy = new List<string>{enemyTypesToUse[j], tempNumbers[j].ToString(), Random.Range(0.2f, 2.3f).ToString()};
                    tempList.Add(tempEnemy);
                }

                enemyWaves.Add(tempList);
            }
            getRandomEnemies(maxPerEnemy, powerLevel, numberOfEnemyTypes);
        }
    }

    void Update(){
        if (StatTracker.instance.getWave() <= enemyWaves.Count){
            if (!waveIsOver){
                maxTimer = (float)Convert.ToDouble(enemyWaves[StatTracker.instance.getWave() - 1][iLoop][2]);
                if (timer >= maxTimer){
                    bool hasToEnd = false;
                    for (int i = iLoop; i < enemyWaves[StatTracker.instance.getWave() - 1].Count; i++){
                        if (!hasToEnd){
                            for (int j = jLoop; j < int.Parse(enemyWaves[StatTracker.instance.getWave() - 1][i][1]); j++){
                                jLoop++;
                                string enemyType = enemyWaves[StatTracker.instance.getWave() - 1][i][0];

                                if (jLoop == int.Parse(enemyWaves[StatTracker.instance.getWave() - 1][i][1])){
                                    iLoop++;
                                    jLoop = 0;

                                    if (iLoop == enemyWaves[StatTracker.instance.getWave() - 1].Count){
                                        waveIsOver = true;
                                    }
                                }

                                hasToEnd = true;
                                if (enemyType == "normalRegular"){
                                    spawnEnemy(normalRegular);
                                    break;
                                }
                                else if (enemyType == "normalFast"){
                                    spawnEnemy(normalFast);
                                    break;
                                }
                                else if (enemyType == "normalSlow"){
                                    spawnEnemy(normalSlow);
                                    break;
                                }
                                else if (enemyType == "normalBoss"){
                                    spawnEnemy(normalBoss);
                                    break;
                                }
                                else if (enemyType == "laserRegular"){
                                    spawnEnemy(laserRegular);
                                    break;
                                }
                                else if (enemyType == "laserFast"){
                                    spawnEnemy(laserFast);
                                    break;
                                }
                                else if (enemyType == "laserSlow"){
                                    spawnEnemy(laserSlow);
                                    break;
                                }
                                else if (enemyType == "laserBoss"){
                                    spawnEnemy(laserBoss);
                                    break;
                                }
                                else if (enemyType == "plasmaRegular"){
                                    spawnEnemy(plasmaRegular);
                                    break;
                                }
                                else if (enemyType == "plasmaFast"){
                                    spawnEnemy(plasmaFast);
                                    break;
                                }
                                else if (enemyType == "plasmaSlow"){
                                    spawnEnemy(plasmaSlow);
                                    break;
                                }
                                else if (enemyType == "plasmaBoss"){
                                    spawnEnemy(plasmaBoss);
                                    break;
                                }
                            }  
                        }  
                    }
                    timer -= maxTimer;
                }
                timer += 1 * Time.deltaTime;
            }
            else{
                if ((!hasGottenEndOfRoundTokens) && (StatTracker.instance.getWave() > 0)){
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 30);
                    List<Collider> energyGeneratorsHit = new List<Collider>();
                    foreach (var hitCollider in hitColliders){
                        if (hitCollider.tag == "EnergyGenerator"){
                            energyGeneratorsHit.Add(hitCollider);
                        }
                    }

                    for (int i = 0; i < energyGeneratorsHit.Count; i++){
                        if (energyGeneratorsHit[i].GetComponent<Tower>().energyGeneratorUpgrade1){
                            StatTracker.instance.changeTokens(30);
                        }
                    }

                    // Du får litt penger på slutten av runden uansett
                    StatTracker.instance.changeTokens(50);

                    StatTracker.instance.updateText();

                    hasGottenEndOfRoundTokens = true;
                }

                if (sendWaveTimer < maxSendWaveTimer){
                    sendWaveTimer += 1 * Time.deltaTime;
                }
                else{
                    startWave();
                }
            }
        }
        
        roundTimerImg.fillAmount = sendWaveTimer / maxSendWaveTimer;

        if (roundTimerImg.fillAmount == 0){
            roundTimerText.enabled = false;
        }
        else {
            roundTimerText.enabled = true;
        }
    }
}
