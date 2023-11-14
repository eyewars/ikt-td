using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    
    // DET ER EN BUG AV OG TIL SOM GJØR AT ENEMIESA JITTERER I TARGET POINTSA (VET IKKE HELT HVORFOR DET SKJER, HAR KANSKJE NOE MED DOTSA Å GJØRE)
    public string type = "Normal";

    [HideInInspector] public float speed = 1f;
    public float baseSpeed = 1f;
    public float maxHealth = 2f;
    public float health = 2f;
    public int playerDamage = 1;
    public int tokenIncrease = 2;
    public string damageResistance = "Laser";
    [SerializeField] private float ccResistance = 1f;

    public float distanceToWaypoint = 0f;

    // Status conditions
    public List<float[]> laserShooterUpgrade3Status = new List<float[]>();
    public List<float[]> laserShooterUpgrade4Status = new List<float[]>();
    public float cryoCanonUpgrade0Status = 0;
    public float cryoCanonUpgrade2Status = 0;
    public float cryoCanonUpgrade4Status = 0;
    public float beaconUpgrade0Status = 0;
    public float beaconUpgrade3Status = 0;
    public float beaconUpgrade4Status = 0;
    public float beaconUpgrade4StatusBonusStun = 0;
    public float beaconUpgrade4StatusBonusStunTimeToActivate = 0;
    public List<float> lightsabreArmUpgrade3StatusTimer = new List<float>();
    public List<GameObject> lightsabreArmUpgrade3StatusTower = new List<GameObject>();

    public Transform target;
    private int waypointIndex = 0;

    void Start(){
        target = Waypoints.points[waypointIndex];

        maxHealth = (maxHealth + (0.1f * StatTracker.instance.getWave())) * (float)Math.Pow(1.03, StatTracker.instance.getWave() - 1);
        health = maxHealth;

        if (StatTracker.instance.getWave() >= 60){
            baseSpeed = baseSpeed * (float)Math.Pow(1.02, 75 - 1);
        }
        else baseSpeed = baseSpeed * (float)Math.Pow(1.02, StatTracker.instance.getWave() - 1);

        if (StatTracker.instance.getWave() >= 25){
            tokenIncrease = (int)(tokenIncrease + (0.1f * 25)) * (int)Math.Pow(1.05, 25 - 1);
        }
        else tokenIncrease = (int)(tokenIncrease + (0.1f * StatTracker.instance.getWave())) * (int)Math.Pow(1.05, StatTracker.instance.getWave() - 1);

        speed = baseSpeed;

        if (damageResistance == "None"){
            ccResistance = 0.5f;
        }
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
            speed = baseSpeed * (1 - (0.66f * ccResistance));

            cryoCanonUpgrade0Status -= (1 / ccResistance) * Time.deltaTime;

            if (cryoCanonUpgrade0Status <= 0){
                speed = baseSpeed;
            }
        }

        if (cryoCanonUpgrade2Status > 0){
            speed = baseSpeed * (1 - (0.5f * ccResistance));

            cryoCanonUpgrade2Status -= (1 / ccResistance) * Time.deltaTime;

            if (cryoCanonUpgrade2Status <= 0){
                speed = baseSpeed;
            }
        }

        if (cryoCanonUpgrade4Status > 0){
            speed = 0f;

            cryoCanonUpgrade4Status -= (1 / ccResistance) * Time.deltaTime;

            if (cryoCanonUpgrade4Status <= 0){
                cryoCanonUpgrade2StatusAdd();
            }
        }

        if (beaconUpgrade0Status > 0){
            speed = 0f;

            beaconUpgrade0Status -= (1 / ccResistance) * Time.deltaTime;

            if (beaconUpgrade0Status <= 0){
                speed = baseSpeed;
            }
        }

        if (beaconUpgrade3Status > 0){
            speed = 0f;

            beaconUpgrade3Status -= (1 / ccResistance) * Time.deltaTime;

            if (beaconUpgrade3Status <= 0){
                speed = baseSpeed;
            }
        }

        if (beaconUpgrade4Status > 0){
            speed = 0f;

            beaconUpgrade4Status -= (1 / ccResistance) * Time.deltaTime;

            if (beaconUpgrade4Status <= 0){
                speed = baseSpeed;
                beaconUpgrade4StatusBonusStunTimeToActivate = 0.5f;
            }
        }

        if (beaconUpgrade4StatusBonusStunTimeToActivate > 0){
            beaconUpgrade4StatusBonusStunTimeToActivate -= (1 / ccResistance) * Time.deltaTime;

            if (beaconUpgrade4StatusBonusStunTimeToActivate <= 0){
                beaconUpgrade4StatusBonusStun = 0.5f;
            }
        }

        if (beaconUpgrade4StatusBonusStun > 0){
            speed = 0f;

            beaconUpgrade4StatusBonusStun -= (1 / ccResistance) * Time.deltaTime;

            if (beaconUpgrade4StatusBonusStun <= 0){
                speed = baseSpeed;
            }
        }

        for (int i = 0; i < lightsabreArmUpgrade3StatusTimer.Count; i++){
            lightsabreArmUpgrade3StatusTimer[i] += 1 * Time.deltaTime;
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

    public void beaconUpgrade0StatusAdd(){
        beaconUpgrade0Status = 0.5f;
    }

    public void beaconUpgrade3StatusAdd(){
        beaconUpgrade3Status = 0.8f;
    }

    public void beaconUpgrade4StatusAdd(){
        beaconUpgrade4Status = 0.8f;
    }

    public void lightsabreArmUpgrade3StatusAdd(GameObject myTower){
        lightsabreArmUpgrade3StatusTimer.Add(0f);
        lightsabreArmUpgrade3StatusTower.Add(myTower);
    }

    // Destroy(Spawner.enemies[enemyIndex]) INDEXEN ER NOEN GANGER UTE AV RANGE, MEST SANSYNLIG FORDI ENEMIEN ER DØD ALLEREDE OG DEN PRØVER Å ØDELEGGE PÅ NYTT
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
