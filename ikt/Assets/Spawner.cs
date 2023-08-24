using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{
    
    float timer = 199f;
    float maxTimer = 200f;
    public GameObject enemy;

    void Start(){
        
    }

    void spawnEnemy(){
        Instantiate(enemy, new Vector3(0, 1, 1), Quaternion.identity);
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
