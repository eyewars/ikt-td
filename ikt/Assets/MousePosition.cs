using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour{

    public Vector3 screenPosition;
    public Vector3 worldPosition;

    void Start(){
        
    }


    void Update(){
        screenPosition = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
    }
}
