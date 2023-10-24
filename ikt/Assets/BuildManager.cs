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
    public GameObject cryoCanonPrefab;
    public GameObject energyGeneratorPrefab;
    public GameObject lightSabreArmPrefab;

    private GameObject towerToBuild;

    public GameObject upgradePanel;

    public void setTowerToBuild(int index){
        switch (index){
            case 0:
                towerToBuild = laserShooterPrefab;
                break;
            case 1:
                towerToBuild = lightSabreArmPrefab;
                break;
            // Putt single target plasma før aoe plasma AKA her
            case 2:
                towerToBuild = plasmaCanonPrefab;
                break;
            case 3:
                towerToBuild = cryoCanonPrefab;
                break;
            case 4:
                towerToBuild = energyGeneratorPrefab;
                break;
        }
    }

    void Start(){
        towerToBuild = laserShooterPrefab;
    }

    public GameObject GetTowerToBuild(){
        return towerToBuild;
    }

    public Tile currentTileHoldingTowerUI;
}
