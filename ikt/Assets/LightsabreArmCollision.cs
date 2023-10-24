using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsabreArmCollision : MonoBehaviour{
    public Tower myTower;

    void OnTriggerEnter(Collider thing){
        if (thing.gameObject.tag == "Enemy"){
            myTower.lightsabreEnemies.Add(thing.gameObject);
        }  
    }

    void OnTriggerExit(Collider thing){
        if (thing.gameObject.tag == "Enemy"){
            myTower.lightsabreEnemies.Remove(thing.gameObject);
        } 
    }
}
