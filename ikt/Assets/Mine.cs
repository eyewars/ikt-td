using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour{
    public Tower myTower;

    void Start(){
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void OnTriggerEnter(Collider thing){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);
        List<Collider> enemiesHit = new List<Collider>();
        foreach (var hitCollider in hitColliders){
            if (hitCollider.tag == "Enemy"){
                enemiesHit.Add(hitCollider);
            }
        }

        for (int i = 0; i < enemiesHit.Count; i++){
            if (myTower.plasmaCanonUpgrade4){
                    enemiesHit[i].GetComponent<Enemy>().health -= myTower.getDamage() + enemiesHit.Count / 2;
            }
            else {
                enemiesHit[i].GetComponent<Enemy>().health -= myTower.getDamage();
            }

            deleteEnemy(enemiesHit[i].gameObject);
        }

        Collider[] hitColliderGround = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders){
            if (hitCollider.tag == "Ground"){
                hitCollider.GetComponent<Ground>().hasMine = false;
            }
        }

        Destroy(gameObject);
    }

    void deleteEnemy(GameObject theEnemy){
        if (theEnemy.GetComponent<Enemy>().health <= 0){
            StatTracker.instance.changeTokens(theEnemy.GetComponent<Enemy>().tokenIncrease);
            Destroy(theEnemy);
            Spawner.enemies.Remove(theEnemy);
            StatTracker.instance.updateText(); 
        }
    }
}
