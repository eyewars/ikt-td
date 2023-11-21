using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour{

    public Color hoverColor;
    private Color startColor;

    public GameObject tower;

    private GameObject realShowTile;
    [SerializeField] private GameObject showTile;

    [SerializeField] private GameObject laserShooter;
    [SerializeField] private GameObject plasmaCanon;
    [SerializeField] private GameObject cryoCanon;
    [SerializeField] private GameObject energyGenerator;
    [SerializeField] private GameObject lightSabreArm;
    [SerializeField] private GameObject plasmaTower;
    [SerializeField] private GameObject beacon;
    [SerializeField] private GameObject supportTower;

    private Image tempUIImage;
    [SerializeField] private Sprite white;

    private Renderer rend;

    void Start(){
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void Update(){
        if(Input.GetMouseButtonDown(1)){
            BuildManager.instance.resetTowerToBuild();
            Destroy(realShowTile);
            line.enabled = false;
        }
    }

    void buildTower(){
        if (tower != null){
            Debug.Log("BROR DET ER NOE DER LOL");
            return;
        } 
        
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
        BuildManager.instance.resetTowerToBuild();
        if (towerToBuild == null) return;
        Destroy(realShowTile);
        Vector3 tempPos = transform.position;
        tempPos.y += 0.6f;
        tower = (GameObject)Instantiate(towerToBuild, tempPos, transform.rotation);
        tower.GetComponent<Tower>().myTile = this;

        if (StatTracker.instance.getTokens() < tower.GetComponent<Tower>().getCost()){
            Destroy(tower);
            return;
        }   

        StatTracker.instance.changeTokens(-tower.GetComponent<Tower>().getCost(), 0);
    
        tower.GetComponent<Tower>().totalMoneySpent += tower.GetComponent<Tower>().getCost();
        tower.GetComponent<Tower>().updateSellValue();

        StatTracker.instance.updateText();
    }

    public void sellTower(){
        int value = tower.GetComponent<Tower>().sellValue;
        StatTracker.instance.changeTokens(value, 0);
        StatTracker.instance.updateText();

        Destroy(tower);
    }

    void OnMouseDown(){
        if (tower == null){
            buildTower();
        }
        else{
            BuildManager.instance.resetTowerToBuild();
            Destroy(realShowTile);
            line.enabled = false;

            tower.GetComponent<Tower>().createUpgradePanelUI();
        }
    }

    void OnMouseEnter(){
        //rend.material.color = hoverColor;
        Vector3 tempPos = transform.position;
        tempPos.y += 0.6f;

        if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.laserShooterPrefab){
            realShowTile = Instantiate(laserShooter, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/LaserShooter").GetComponent<Image>();
            BuildManager.instance.setSelectedTower("LaserShooter", tempUIImage.sprite);
            tempUIImage.sprite = white;
            CreatePoints(100, StatTracker.instance.getRange(0));
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.plasmaCanonPrefab){
            realShowTile = Instantiate(plasmaCanon, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/PlasmaCanon").GetComponent<Image>();
            BuildManager.instance.setSelectedTower("PlasmaCanon", tempUIImage.sprite);
            tempUIImage.sprite = white;
            CreatePoints(100, StatTracker.instance.getRange(3));
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.cryoCanonPrefab){
            realShowTile = Instantiate(cryoCanon, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/CryoCanon").GetComponent<Image>();
            BuildManager.instance.setSelectedTower("CryoCanon", tempUIImage.sprite);
            tempUIImage.sprite = white;
            CreatePoints(100, StatTracker.instance.getRange(4));
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.energyGeneratorPrefab){
            realShowTile = Instantiate(energyGenerator, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/EnergyGenerator").GetComponent<Image>();
            BuildManager.instance.setSelectedTower("EnergyGenerator", tempUIImage.sprite);
            tempUIImage.sprite = white;
            CreatePoints(100, StatTracker.instance.getRange(6));
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.lightSabreArmPrefab){
            realShowTile = Instantiate(lightSabreArm, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/LightSabreArm").GetComponent<Image>();
            BuildManager.instance.setSelectedTower("LightSabreArm", tempUIImage.sprite);
            tempUIImage.sprite = white;
            CreatePoints(100, StatTracker.instance.getRange(1));
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.plasmaTowerPrefab){
            realShowTile = Instantiate(plasmaTower, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/PlasmaTower").GetComponent<Image>();
            BuildManager.instance.setSelectedTower("PlasmaTower", tempUIImage.sprite);
            tempUIImage.sprite = white;
            CreatePoints(100, StatTracker.instance.getRange(2));
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.beaconPrefab){
            realShowTile = Instantiate(beacon, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/Beacon").GetComponent<Image>();
            BuildManager.instance.setSelectedTower("Beacon", tempUIImage.sprite);
            tempUIImage.sprite = white;
            CreatePoints(100, StatTracker.instance.getRange(5));
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.supportTowerPrefab){
            realShowTile = Instantiate(supportTower, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/SupportTower").GetComponent<Image>();
            BuildManager.instance.setSelectedTower("SupportTower", tempUIImage.sprite);
            tempUIImage.sprite = white;
            CreatePoints(100, StatTracker.instance.getRange(7));
        }

        if (BuildManager.instance.GetTowerToBuild() != null){
            realShowTile.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        /*if (BuildManager.instance.GetTowerToBuild() != null){
            realShowTile = Instantiate(showTile, transform);
        }*/
    }

    void OnMouseExit(){
        //rend.material.color = startColor;
        Destroy(realShowTile);
        line.enabled = false;
    }

    [SerializeField] LineRenderer line;

    public void CreatePoints(int steps, float range){
        line.enabled = true;
        line.widthMultiplier = 0.05f;
        line.useWorldSpace = false;
        line.positionCount = steps + 1;
        
        float scaleValue = 1 / transform.localScale.x;

        //Debug.Log(scaleValue);

        float x;
        float z;

        var points = new Vector3[steps + 1];
        for (int currentStep = 0; currentStep < steps + 1; currentStep++){
            float circumferenceProgress = (float)currentStep/steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            x = Mathf.Cos(currentRadian) * range * scaleValue;
            z = Mathf.Sin(currentRadian) * range * scaleValue;

            points[currentStep] = new Vector3(x, 0.6f, z);
        }

        line.SetPositions(points);
    }
}
