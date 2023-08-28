using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour{

    public static BuildManager instance;

    void Awake(){
        if (instance != null){
            Debug.LogError("BRO DET ER MER ENN EN BUILDMANAGER!!!!!!!!!!!!!!!!!!");
            return;
        }
        instance = this;
    }

    public GameObject laserShooterPrefab;
    public GameObject plasmaCanonPrefab;

    private GameObject towerToBuild;

    void Start(){
        towerToBuild = laserShooterPrefab;
    }

    public GameObject GetTowerToBuild(){
        return towerToBuild;
    }
}
