using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{

    public Color hoverColor;
    private Color startColor;

    public GameObject tower;

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
    }

    void OnMouseExit(){
        rend.material.color = startColor;
    }

}
