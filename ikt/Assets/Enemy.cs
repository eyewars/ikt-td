using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    
    // DET ER EN BUG AV OG TIL SOM GJØR AT ENEMIESA JITTERER I TARGET POINTSA (VET IKKE HELT HVORFOR DET SKJER, HAR KANSKJE NOE MED DOTSA Å GJØRE)
    public string type = "Normal";

    private float speed = 1f;
    public float baseSpeed = 1f;
    public float health = 2f;
    public int playerDamage = 1;
    public int tokenIncrease = 2;
    public string damageResistance = "Laser";

    public float distanceToWaypoint = 0f;

    // Status conditions
    public List<float[]> laserShooterUpgrade3Status = new List<float[]>();
    public List<float[]> laserShooterUpgrade4Status = new List<float[]>();
    public float cryoCanonUpgrade0Status = 0;
    public float cryoCanonUpgrade2Status = 0;
    public float cryoCanonUpgrade4Status = 0;

    public Transform target;
    private int waypointIndex = 0;

    void Start(){
        target = Waypoints.points[waypointIndex];

        health = health * (float)Math.Pow(1.05, StatTracker.instance.getWave() - 1);
        baseSpeed = baseSpeed * (float)Math.Pow(1.05, StatTracker.instance.getWave() - 1);
        tokenIncrease = tokenIncrease * (int)Math.Pow(1.05, StatTracker.instance.getWave() - 1);

        speed = baseSpeed;
    }

    void Update(){
        Vector3 movement = target.position - transform.position;
        transform.Translate(movement.normalized * speed * Time.deltaTime, Space.World);

        distanceToWaypoint = Vector3.Distance(transform.position, target.position);

        // Var originalt på 0.01, men det stuttera en del av og til da
        if (distanceToWaypoint < 0.1){
            if (waypointIndex >= Waypoints.points.Length - 1){
                Destroy(gameObject);
                StatTracker.instance.takeDamage(playerDamage);
                StatTracker.instance.updateText();
                return;
            }

            waypointIndex++;
            target = Waypoints.points[waypointIndex];
        }

        for (int i = 0; i < laserShooterUpgrade3Status.Count; i++){
            if (laserShooterUpgrade3Status[i][0] >= 1){
                laserShooterUpgrade3Status[i][0] -= 1;
                laserShooterUpgrade3Status[i][1]++;

                health -= 1 / GlobalCalc.instance.getResistance("Laser", damageResistance);
                checkIfDead();
            }

            if (laserShooterUpgrade3Status[i][1] == 3){
                laserShooterUpgrade3Status.RemoveAt(i);
                break;
            }

            laserShooterUpgrade3Status[i][0] += 1 * Time.deltaTime;
        }

        for (int i = 0; i < laserShooterUpgrade4Status.Count; i++){
            if (laserShooterUpgrade4Status[i][0] >= 0.33){
                laserShooterUpgrade4Status[i][0] -= 1;
                laserShooterUpgrade4Status[i][1]++;

                health -= 1 / GlobalCalc.instance.getResistance("Laser", damageResistance);
                checkIfDead();
            }

            if (laserShooterUpgrade4Status[i][1] == 9){
                laserShooterUpgrade4Status.RemoveAt(i);
                break;
            }

            laserShooterUpgrade4Status[i][0] += 1 * Time.deltaTime;
        }

        if (cryoCanonUpgrade0Status > 0){
            speed = baseSpeed * 0.66f;

            cryoCanonUpgrade0Status -= 1 * Time.deltaTime;

            if (cryoCanonUpgrade0Status <= 0){
                speed = baseSpeed;
            }
        }

        if (cryoCanonUpgrade2Status > 0){
            speed = baseSpeed * 0.5f;

            cryoCanonUpgrade2Status -= 1 * Time.deltaTime;

            if (cryoCanonUpgrade2Status <= 0){
                speed = baseSpeed;
            }
        }

        if (cryoCanonUpgrade4Status > 0){
            speed = 0f;

            cryoCanonUpgrade4Status -= 1 * Time.deltaTime;

            if (cryoCanonUpgrade4Status <= 0){
                cryoCanonUpgrade2StatusAdd();
            }
        }
    }

    public void laserShooterUpgrade3StatusAdd(){
        float[] temp = {0, 0};
        laserShooterUpgrade3Status.Add(temp);
    }

    public void laserShooterUpgrade4StatusAdd(){
        float[] temp = {0, 0};
        laserShooterUpgrade4Status.Add(temp);
    }

    public void cryoCanonUpgrade0StatusAdd(){
        cryoCanonUpgrade0Status = 2f;
    }

    public void cryoCanonUpgrade2StatusAdd(){
        cryoCanonUpgrade2Status = 2f;
    }

    public void cryoCanonUpgrade4StatusAdd(){
        cryoCanonUpgrade4Status = 0.5f;
    }

    // Destroy(Spawner.enemies[enemyIndex]) INDEXEN ER NOEN GANGER UTE AV RANGE, MEST SANSYNLIG FORDI ENEMIEN ER DØD ALLEREDE OG DEN PRØVER Å GJØRE DET PÅ NYTT
    // BUGGEN ØDELEGGER IKKE SPILLET, MEN KAAAAAN HENDE DU FÅR PENGER 2 GANGER (Må sjekke)
    public void checkIfDead(){
        int enemyIndex = Spawner.enemies.IndexOf(this.gameObject);
        if (health <= 0){
            StatTracker.instance.changeTokens(tokenIncrease);
            Destroy(Spawner.enemies[enemyIndex]);
            Spawner.enemies.Remove(Spawner.enemies[enemyIndex]);
            StatTracker.instance.updateText();
        }
    }
}
