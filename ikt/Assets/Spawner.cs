using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{
    
    float timer = 0f;
    float maxTimer = 2f;
    public GameObject enemy;

    public static List<GameObject> enemies = new List<GameObject>();

    void spawnEnemy(){
        enemies.Add((GameObject)Instantiate(enemy, this.transform));
        enemies[enemies.Count - 1].transform.position = new Vector3(0, 1, 1); 
    }

    void Update(){
        if (timer >= maxTimer){
            spawnEnemy();
            timer -= maxTimer;
        }

        timer += 1 * Time.deltaTime;
        //Debug.Log(timer);
    }
}
