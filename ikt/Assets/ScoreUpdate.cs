using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdate : MonoBehaviour{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start(){
        StatTracker.updateScore();
        scoreText.text = "Score: " + StatTracker.score.ToString();
    }
}
