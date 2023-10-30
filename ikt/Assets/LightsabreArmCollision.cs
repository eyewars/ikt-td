using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsabreArmCollision : MonoBehaviour{
    public GameObject myTower;

    void OnTriggerEnter(Collider thing){
        if (thing.gameObject.tag == "Enemy"){
            myTower.GetComponent<Tower>().lightsabreEnemies.Add(thing.gameObject);
            thing.gameObject.GetComponent<Enemy>().lightsabreArmUpgrade3StatusAdd(myTower);
        }  
    }

    void OnTriggerExit(Collider thing){
        if (thing.gameObject.tag == "Enemy"){
            myTower.GetComponent<Tower>().lightsabreEnemies.Remove(thing.gameObject);
        } 
    }
}
