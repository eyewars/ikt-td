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

    public static List<GameObject> enemies = new List<GameObject>();

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
        int tokenAmount = (int)Math.Round((10 - sendWaveTimer) * 10);
        StatTracker.instance.changeTokens(tokenAmount);

        StatTracker.instance.increaseWave();
        sendWave(StatTracker.instance.getWave());
        StatTracker.instance.updateText();
    }

    void Start(){
        
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
