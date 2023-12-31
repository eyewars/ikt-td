using System;
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

        Quaternion turnDirection = Quaternion.LookRotation(myDir);
        Vector3 turnRotation = turnDirection.eulerAngles;
            
        transform.rotation = Quaternion.Euler(90f, turnRotation.y, 0f);
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
        int enemyIndex = Spawner.enemies.IndexOf(thing.gameObject);

        if (enemyIndex >= 0){
            Destroy(gameObject);
            SFXMaster.instance.playImpactSFX();

            if (myTower.type == "Laser Shooter"){
                Spawner.enemies[enemyIndex].GetComponent<Enemy>().health -= myTower.dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>(), 0);

                if (myTower.laserShooterUpgrade4){
                    Spawner.enemies[enemyIndex].GetComponent<Enemy>().laserShooterUpgrade4StatusAdd();  
                }
                else if (myTower.laserShooterUpgrade3){
                    Spawner.enemies[enemyIndex].GetComponent<Enemy>().laserShooterUpgrade3StatusAdd();  
                }

                deleteEnemy(Spawner.enemies[enemyIndex]);
            }
            else if (myTower.type == "Plasma Canon"){
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, myTower.explosionRadius);
                List<Collider> enemiesHit = new List<Collider>();
                foreach (var hitCollider in hitColliders){
                    if (hitCollider.tag == "Enemy"){
                        enemiesHit.Add(hitCollider);
                    }
                }

                for (int i = 0; i < enemiesHit.Count; i++){
                    if (myTower.plasmaCanonUpgrade4){
                        enemiesHit[i].GetComponent<Enemy>().health -= myTower.dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>(), enemiesHit.Count);
                    }
                    else {
                        enemiesHit[i].GetComponent<Enemy>().health -= myTower.dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>(), 0);
                    }

                    deleteEnemy(enemiesHit[i].gameObject);
                }
            }
            else if (myTower.type == "Cryo Canon"){
                if (myTower.totalUpgrades >= 3){
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, myTower.explosionRadius);
                    List<Collider> enemiesHit = new List<Collider>();
                    foreach (var hitCollider in hitColliders){
                        if (hitCollider.tag == "Enemy"){
                            enemiesHit.Add(hitCollider);
                        }
                    }  

                    for (int i = 0; i < enemiesHit.Count; i++){
                        enemiesHit[i].GetComponent<Enemy>().health -= myTower.dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>(), 0);

                        if (myTower.cryoCanonUpgrade4){
                            enemiesHit[i].GetComponent<Enemy>().cryoCanonUpgrade4StatusAdd(); 
                        }
                        else{
                            enemiesHit[i].GetComponent<Enemy>().cryoCanonUpgrade2StatusAdd(); 
                        }
                        
                        deleteEnemy(enemiesHit[i].gameObject);
                    }
                }
                else if (myTower.cryoCanonUpgrade2){
                    Spawner.enemies[enemyIndex].GetComponent<Enemy>().health -= myTower.dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>(), 0);
                    Spawner.enemies[enemyIndex].GetComponent<Enemy>().cryoCanonUpgrade2StatusAdd(); 
                    deleteEnemy(Spawner.enemies[enemyIndex]);
                }
                else if (myTower.cryoCanonUpgrade0){
                    Spawner.enemies[enemyIndex].GetComponent<Enemy>().health -= myTower.dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>(), 0);
                    Spawner.enemies[enemyIndex].GetComponent<Enemy>().cryoCanonUpgrade0StatusAdd(); 
                    deleteEnemy(Spawner.enemies[enemyIndex]);
                }
            }
        }      
    }

    void deleteEnemy(GameObject theEnemy){
        if (theEnemy.GetComponent<Enemy>().health <= 0){
            SFXMaster.instance.playDeathSFX();
            StatTracker.instance.changeTokens(theEnemy.GetComponent<Enemy>().tokenIncrease, theEnemy.GetComponent<Enemy>().hackingUpgrade1Status);
            Destroy(theEnemy);
            Spawner.enemies.Remove(theEnemy);
            StatTracker.instance.updateText();
        }
    }
}
