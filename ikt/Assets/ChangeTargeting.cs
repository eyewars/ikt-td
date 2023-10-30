using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTargeting : MonoBehaviour{
    public void changeTargeting(){
        BuildManager.instance.currentTileHoldingTowerUI.GetComponent<Tile>().tower.GetComponent<Tower>().changeTargeting();
    }
}
