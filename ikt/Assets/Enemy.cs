using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private float ccResistanceScaling = 1f;
    private float ccResistanceBase = 1f;
    private GameObject wheel;

    public float distanceToWaypoint = 0f;
    public float distanceToLastWaypoint = 10f;

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
    public float hackingUpgrade0Status = 0;
    public float hackingUpgrade1Status = 0;
    public float hackingUpgrade2Status = 0;
    public float hackingUpgrade2StatusDowntime = 0;
    public float hackingUpgrade3Status = 0;

    public Transform target;
    private int waypointIndex = 0;

    void Start(){
        //transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        if (!type.Contains("Fast")){
            wheel = GameObject.FindGameObjectWithTag("EnemyWheel");
            wheel.tag = "Untagged";
        }

        target = Waypoints.points[waypointIndex];

        float extraScalingBoss = StatTracker.instance.getWave() / 1000;
        float extraScalingNormal = StatTracker.instance.getWave() / 2000;

        if ((type == "normalBoss") || (type == "laserBoss") || (type == "plasmaBoss")){
            maxHealth = (maxHealth + (0.1f * StatTracker.instance.getWave())) * (float)Math.Pow(1.05 + extraScalingBoss, StatTracker.instance.getWave() - 1);
        }
        else{
            maxHealth = (maxHealth + (0.1f * StatTracker.instance.getWave())) * (float)Math.Pow(1.03 + extraScalingNormal, StatTracker.instance.getWave() - 1);
        }

        health = maxHealth;

        if (StatTracker.instance.getWave() >= 60){
            baseSpeed = baseSpeed * (float)Math.Pow(1.02, 75 - 1);
        }
        else baseSpeed = baseSpeed * (float)Math.Pow(1.02, StatTracker.instance.getWave() - 1);

        int maxTokenWave = 20;
        if (StatTracker.instance.getWave() >= maxTokenWave){
            tokenIncrease = (int)(tokenIncrease + (0.1f * maxTokenWave)) * (int)Math.Pow(1.05, maxTokenWave - 1);
        }
        else tokenIncrease = (int)(tokenIncrease + (0.1f * StatTracker.instance.getWave())) * (int)Math.Pow(1.05, StatTracker.instance.getWave() - 1);

        speed = baseSpeed;

        if (damageResistance == "None"){
            ccResistanceBase = 0.5f;
        }

        if (StatTracker.instance.getWave() <= 80){
            ccResistanceScaling = 1 - (StatTracker.instance.getWave() * 0.01f);
        }
        else ccResistanceScaling = 0.2f;

        ccResistance = ccResistanceBase * ccResistanceScaling;
    }

    void Update(){
        Vector3 movement = target.position - transform.position;
        Vector3 movement2 = new Vector3(-100, 0, 1);
        if (waypointIndex > 0){
            movement2 = Waypoints.points[waypointIndex-1].position - transform.position;
        }

        if (hackingUpgrade2Status <= 0){
            transform.Translate(movement.normalized * speed * Time.deltaTime, Space.World);
        }
        else transform.Translate(movement2.normalized * speed * Time.deltaTime, Space.World);

        distanceToWaypoint = Vector3.Distance(transform.position, target.position);
        if (waypointIndex > 0){
            distanceToLastWaypoint = Vector3.Distance(transform.position, Waypoints.points[waypointIndex-1].position);
        }

        if (!type.Contains("Fast")){
            wheel.transform.Rotate(180 * Time.deltaTime, 0f, 0f);
        }

        Vector3 direction = target.position - transform.position;
        Quaternion turnDirection = Quaternion.LookRotation(direction);
        Vector3 turnRotation = turnDirection.eulerAngles;
            
        //transform.rotation = Quaternion.Euler(90f, turnRotation.y, 0f);
        transform.rotation = Quaternion.Euler(0f, turnRotation.y, 0f);

        // Var originalt på 0.01, men det stuttera en del av og til da
        if ((distanceToWaypoint < 0.1) && (hackingUpgrade2Status <= 0)){
            if (waypointIndex >= Waypoints.points.Length - 1){
                Destroy(gameObject);
                StatTracker.instance.takeDamage(playerDamage);
                StatTracker.instance.updateText();
                return;
            }

            waypointIndex++;
            target = Waypoints.points[waypointIndex];
        }

        if ((distanceToLastWaypoint < 0.1) && (hackingUpgrade2Status > 0)){
            if (waypointIndex != 0){
                waypointIndex--;
            }
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

        if (hackingUpgrade0Status > 0){
            hackingUpgrade0Status -= (1 / ccResistance) * Time.deltaTime;
        }

        if (hackingUpgrade1Status > 0){
            hackingUpgrade1Status -= (1 / ccResistance) * Time.deltaTime;
        }

        if (hackingUpgrade2Status > 0){
            hackingUpgrade2Status -= (1 / ccResistance) * Time.deltaTime;

            if (hackingUpgrade2Status <= 0){
                hackingUpgrade2StatusDowntime = 1.2f;
            }
        }

        if (hackingUpgrade2StatusDowntime > 0){
            hackingUpgrade2StatusDowntime -= (1 / ccResistance) * Time.deltaTime;
        }

        if (hackingUpgrade3Status > 0){
            //Gjør sånn at den innebygde resisten blir resatt og enemien sin resist kun er det de får over tid
            ccResistance = ccResistanceScaling;

            hackingUpgrade3Status -= (1 / ccResistance) * Time.deltaTime;

            if (hackingUpgrade3Status <= 0){
                if (type == "None"){
                    ccResistance = ccResistanceBase * ccResistanceScaling;
                }
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

    public void hackingUpgrade0StatusAdd(bool hasUpgrade4){
        if (hasUpgrade4){
            hackingUpgrade0Status = 2f;
        }
        else if (hackingUpgrade0Status <= 1f){
            hackingUpgrade0Status = 1f;
        } 
    }

    public void hackingUpgrade1StatusAdd(bool hasUpgrade4){
        if (hasUpgrade4){
            hackingUpgrade1Status = 2f;
        }
        else if (hackingUpgrade1Status <= 1.2f){
            hackingUpgrade1Status = 1.2f;
        } 
    }

    public void hackingUpgrade2StatusAdd(bool hasUpgrade4){
        if (hackingUpgrade2StatusDowntime > 0) return;

        int tempRandom = Random.Range(1, 101);

        if (hasUpgrade4){
            if (tempRandom <= 35){
                hackingUpgrade2Status = 0.8f;
            }
        }
        else {
            if (tempRandom <= 25){
                if (hackingUpgrade2Status <= 0.5f){
                    hackingUpgrade2Status = 0.5f;
                } 
            }
        }
    }

    public void hackingUpgrade3StatusAdd(bool hasUpgrade4){
        if (hasUpgrade4){
            hackingUpgrade3Status = 2f;
        }
        else if (hackingUpgrade3Status <= 1.3f){
            hackingUpgrade3Status = 1.3f;
        } 
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
            SFXMaster.instance.playDeathSFX();
            StatTracker.instance.changeTokens(tokenIncrease, Spawner.enemies[enemyIndex].GetComponent<Enemy>().hackingUpgrade1Status);
            Destroy(Spawner.enemies[enemyIndex]);
            Spawner.enemies.Remove(Spawner.enemies[enemyIndex]);
            StatTracker.instance.updateText();
        }
    }
}
