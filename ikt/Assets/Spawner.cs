using System;
using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawner : MonoBehaviour{
    
    float timer = 0f;
    float maxTimer = 1f;
    float sendWaveTimer = 0f;
    float maxSendWaveTimer = 10f;
    int numberOfEnemies = 0;
    int numberOfEnemiesSent = 0;
    public GameObject enemy;

    bool hasGottenEndOfRoundTokens = false;

    public static List<GameObject> enemies = new List<GameObject>();

    List<List<List<string>>> enemyWaves = new List<List<List<string>>>{
        new List<List<string>>{
            new List<string> { "Normal", "2" },
            new List<string> { "Fast", "1" },
            new List<string> { "Slow", "1" }
        },
        new List<List<string>>{
            new List<string> { "Fast", "3" },
            new List<string> { "Slow", "1" }
        }
    };

    [SerializeField] private Image roundTimerImg;
    [SerializeField] private TextMeshProUGUI roundTimerText;

    void spawnEnemy(){
        enemies.Add((GameObject)Instantiate(enemy, this.transform));
        enemies[enemies.Count - 1].transform.position = new Vector3(0, 1, 1);
        enemies[enemies.Count - 1].GetComponent<Enemy>().health = StatTracker.instance.getWave();
        enemies[enemies.Count - 1].GetComponent<Enemy>().tokenIncrease = StatTracker.instance.getWave() * 2;

        numberOfEnemiesSent++;
    }

    void sendWave(int wave){
        numberOfEnemiesSent = 0;
        numberOfEnemies = wave * 2;
        timer = 1f;
        sendWaveTimer = 0f;
    }

    public void startWave(){
        hasGottenEndOfRoundTokens = false;

        int tokenAmount = (int)Math.Round((10 - sendWaveTimer) * 10);
        StatTracker.instance.changeTokens(tokenAmount);

        StatTracker.instance.increaseWave();
        sendWave(StatTracker.instance.getWave());
        StatTracker.instance.updateText();
    }

    void Start(){
        //enemyWaves = {{{"Normal", "2"}, {"Fast", "1"}, {"Fast", "1"}}, {{"Slow", "3"}, {"Fast", "1"}}};
    }

    void Update(){
        if (numberOfEnemiesSent < numberOfEnemies){
            if (timer >= maxTimer){
                spawnEnemy();
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
                    if (energyGeneratorsHit[i].GetComponent<Tower>().energyGeneratorUpgrade2){
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

        roundTimerImg.fillAmount = sendWaveTimer / maxSendWaveTimer;

        if (roundTimerImg.fillAmount == 0){
            roundTimerText.enabled = false;
        }
        else {
            roundTimerText.enabled = true;
        }
    }
}
