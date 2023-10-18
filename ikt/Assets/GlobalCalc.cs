using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCalc : MonoBehaviour{

    public static GlobalCalc instance;

    void Awake(){
        if (instance != null){
            Debug.LogError("BRO DET ER MER ENN EN STATRACKER!!!!!!!!!!!!!!!!!!");
            return;
        }
        instance = this;
    }

    public int getResistance(string towerDamageType, string enemyResistanceType){
        if (towerDamageType == enemyResistanceType){
            return 2;
        }
        else{
            return 1;
        }
    }
}
