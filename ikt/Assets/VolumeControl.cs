using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour{
    public AudioMixer mixer;
    public string myAudioType;
    public Slider mySlider;

    public static float sliderValueMusic = 1f;
    public static float sliderValueEffect = 1f;

    public void setVolume (float value){
        mixer.SetFloat(myAudioType, Mathf.Log10(value) * 20);

        if (myAudioType == "MusicVolume"){
            sliderValueMusic = value;
        }
        else{
            sliderValueEffect = value;
        }
    }

    void Awake(){
        if (myAudioType == "MusicVolume"){
            mySlider.value = sliderValueMusic;
        }
        else{
            mySlider.value = sliderValueEffect;
        }
    }
}
