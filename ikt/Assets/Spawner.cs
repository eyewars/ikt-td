using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{
    
    float timer = 0f;
    float maxTimer = 1f;
    int numberOfEnemies = 0;
    int numberOfEnemiesSent = 0;
    public GameObject enemy;

    public static List<GameObject> enemies = new List<GameObject>();

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
        timer = -2f;
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
            StatTracker.instance.increaseWave();
            sendWave(StatTracker.instance.getWave());
            StatTracker.instance.updateText();
        }
    }
}
