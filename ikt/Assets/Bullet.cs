using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour{

    // DIRECTION TO MOVE
    public Vector3 myDir;
    Vector3 startPos;

    public GameObject myTower;

    public float bulletSpeed = 20f;

    void Start(){
        startPos = transform.position;
    }

    void Update(){
        transform.Translate(myDir.normalized * bulletSpeed * Time.deltaTime, Space.World);

        // MAKES A VECTOR FROM WHERE THE BULLET STARTED TO WHERE IT CURRENTLY IS, THEN GETS THE LENGTH OF THAT VECTOR
        float distanceFromStart = (transform.position - startPos).magnitude;
        if (distanceFromStart >= 4){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider thing){
        Destroy(gameObject);

        //Debug.Log(thing.gameObject);
        int enemyIndex = Spawner.enemies.IndexOf(thing.gameObject);
        
        Spawner.enemies[enemyIndex].GetComponent<Enemy>().health -= myTower.GetComponent<Tower>().damage;

        if (Spawner.enemies[enemyIndex].GetComponent<Enemy>().health <= 0){
            Destroy(Spawner.enemies[enemyIndex]);
            Spawner.enemies.Remove(Spawner.enemies[enemyIndex]);
        }
    }
}
