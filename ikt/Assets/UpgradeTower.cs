using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour{
    public void upgradeTower(){
        BuildManager.instance.currentTileHoldingTowerUI.GetComponent<Tile>().tower.GetComponent<Tower>().upgradeTower();
    }
}
