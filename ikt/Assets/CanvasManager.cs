using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour{
    
    public static CanvasManager instance;

    void Awake(){
        if (instance != null){
            Debug.LogError("BRO DET ER MER ENN EN BUILDMANAGER!!!!!!!!!!!!!!!!!!");
            return;
        }
        instance = this;
    }
}
