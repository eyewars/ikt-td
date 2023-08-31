using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{

    public Color hoverColor;
    private Color startColor;

    private GameObject tower;

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
        tempPos.y += 1;
        tower = (GameObject)Instantiate(towerToBuild, tempPos, transform.rotation);

        if (StatTracker.instance.getTokens() < tower.GetComponent<Tower>().getCost()){
            Destroy(tower);
            return;
        }   

        StatTracker.instance.changeTokens(-tower.GetComponent<Tower>().getCost());
        StatTracker.instance.updateText();
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
