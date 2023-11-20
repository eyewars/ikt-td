using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

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

    [SerializeField] private Sprite white;
    private Image tempUIImage;

    private Renderer rend;

    void Start(){
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
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
        buildTower();
    }

    void OnMouseEnter(){
        rend.material.color = hoverColor;
        Vector3 tempPos = transform.position;
        tempPos.y += 0.6f;

        if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.laserShooterPrefab){
            realShowTile = Instantiate(laserShooter, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            BuildManager.instance.setSelectedTower("Laser Shooter", GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/LaserShooter").transform.GetComponent<Sprite>());
            tempUIImage = GameObject.FindGameObjectWithTag("TowerPicker").transform.Find("Viewport/Content/LaserShooter").GetComponent<Image>();
            //tempUIImage.sprite = white;
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.plasmaCanonPrefab){
            realShowTile = Instantiate(plasmaCanon, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.cryoCanonPrefab){
            realShowTile = Instantiate(cryoCanon, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.energyGeneratorPrefab){
            realShowTile = Instantiate(energyGenerator, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.lightSabreArmPrefab){
            realShowTile = Instantiate(lightSabreArm, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.plasmaTowerPrefab){
            realShowTile = Instantiate(plasmaTower, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.beaconPrefab){
            realShowTile = Instantiate(beacon, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else if (BuildManager.instance.GetTowerToBuild() == BuildManager.instance.supportTowerPrefab){
            realShowTile = Instantiate(supportTower, tempPos, transform.rotation);
            realShowTile.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        }

        if (BuildManager.instance.GetTowerToBuild() != null){
            realShowTile.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        /*if (BuildManager.instance.GetTowerToBuild() != null){
            realShowTile = Instantiate(showTile, transform);
        }*/
    }

    void OnMouseExit(){
        rend.material.color = startColor;

        Destroy(realShowTile);
    }

}
