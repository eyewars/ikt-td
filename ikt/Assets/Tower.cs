using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour{

    // TYPE OF BULLET TO SHOOT
    public GameObject bullet;

    public float shootTimer;

    // LIST OF BULLETS THAT THE TOWER SHOT
    public static List<GameObject> bullets = new List<GameObject>();

    public string enemyTag = "Enemy";

    public Transform enemyTarget = null;

    public GameObject partToRotate;

    public string type = "Laser Shooter";

    public float damage;
    public float range;
    public float attackSpeed;
    public float projectileSpeed;
    public int cost;

    // upgradePanel er en UI Prefab vi dro inn i Unity editoren
    // panel er instancen (som blir lagd senere)
    public GameObject upgradePanel;
    private GameObject panel;
    private GameObject description;
    private TextMeshProUGUI descriptionText;

    void Awake(){
        if (type == "Laser Shooter"){
            damage = StatTracker.instance.getDamage(0);
            range = StatTracker.instance.getRange(0);
            attackSpeed = StatTracker.instance.getAttackSpeed(0);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(0);
            cost = StatTracker.instance.getCost(0);
        }
        else if (type == "Plasma Canon"){
            damage = StatTracker.instance.getDamage(1);
            range = StatTracker.instance.getRange(1);
            attackSpeed = StatTracker.instance.getAttackSpeed(1);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(1);
            cost = StatTracker.instance.getCost(1);
        }

        shootTimer = attackSpeed;
    }

    public GameObject bulletHolder;
    void Start(){
        bulletHolder = GameObject.Find("Bullets");

        // KANSKJE IDK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // FJERN DENNE LINJA NÅR DU FÅR MODELL SHITTET TIL Å FUNGERE ELLER NOE SÅNN !!!!!!!!!!!!!!!!!!!!!!!!!! 
        // KANSKJE IDK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        CreatePoints(100);
    }
    
    // INSTANTIATES BULLETS AND ADDS TO LIST
    // FINDS THE DIRECTION FROM THE BULLET THAT WAS JUST MADE TO THE TARGET, AND ADDS THAT VALUE TO THE myDir VARIABLE
    void shoot(){
        bullets.Add((GameObject)Instantiate(bullet, bulletHolder.transform));
        bullets[bullets.Count - 1].transform.position = transform.position;
            
        bullets[bullets.Count - 1].GetComponent<Bullet>().myDir = enemyTarget.position - transform.position;

        bullets[bullets.Count - 1].GetComponent<Bullet>().myTower = this;   
    }

    void findEnemy(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        GameObject enemyNearestEnd = null;
        float nearestWaypoint = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; i++){
            Transform targetOfEnemy = enemies[i].GetComponent<Enemy>().target;
            int tempWaypointNum = System.Array.IndexOf(Waypoints.points, targetOfEnemy);

            float distanceToEnemy = (enemies[i].transform.position - transform.position).magnitude;

            if (distanceToEnemy > range){
                continue;
            }

            if (enemyNearestEnd == null){
                enemyNearestEnd = enemies[i];
                nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
            }
            
            if ((tempWaypointNum >= System.Array.IndexOf(Waypoints.points, enemyNearestEnd.GetComponent<Enemy>().target)) && (enemies[i].GetComponent<Enemy>().distanceToWaypoint < nearestWaypoint)){
                enemyNearestEnd = enemies[i];
                nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
            }
        }

        enemyTarget = null;
        if (enemyNearestEnd != null){
            enemyTarget = enemyNearestEnd.transform;
        }
    }

    void Update(){
        findEnemy();

        if (enemyTarget == null){
            return;
        }

        Vector3 direction = enemyTarget.position - transform.position;
        Quaternion turnDirection = Quaternion.LookRotation(direction);
        Vector3 turnRotation = turnDirection.eulerAngles;
        
        partToRotate.transform.rotation = Quaternion.Euler(0f, turnRotation.y, 0f);

        if (shootTimer >= attackSpeed){
            shoot();
            shootTimer -= attackSpeed;
        }
        
        shootTimer += 1 * Time.deltaTime;
    }

    public float getDamage(){
        return damage;
    }

    public float getRange(){
        return range;
    }

    public float getAttackSpeed(){
        return attackSpeed;
    }

    public float getProjectileSpeed(){
        return projectileSpeed;
    }

    public int getCost(){
        return cost;
    }

    [SerializeField] LineRenderer line;


    // HUSK Å KJØRE DENNE PÅ NYTT HVIS DU OPPDATERER RANGE (DEN KJØRER NÅ KUN EN GANG I START)
    public void CreatePoints(int steps){
        line.enabled = false;
        line.widthMultiplier = 0.05f;
        line.useWorldSpace = false;
        line.positionCount = steps + 1;
        
        float scaleValue = 1 / transform.localScale.x;

        //Debug.Log(scaleValue);

        float x;
        float y;

        var points = new Vector3[steps + 1];
        for (int currentStep = 0; currentStep < steps + 1; currentStep++){
            float circumferenceProgress = (float)currentStep/steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            x = Mathf.Cos(currentRadian) * range * scaleValue;
            y = Mathf.Sin(currentRadian) * range * scaleValue;

            points[currentStep] = new Vector3(x, y, 0f);
        }

        line.SetPositions(points);
    }

    void OnMouseEnter(){
        line.enabled = true;
    }

    void OnMouseExit(){
        line.enabled = false;

        Destroy(panel);
    }

    public void createUpgradePanelUI(){
        if (panel != null){
            Destroy(panel);
        }

        //Vi lager en instance av upgradePanel inni panel, så setter vi panel til å være en parent av Canvas
        panel = (GameObject)Instantiate(upgradePanel);
        panel.transform.SetParent(CanvasManager.instance.transform, false);

        description = panel.transform.Find("UpgradeButton/UpgradeDescription").gameObject;
        descriptionText = description.GetComponentInChildren<TextMeshProUGUI>(true);
        descriptionText.text = "Testtttt";
    }

    void OnMouseDown(){
        createUpgradePanelUI();
    }
}
