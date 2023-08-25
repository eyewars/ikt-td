using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Towers : MonoBehaviour{

    // LIST OF TOWERS
    public static List<GameObject> towers = new List<GameObject>();
    public GameObject typeOfTower;

    // INSTANTIATES TOWERS, AND ADDS THEM INSIDE LIST (AND AS A CHILD OF EMPTY CONTAINER)
    void Awake(){
        for (int i = 0; i < 1; i++){
            towers.Add((GameObject)Instantiate(typeOfTower, this.transform));
            towers[towers.Count - 1].transform.position = new Vector3(towers.Count + 1, 1, 3); 
        } 
    }
}
