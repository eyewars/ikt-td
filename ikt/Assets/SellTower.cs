using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTower : MonoBehaviour{
    public void sellTower(){
        Destroy(BuildManager.instance.currentTileHoldingTowerUI.GetComponent<Tile>().tower.GetComponent<Tower>().panel);

        BuildManager.instance.currentTileHoldingTowerUI.sellTower();
    }
}
