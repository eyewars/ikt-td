using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour{

    // DIRECTION TO MOVE
    public Vector3 myDir;
    Vector3 startPos;

    public Tower myTower;

    void Start(){
        startPos = transform.position;
    }

    void Update(){
        transform.Translate(myDir.normalized * myTower.getProjectileSpeed() * Time.deltaTime, Space.World);

        // MAKES A VECTOR FROM WHERE THE BULLET STARTED TO WHERE IT CURRENTLY IS, THEN GETS THE LENGTH OF THAT VECTOR
        float distanceFromStart = (transform.position - startPos).magnitude;
        if (distanceFromStart >= myTower.getRange()){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider thing){
        Destroy(gameObject);

        //Debug.Log(thing.gameObject);
        int enemyIndex = Spawner.enemies.IndexOf(thing.gameObject);
        
        Spawner.enemies[enemyIndex].GetComponent<Enemy>().health -= myTower.getDamage();

        if (Spawner.enemies[enemyIndex].GetComponent<Enemy>().health <= 0){
            Destroy(Spawner.enemies[enemyIndex]);
            Spawner.enemies.Remove(Spawner.enemies[enemyIndex]);
        }
    }
}
