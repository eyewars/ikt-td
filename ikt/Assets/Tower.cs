using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class Tower : MonoBehaviour{

    // TYPE OF BULLET TO SHOOT
    public GameObject bullet;

    // LIST OF BULLETS THAT THE TOWER SHOT
    public static List<GameObject> bullets = new List<GameObject>();

    public string enemyTag = "Enemy";

    public Transform enemyTarget = null;

    public float damage = 1f;

    public GameObject bulletHolder;
    void Start(){
        bulletHolder = GameObject.Find("Bullets");
    }
    
    // INSTANTIATES BULLETS AND ADDS TO LIST
    // FINDS THE DIRECTION FROM THE BULLET THAT WAS JUST MADE TO THE TARGET, AND ADDS THAT VALUE TO THE myDir VARIABLE
    void shoot(){
        bullets.Add((GameObject)Instantiate(bullet, bulletHolder.transform));
        bullets[bullets.Count - 1].transform.position = transform.position;
        
        bullets[bullets.Count - 1].GetComponent<Bullet>().myDir = enemyTarget.position - transform.position;

        bullets[bullets.Count - 1].GetComponent<Bullet>().myTower = gameObject;
    }

    void findEnemy(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        GameObject enemyNearestEnd = null;
        float nearestWaypoint = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; i++){
            Transform targetOfEnemy = enemies[i].GetComponent<Enemy>().target;
            int tempWaypointNum = System.Array.IndexOf(Waypoints.points, targetOfEnemy);

            if (enemyNearestEnd == null){
                enemyNearestEnd = enemies[i];
                nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
            }
            
            if ((tempWaypointNum >= System.Array.IndexOf(Waypoints.points, enemyNearestEnd.GetComponent<Enemy>().target)) && (enemies[i].GetComponent<Enemy>().distanceToWaypoint < nearestWaypoint)){
                enemyNearestEnd = enemies[i];
                nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
            }
        }

        if (enemyNearestEnd != null){
            enemyTarget = enemyNearestEnd.transform;
        }
    }

    void Update(){
        findEnemy();

        if (enemyTarget == null){
            return;
        }

        Vector3 direction = enemyTarget.position - transform.position;
        Quaternion turnDirection = Quaternion.LookRotation(direction);
        Vector3 turnRotation = turnDirection.eulerAngles;

        transform.rotation = Quaternion.Euler(0f, turnRotation.y, 0f);

        if (Input.GetKeyUp("space")){
            shoot();
        }

    }
}
