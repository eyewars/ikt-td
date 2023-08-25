using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Towers : MonoBehaviour{

    public static List<GameObject> towers = new List<GameObject>();
    public GameObject typeOfTower;

    void Awake(){
        for (int i = 0; i < 2; i++){
            towers.Add((GameObject)Instantiate(typeOfTower, this.transform));
            towers[towers.Count - 1].transform.position = new Vector3(towers.Count + 1, 1, 3); 
        } 
    }

    
    void Update(){
        
    }
}
