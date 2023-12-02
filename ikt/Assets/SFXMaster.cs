using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFXMaster : MonoBehaviour{
    public static SFXMaster instance;

    void Awake(){
        if (instance != null){
            Debug.LogError("BRO DET ER MER ENN EN STATRACKER!!!!!!!!!!!!!!!!!!");
            return;
        }
        instance = this;
    }

    public AudioSource mineSFX;
    public AudioSource hackingSFX;
    public AudioSource beaconSFX;

    public AudioSource[] deathSFX;

    public AudioSource[] impactSFX;

    public void playDeathSFX(){
        int index = Random.Range(0, 6);

        deathSFX[index].Play();
    }

    public void playImpactSFX(){
        int index = Random.Range(0, 4);

        impactSFX[index].Play();
    }
}
