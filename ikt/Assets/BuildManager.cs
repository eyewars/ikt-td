  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject plasmaTowerPrefab;
    public GameObject beaconPrefab;
    public GameObject supportTowerPrefab;

    private GameObject towerToBuild;

    public GameObject upgradePanel;

    public Sprite tempImage;

    public string selectedTower = "";

    public void setTowerToBuild(int index){
        resetTowerToBuild();
        switch (index){
            case 0:
                towerToBuild = laserShooterPrefab;
                break;
            case 1:
                towerToBuild = lightSabreArmPrefab;
                break;
            case 2:
                towerToBuild = plasmaTowerPrefab;
                break;
            case 3:
                towerToBuild = plasmaCanonPrefab;
                break;
            case 4:
                towerToBuild = cryoCanonPrefab;
                break;
            case 5:
                towerToBuild = beaconPrefab;
                break;
            case 6:
                towerToBuild = energyGeneratorPrefab;
                break;
            case 7:
                towerToBuild = supportTowerPrefab;
                break;
        }
    }

    public GameObject GetTowerToBuild(){
        return towerToBuild;
    }

    public void resetTowerToBuild(){
        towerToBuild = null;

        if (selectedTower == "LaserShooter"){
            GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/LaserShooter").GetComponent<Image>().sprite = tempImage;
        }
        else if (selectedTower == "LightSabreArm"){
            GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/LightSabreArm").GetComponent<Image>().sprite = tempImage;
        }
        else if (selectedTower == "PlasmaTower"){
            GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/PlasmaTower").GetComponent<Image>().sprite = tempImage;
        }
        else if (selectedTower == "PlasmaCanon"){
            GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/PlasmaCanon").GetComponent<Image>().sprite = tempImage;
        }
        else if (selectedTower == "CryoCanon"){
            GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/CryoCanon").GetComponent<Image>().sprite = tempImage;
        }
        else if (selectedTower == "Beacon"){
            GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/Beacon").GetComponent<Image>().sprite = tempImage;
        }
        else if (selectedTower == "EnergyGenerator"){
            GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/EnergyGenerator").GetComponent<Image>().sprite = tempImage;
        }
        else if (selectedTower == "SupportTower"){
            GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/SupportTower").GetComponent<Image>().sprite = tempImage;
        }

        selectedTower = "";
    }

    public Tile currentTileHoldingTowerUI;

    public void setSelectedTower(string type, Sprite img){
        if (selectedTower == ""){
            selectedTower = type;
            tempImage = img;
        }
    }
}
