using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSelect : MonoBehaviour{
 
    public int towerIndex = 0;

    public TextMeshProUGUI buttonText;

    void Start(){
        buttonText.text = StatTracker.instance.getCost(towerIndex).ToString();
    }

    public void ChangeTower(){
        BuildManager.instance.setTowerToBuild(towerIndex);
    }
}
