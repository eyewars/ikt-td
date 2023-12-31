using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
// using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using Unity.VisualScripting;

public class Tower : MonoBehaviour{

    public Tile myTile;

    // TYPE OF BULLET TO SHOOT
    private List<GameObject> shootPointArr = new List<GameObject>();
    public Transform shootPoint;
    public GameObject bullet;
    public GameObject mine;

    public float shootTimer;
    public float mineTimer;
    public float plasmaTowerCooldownTimer;

    // LIST OF BULLETS THAT THE TOWER SHOT
    public static List<GameObject> bullets = new List<GameObject>();
    public static List<GameObject> mines = new List<GameObject>();

    public string enemyTag = "Enemy";

    public Transform enemyTarget = null;

    public GameObject upgrade0Model;
    public GameObject upgrade1Model;
    public GameObject upgrade2Model;
    public GameObject upgrade3Model;
    public GameObject upgrade4Model;
    private List<GameObject> partToRotateArr = new List<GameObject>();
    private GameObject partToRotate;

    private Vector3 partToRotatePositionTemp;

    private GameObject rotate0;
    private GameObject rotate1;
    private GameObject rotate2;
    private GameObject rotate3;
    private GameObject rotate4;

    public List<GameObject> lightsabreEnemies = new List<GameObject>();
    List<GameObject> lightsabreArmsToRotateAll = new List<GameObject>();
    List<GameObject> lightsabreArmsToRotate = new List<GameObject>();

    List<GameObject> plasmaShooterLineRenderersGameObject = new List<GameObject>();
    List<LineRenderer> plasmaShooterLineRenderers = new List<LineRenderer>();

    public string type = "Laser Shooter";

    public string damageType = "Laser";

    public List<string> targetingOptions = new List<string>();
    public int currentTargetingIndex = 0;

    public float damage;
    public float range;
    public float attackSpeed;
    public float projectileSpeed;
    public float explosionRadius;
    public int cost;
    public int[] upgradeCosts;
    public string[] upgradeDescriptions;
    public int totalMoneySpent = 0;
    public int sellValue = 0;
    public int totalUpgrades = 0;

    public int tokensPerPump = 3;

    float bonusDamagePlasmaTower = 0f;

    // Upgrade conditions
    public bool laserShooterUpgrade3 = false;
    public bool laserShooterUpgrade4 = false;
    public bool plasmaCanonUpgrade3 = false;
    public bool plasmaCanonUpgrade4 = false;
    public bool cryoCanonUpgrade0 = false;
    public bool cryoCanonUpgrade2 = false;
    public bool cryoCanonUpgrade4 = false;
    public bool energyGeneratorUpgrade1 = false;
    public bool energyGeneratorUpgrade4 = false;
    public bool lightsabreArmUpgrade3 = false;
    public bool lightsabreArmUpgrade4 = false;
    public bool beaconUpgrade0 = false;
    public bool beaconUpgrade3 = false;
    public bool beaconUpgrade4 = false;
    public bool hackingUpgrade0 = false;
    public bool hackingUpgrade1 = false;
    public bool hackingUpgrade2 = false;
    public bool hackingUpgrade3 = false;
    public bool hackingUpgrade4 = false;

    // upgradePanel er en UI Prefab vi dro inn i Unity editoren
    // panel er instancen (som blir lagd senere)
    private GameObject upgradePanel;
    public GameObject panel;
    private GameObject descriptionPanel;
    private TextMeshProUGUI descriptionPanelText;
    private TextMeshProUGUI upgradeCostText;
    private TextMeshProUGUI sellValueText;
    private TextMeshProUGUI towerNameText;
    private TextMeshProUGUI targetText;
    private Image upgradeImageUI;

    public Sprite[] upgradeImageUIArray;

    private string descriptionText;

    public AudioSource beaconSFX;

    void Awake(){
        if (type == "Laser Shooter"){
            damage = StatTracker.instance.getDamage(0);
            range = StatTracker.instance.getRange(0);
            attackSpeed = StatTracker.instance.getAttackSpeed(0);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(0);
            explosionRadius = StatTracker.instance.getExplosionRadius(0);
            cost = StatTracker.instance.getCost(0);

            upgradeCosts = StatTracker.instance.getUpgradeCost(0);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(0);

            descriptionText = StatTracker.instance.getDescription(0);

            targetingOptions = StatTracker.instance.getTargetingOptions(0);
        }
        else if (type == "Lightsabre Arm"){
            damage = StatTracker.instance.getDamage(1);
            range = StatTracker.instance.getRange(1);
            attackSpeed = StatTracker.instance.getAttackSpeed(1);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(1);
            explosionRadius = StatTracker.instance.getExplosionRadius(1);
            cost = StatTracker.instance.getCost(1);

            upgradeCosts = StatTracker.instance.getUpgradeCost(1);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(1);

            descriptionText = StatTracker.instance.getDescription(1);

            targetingOptions = StatTracker.instance.getTargetingOptions(1);
        }
        else if (type == "Plasma Tower"){
            damage = StatTracker.instance.getDamage(2);
            range = StatTracker.instance.getRange(2);
            attackSpeed = StatTracker.instance.getAttackSpeed(2);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(2);
            explosionRadius = StatTracker.instance.getExplosionRadius(2);
            cost = StatTracker.instance.getCost(2);

            upgradeCosts = StatTracker.instance.getUpgradeCost(2);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(2);

            descriptionText = StatTracker.instance.getDescription(2);

            targetingOptions = StatTracker.instance.getTargetingOptions(2);
        }
        else if (type == "Plasma Canon"){
            damage = StatTracker.instance.getDamage(3);
            range = StatTracker.instance.getRange(3);
            attackSpeed = StatTracker.instance.getAttackSpeed(3);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(3);
            explosionRadius = StatTracker.instance.getExplosionRadius(3);
            cost = StatTracker.instance.getCost(3);

            upgradeCosts = StatTracker.instance.getUpgradeCost(3);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(3);

            descriptionText = StatTracker.instance.getDescription(3);

            targetingOptions = StatTracker.instance.getTargetingOptions(3);
        }
        else if (type == "Cryo Canon"){
            damage = StatTracker.instance.getDamage(4);
            range = StatTracker.instance.getRange(4);
            attackSpeed = StatTracker.instance.getAttackSpeed(4);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(4);
            explosionRadius = StatTracker.instance.getExplosionRadius(4);
            cost = StatTracker.instance.getCost(4);

            upgradeCosts = StatTracker.instance.getUpgradeCost(4);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(4);

            descriptionText = StatTracker.instance.getDescription(4);

            targetingOptions = StatTracker.instance.getTargetingOptions(4);

            cryoCanonUpgrade0 = true;
        }
        else if (type == "Beacon"){
            damage = StatTracker.instance.getDamage(5);
            range = StatTracker.instance.getRange(5);
            attackSpeed = StatTracker.instance.getAttackSpeed(5);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(5);
            explosionRadius = StatTracker.instance.getExplosionRadius(5);
            cost = StatTracker.instance.getCost(5);

            upgradeCosts = StatTracker.instance.getUpgradeCost(5);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(5);

            descriptionText = StatTracker.instance.getDescription(5);

            targetingOptions = StatTracker.instance.getTargetingOptions(5);

            beaconUpgrade0 = true;
        }
        else if (type == "Energy Generator"){
            damage = StatTracker.instance.getDamage(6);
            range = StatTracker.instance.getRange(6);
            attackSpeed = StatTracker.instance.getAttackSpeed(6);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(6);
            explosionRadius = StatTracker.instance.getExplosionRadius(6);
            cost = StatTracker.instance.getCost(6);

            upgradeCosts = StatTracker.instance.getUpgradeCost(6);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(6);

            descriptionText = StatTracker.instance.getDescription(6);

            targetingOptions = StatTracker.instance.getTargetingOptions(6);
        }
         else if (type == "Hacking Tower"){
            damage = StatTracker.instance.getDamage(7);
            range = StatTracker.instance.getRange(7);
            attackSpeed = StatTracker.instance.getAttackSpeed(7);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(7);
            explosionRadius = StatTracker.instance.getExplosionRadius(7);
            cost = StatTracker.instance.getCost(7);

            upgradeCosts = StatTracker.instance.getUpgradeCost(7);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(7);

            descriptionText = StatTracker.instance.getDescription(7);

            targetingOptions = StatTracker.instance.getTargetingOptions(7);

            hackingUpgrade0 = true;
        }
        shootTimer = attackSpeed;
    }

    public GameObject bulletHolder;
    public GameObject mineHolder;
    void Start(){
        upgradePanel = BuildManager.instance.upgradePanel;
        bulletHolder = GameObject.Find("Bullets");
        mineHolder = GameObject.Find("Mines");

        if (type == "Lightsabre Arm"){
            for (int i = 0; i < 7; i++){
                lightsabreArmsToRotateAll.Add(GameObject.FindGameObjectWithTag("Sabre" + i));
                lightsabreArmsToRotateAll[i].tag = "Untagged";
            }

            lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[0]);
        }

        if (type == "Plasma Tower"){
            for (int i = 0; i < 5; i++){
               plasmaShooterLineRenderersGameObject.Add(GameObject.FindGameObjectWithTag("Line" + i)); 
            }

            for (int i = 0; i < plasmaShooterLineRenderersGameObject.Count; i++){
                plasmaShooterLineRenderersGameObject[i].tag = "Untagged";
                plasmaShooterLineRenderers.Add(plasmaShooterLineRenderersGameObject[i].GetComponent<LineRenderer>());
            }

            plasmaShooterLineRenderers[0].enabled = false;
        }

        if ((type == "Laser Shooter") || (type == "Plasma Canon") || (type == "Plasma Tower") || (type == "Cryo Canon")){

            for (int i = 0; i < 5; i++){
                shootPointArr.Add(GameObject.FindGameObjectWithTag("Shoot" + i));
                shootPointArr[i].tag = "Untagged";
            }

            shootPoint = shootPointArr[0].transform;
        }

        for (int i = 0; i < 5; i++){
            partToRotateArr.Add(GameObject.FindGameObjectWithTag("Rotate" + i));
            partToRotateArr[i].tag = "Untagged";
        }

        partToRotate = partToRotateArr[0];
        upgrade1Model.SetActive(false);
        upgrade2Model.SetActive(false);
        upgrade3Model.SetActive(false);
        upgrade4Model.SetActive(false);

        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
      
        partToRotatePositionTemp = partToRotate.transform.position;

        CreatePoints(100);
    }
    
    // INSTANTIATES BULLETS AND ADDS TO LIST
    // FINDS THE DIRECTION FROM THE BULLET THAT WAS JUST MADE TO THE TARGET, AND ADDS THAT VALUE TO THE myDir VARIABLE
    void shoot(){
        bullets.Add((GameObject)Instantiate(bullet, bulletHolder.transform));
        bullets[bullets.Count - 1].transform.position = shootPoint.position;
            
        bullets[bullets.Count - 1].GetComponent<Bullet>().myDir = enemyTarget.position - shootPoint.position;

        bullets[bullets.Count - 1].GetComponent<Bullet>().myTower = this;
    }

    void plantMine(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        List<Collider> groundColliders = new List<Collider>();
        foreach (var hitCollider in hitColliders){
            if (hitCollider.tag == "Ground"){
                if (!hitCollider.GetComponent<Ground>().hasMine){
                    groundColliders.Add(hitCollider);
                }  
            }
        }

        if (groundColliders.Count > 0){
            int randomGround = Random.Range(0, groundColliders.Count);

            groundColliders[randomGround].GetComponent<Ground>().hasMine = true;

            mines.Add((GameObject)Instantiate(mine, mineHolder.transform));
            mines[mines.Count - 1].transform.position = groundColliders[randomGround].transform.position;
            mines[mines.Count - 1].transform.position += new Vector3(0, 0.7f, 0);

            mines[mines.Count - 1].GetComponent<Mine>().myTower = this; 
        }
    }

    void findEnemy(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (currentTargetingIndex == 0){
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

                if (tempWaypointNum > System.Array.IndexOf(Waypoints.points, enemyNearestEnd.GetComponent<Enemy>().target)){
                    enemyNearestEnd = enemies[i];
                    nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                }
                
                if ((tempWaypointNum == System.Array.IndexOf(Waypoints.points, enemyNearestEnd.GetComponent<Enemy>().target)) && (enemies[i].GetComponent<Enemy>().distanceToWaypoint < nearestWaypoint)){
                    enemyNearestEnd = enemies[i];
                    nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                }
            }

            enemyTarget = null;
            if (enemyNearestEnd != null){
                enemyTarget = enemyNearestEnd.transform;
            }
        }
        else if (currentTargetingIndex == 1){
            GameObject enemyNearestEnd = null;
            float nearestWaypoint = Mathf.Infinity;
            float mostMaxHealth = 0f;

            for (int i = 0; i < enemies.Length; i++){
                Transform targetOfEnemy = enemies[i].GetComponent<Enemy>().target;
                int tempWaypointNum = System.Array.IndexOf(Waypoints.points, targetOfEnemy);
                float tempMaxHealth = enemies[i].GetComponent<Enemy>().maxHealth;

                float distanceToEnemy = (enemies[i].transform.position - transform.position).magnitude;

                if (distanceToEnemy > range){
                    continue;
                }

                if (enemyNearestEnd == null){
                    enemyNearestEnd = enemies[i];
                    nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                }

                if (tempMaxHealth > mostMaxHealth){
                    enemyNearestEnd = enemies[i];
                    nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                    mostMaxHealth = tempMaxHealth;
                }

                if ((tempMaxHealth == mostMaxHealth) && (tempWaypointNum > System.Array.IndexOf(Waypoints.points, enemyNearestEnd.GetComponent<Enemy>().target))){
                    enemyNearestEnd = enemies[i];
                    nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                    mostMaxHealth = tempMaxHealth;
                }
                
                if ((tempMaxHealth == mostMaxHealth) && (tempWaypointNum == System.Array.IndexOf(Waypoints.points, enemyNearestEnd.GetComponent<Enemy>().target)) && (enemies[i].GetComponent<Enemy>().distanceToWaypoint < nearestWaypoint)){
                    enemyNearestEnd = enemies[i];
                    nearestWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                    mostMaxHealth = tempMaxHealth;
                }
            }

            enemyTarget = null;
            if (enemyNearestEnd != null){
                enemyTarget = enemyNearestEnd.transform;
            }
        }
        else if (currentTargetingIndex == 2){
            GameObject enemyNearestTower = null;
            float nearestTower = Mathf.Infinity;

            for (int i = 0; i < enemies.Length; i++){
                float distanceToEnemy = (enemies[i].transform.position - transform.position).magnitude;

                if (distanceToEnemy > range){
                    continue;
                }

                if (enemyNearestTower == null){
                    enemyNearestTower = enemies[i];
                    nearestTower = distanceToEnemy;
                }

                if (distanceToEnemy < nearestTower){
                    enemyNearestTower = enemies[i];
                    nearestTower = distanceToEnemy;
                }
            }

            enemyTarget = null;
            if (enemyNearestTower != null){
                enemyTarget = enemyNearestTower.transform;
            }
        }
        else if (currentTargetingIndex == 3){
            GameObject enemyNearestStart = null;
            float furthestFromWaypoint = 0f;

            for (int i = 0; i < enemies.Length; i++){
                Transform targetOfEnemy = enemies[i].GetComponent<Enemy>().target;
                int tempWaypointNum = System.Array.IndexOf(Waypoints.points, targetOfEnemy);

                float distanceToEnemy = (enemies[i].transform.position - transform.position).magnitude;

                if (distanceToEnemy > range){
                    continue;
                }

                if (enemyNearestStart == null){
                    enemyNearestStart = enemies[i];
                    furthestFromWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                }

                if (tempWaypointNum < System.Array.IndexOf(Waypoints.points, enemyNearestStart.GetComponent<Enemy>().target)){
                    enemyNearestStart = enemies[i];
                    furthestFromWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                }
                
                if ((tempWaypointNum == System.Array.IndexOf(Waypoints.points, enemyNearestStart.GetComponent<Enemy>().target)) && (enemies[i].GetComponent<Enemy>().distanceToWaypoint > furthestFromWaypoint)){
                    enemyNearestStart = enemies[i];
                    furthestFromWaypoint = enemies[i].GetComponent<Enemy>().distanceToWaypoint;
                }
            }

            enemyTarget = null;
            if (enemyNearestStart != null){
                enemyTarget = enemyNearestStart.transform;
            }
        }
    }

    void Update(){
        if ((type != "Energy Generator") && (type != "Hacking Tower") && (type != "Lightsabre Arm") && (type != "Beacon") && (type != "Plasma Tower")){
            findEnemy();

            if ((type == "Plasma Canon") && plasmaCanonUpgrade3){
                if (mineTimer >= 3){
                    plantMine();
                    mineTimer -= 3;
                }
                mineTimer += 1 * Time.deltaTime; 
            }

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
        else if(type == "Energy Generator"){
            if (shootTimer <= (attackSpeed / 2)){
                //10*(0.75/1.5)*2
                partToRotate.transform.position = new Vector3(partToRotatePositionTemp.x, partToRotatePositionTemp.y - (0.15f * (shootTimer / attackSpeed) * 2), partToRotatePositionTemp.z);
            }
            else{
                partToRotate.transform.position = new Vector3(partToRotatePositionTemp.x, partToRotatePositionTemp.y - (0.15f * (1 - (shootTimer / attackSpeed)) * 2), partToRotatePositionTemp.z);
            }

            if (shootTimer >= attackSpeed){
                if (energyGeneratorUpgrade4){
                    if (StatTracker.instance.getTokens() > 2000){
                        StatTracker.instance.changeTokens((int)(tokensPerPump + (2000 * 0.005)), 0);
                    }
                    else StatTracker.instance.changeTokens((int)(tokensPerPump + (StatTracker.instance.getTokens() * 0.005)), 0);
                }
                else {
                    StatTracker.instance.changeTokens(tokensPerPump, 0);
                }
                StatTracker.instance.updateText();
                shootTimer -= attackSpeed;
            }
            
            shootTimer += 1 * Time.deltaTime;

            partToRotate.transform.rotation = Quaternion.Euler(0f, 360 * shootTimer / attackSpeed, 0f);
        }
        else if (type == "Beacon"){
            if (shootTimer >= attackSpeed){
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
                List<Collider> enemiesHit = new List<Collider>();
                foreach (var hitCollider in hitColliders){
                    if (hitCollider.tag == "Enemy"){
                        enemiesHit.Add(hitCollider);
                    }
                }  

                for (int i = 0; i < enemiesHit.Count; i++){
                    enemiesHit[i].GetComponent<Enemy>().health -= dealDamage(enemiesHit[i].GetComponent<Enemy>(), 0);

                    if (beaconUpgrade4){
                        enemiesHit[i].GetComponent<Enemy>().beaconUpgrade4StatusAdd(); 
                    }
                    else if(beaconUpgrade3){
                        enemiesHit[i].GetComponent<Enemy>().beaconUpgrade3StatusAdd(); 
                    }
                    else{
                        enemiesHit[i].GetComponent<Enemy>().beaconUpgrade0StatusAdd(); 
                    }
                            
                    if (enemiesHit[i].GetComponent<Enemy>().health <= 0){
                        SFXMaster.instance.playDeathSFX();
                        StatTracker.instance.changeTokens(enemiesHit[i].GetComponent<Enemy>().tokenIncrease, enemiesHit[i].GetComponent<Enemy>().hackingUpgrade1Status);
                        Destroy(enemiesHit[i].gameObject);
                        Spawner.enemies.Remove(enemiesHit[i].gameObject);
                        StatTracker.instance.updateText();

                        //enemiesToRemoveFromList.Add(i);
                    }
                }

                if (enemiesHit.Count > 0){
                    //SFXMaster.instance.beaconSFX.Play();
                    beaconSFX.Play();
                }

                shootTimer -= attackSpeed;
            }
            shootTimer += 1 * Time.deltaTime;
        }
        else if (type == "Hacking Tower"){
            if (shootTimer >= attackSpeed){
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
                List<Collider> enemiesHit = new List<Collider>();
                foreach (var hitCollider in hitColliders){
                    if (hitCollider.tag == "Enemy"){
                        enemiesHit.Add(hitCollider);
                    }
                }  

                for (int i = 0; i < enemiesHit.Count; i++){
                    //Tårnet gjør ingen damage, men om det skulle gjort, så er det denne du ville brukt (bare uncomment den)
                    //enemiesHit[i].GetComponent<Enemy>().health -= dealDamage(enemiesHit[i].GetComponent<Enemy>().damageResistance, 0);

                    if (hackingUpgrade4){
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade3StatusAdd(true);
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade2StatusAdd(true); 
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade1StatusAdd(true);
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade0StatusAdd(true); 
                    }
                    else if(hackingUpgrade3){
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade3StatusAdd(false);
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade2StatusAdd(false); 
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade1StatusAdd(false);
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade0StatusAdd(false); 
                    }
                    else if(hackingUpgrade2){
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade2StatusAdd(false); 
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade1StatusAdd(false);
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade0StatusAdd(false); 
                    }
                    else if(hackingUpgrade1){
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade1StatusAdd(false);
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade0StatusAdd(false); 
                    }
                    else if(hackingUpgrade0){
                        enemiesHit[i].GetComponent<Enemy>().hackingUpgrade0StatusAdd(false); 
                    }

                    //Hvis den skal gjøre damage, må den sjekke om de blir drept her
                    /*    
                    if (enemiesHit[i].GetComponent<Enemy>().health <= 0){
                        StatTracker.instance.changeTokens(enemiesHit[i].GetComponent<Enemy>().tokenIncrease, enemiesHit[i].GetComponent<Enemy>().hackingUpgrade1Status);
                        Destroy(enemiesHit[i].gameObject);
                        Spawner.enemies.Remove(enemiesHit[i].gameObject);
                        StatTracker.instance.updateText();

                        //enemiesToRemoveFromList.Add(i);
                    }
                    */
                }

                shootTimer -= attackSpeed;
            }
            shootTimer += 1 * Time.deltaTime;
            
            //partToRotate.transform.Rotate(0f, 0f, 80 * Time.deltaTime);
            partToRotate.transform.rotation = Quaternion.Euler(90f, 0f, 360 * shootTimer / attackSpeed);
        }
        else if (type == "Plasma Tower"){
            findEnemy();

            if (plasmaTowerCooldownTimer > 0){
                plasmaTowerCooldownTimer -= 1 * Time.deltaTime;

                plasmaShooterLineRenderers[totalUpgrades].enabled = false;

                return;
            }

            if (enemyTarget == null){
                plasmaShooterLineRenderers[totalUpgrades].enabled = false;

                return;
            }
            else {
                plasmaShooterLineRenderers[totalUpgrades].enabled = true;
            }

            if (totalUpgrades == 4){
                bonusDamagePlasmaTower = 2.5f;
            }
            else if (totalUpgrades == 3){
                if (bonusDamagePlasmaTower >= 2.5f){
                    bonusDamagePlasmaTower = 2.5f;
                }
                else bonusDamagePlasmaTower += 0.15f * Time.deltaTime;
            }

            plasmaShooterLineRenderers[totalUpgrades].SetPosition(0, shootPoint.position);
            plasmaShooterLineRenderers[totalUpgrades].SetPosition(1, enemyTarget.position);

            Vector3 direction = enemyTarget.position - transform.position;
            Quaternion turnDirection = Quaternion.LookRotation(direction);
            Vector3 turnRotation = turnDirection.eulerAngles;
            
            partToRotate.transform.rotation = Quaternion.Euler(0f, turnRotation.y, 0f);

            if (shootTimer >= attackSpeed){
                enemyTarget.GetComponent<Enemy>().health -= dealDamage(enemyTarget.GetComponent<Enemy>(), 0);

                if (enemyTarget.GetComponent<Enemy>().health <= 0){
                    SFXMaster.instance.playDeathSFX();
                    StatTracker.instance.changeTokens(enemyTarget.GetComponent<Enemy>().tokenIncrease, enemyTarget.GetComponent<Enemy>().hackingUpgrade1Status);
                    Destroy(enemyTarget.gameObject);
                    Spawner.enemies.Remove(enemyTarget.gameObject);
                    StatTracker.instance.updateText();

                    if (totalUpgrades >= 1){
                        plasmaTowerCooldownTimer = 1f;
                    }
                    else plasmaTowerCooldownTimer = 2f;

                    bonusDamagePlasmaTower = 0f;
                }

                shootTimer -= attackSpeed;
            }

            shootTimer += 1 * Time.deltaTime;
        }
        else if (type == "Lightsabre Arm"){
            if (shootTimer >= attackSpeed){
                //Spawner.enemies[enemyIndex].GetComponent<Enemy>().health -= myTower.dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>().damageResistance, 0);

                List<int> enemiesToRemoveFromList = new List<int>();

                for (int i = 0; i < lightsabreEnemies.Count; i++){
                    int enemyIndex = Spawner.enemies.IndexOf(lightsabreEnemies[i]);
                    
                    if (enemyIndex == -1){
                        enemiesToRemoveFromList.Add(i);
                        continue;
                    }

                    GameObject theEnemy = Spawner.enemies[enemyIndex];

                    // KAN HENDE AT DETTE SYSTEMET BARE STRAIGHT UP IKKE FUNKER, HUSK Å TEST DET NÅR DU KAN ROTERE OG SJEKKE ORDENTLIG HVILKEN ENEMY SOM ENTERA FØRST
                    int upgrade3StatusIndex = theEnemy.GetComponent<Enemy>().lightsabreArmUpgrade3StatusTower.IndexOf(gameObject);
                    //Debug.Log(upgrade3StatusIndex);

                    float bonusDamage = 0f;
                    if (lightsabreArmUpgrade3){
                        bonusDamage = theEnemy.GetComponent<Enemy>().lightsabreArmUpgrade3StatusTimer[upgrade3StatusIndex] / 10;
                    }

                    if (lightsabreArmUpgrade4){
                        if ((theEnemy.GetComponent<Enemy>().speed != theEnemy.GetComponent<Enemy>().baseSpeed) || (theEnemy.GetComponent<Enemy>().hackingUpgrade2Status > 0)){
                            bonusDamage += 0.5f;
                        }
                    }

                    theEnemy.GetComponent<Enemy>().health -= dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>(), bonusDamage);
                    // KAN HENDE AT DETTE SYSTEMET BARE STRAIGHT UP IKKE FUNKER, HUSK Å TEST DET NÅR DU KAN ROTERE OG SJEKKE ORDENTLIG HVILKEN ENEMY SOM ENTERA FØRST

                    if (theEnemy.GetComponent<Enemy>().health <= 0){
                        SFXMaster.instance.playDeathSFX();
                        StatTracker.instance.changeTokens(theEnemy.GetComponent<Enemy>().tokenIncrease, theEnemy.GetComponent<Enemy>().hackingUpgrade1Status);
                        Destroy(theEnemy);
                        Spawner.enemies.Remove(theEnemy);
                        StatTracker.instance.updateText();

                        enemiesToRemoveFromList.Add(i);
                    }
                }

                for (int i = 0; i < enemiesToRemoveFromList.Count; i++){
                    lightsabreEnemies.RemoveAt(enemiesToRemoveFromList[enemiesToRemoveFromList.Count - i - 1]);
                }

                shootTimer -= attackSpeed;
            }

            shootTimer += 1 * Time.deltaTime; 

            for (int i = 0; i < lightsabreArmsToRotate.Count; i++){
                lightsabreArmsToRotate[i].transform.Rotate(0, 0, 180 * Time.deltaTime);
            }   
        }
    }

    public float dealDamage(Enemy enemy, float bonusDamage){
        float damageResist = 1;
        float damageMultiplier = 1;

        if ((damageType == enemy.damageResistance) && (enemy.hackingUpgrade3Status <= 0)){
            damageResist = 2;
        }
        
        if (enemy.hackingUpgrade0Status > 0){
            damageMultiplier = 1.5f;
        }
        
        return ((damage + bonusDamage) / damageResist) * damageMultiplier;
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

    public void updateSellValue(){
        sellValue = (int)(totalMoneySpent * 0.8);
    }

    public void updateUIText(){
        descriptionPanelText.text = upgradeDescriptions[totalUpgrades];

        upgradeImageUI.sprite = upgradeImageUIArray[totalUpgrades];

        sellValueText.text = sellValue.ToString();
        if (totalUpgrades < 4){
            upgradeCostText.text = upgradeCosts[totalUpgrades].ToString();
        }
        else{
            upgradeCostText.enabled = false;
            panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
        }

        //GameObject.Find("TargetingButton").GetComponentInChildren<TextMeshProUGUI>().text = targetingOptions[currentTargetingIndex];
        targetText.text = targetingOptions[currentTargetingIndex];
    }

    public void upgradeTower(){
        if (totalUpgrades == 4){
            return;
        }

        if (StatTracker.instance.getTokens() < upgradeCosts[totalUpgrades]){
            return;
        }

        upgradeType(type, totalUpgrades);

        StatTracker.instance.changeTokens(-upgradeCosts[totalUpgrades], 0);
        StatTracker.instance.updateText();

        totalMoneySpent += upgradeCosts[totalUpgrades];
        updateSellValue();

        totalUpgrades++;

        if ((type == "Laser Shooter") || (type == "Plasma Canon") || (type == "Plasma Tower") || (type == "Cryo Canon")){
           shootPoint = shootPointArr[totalUpgrades].transform; 
        }
        
        updateUIText();
    }

    public void changeTargeting(){
        if (currentTargetingIndex == (targetingOptions.Count - 1)){
            currentTargetingIndex = 0;
        }
        else{
            currentTargetingIndex++;
        }

        GameObject.Find("TargetingButton").GetComponentInChildren<TextMeshProUGUI>().text = targetingOptions[currentTargetingIndex];

        if (type == "Lightsabre Arm"){
            partToRotate.transform.Rotate(0, 90, 0);
        }
       
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

        //Destroy(panel);
    }

    public void createUpgradePanelUI(){
        Destroy(GameObject.FindGameObjectWithTag("UpgradePanelTag"));

        //Når UIen blir lagd så settes currentTileHoldingTowerUI til å være dette tårnet sin tile
        //Når UIen blir slettet, så blir ikke den variabelen resatt, så være forsiktig med det + husk å slette
        //JEG TROR DEN KOMMENTAREN OVER ER TROLL, SÅ BARE IGNORER
        BuildManager.instance.currentTileHoldingTowerUI = myTile;

        //Vi lager en instance av upgradePanel inni panel, så setter vi panel til å være en parent av Canvas
        panel = (GameObject)Instantiate(upgradePanel);
        panel.transform.SetParent(CanvasManager.instance.transform, false);

        descriptionPanel = panel.transform.Find("UpgradeButton/UpgradeDescription").gameObject;
        descriptionPanelText = descriptionPanel.GetComponentInChildren<TextMeshProUGUI>(true);
        //descriptionPanelText.text = upgradeDescriptions[totalUpgrades];

        descriptionPanel.SetActive(false);

        towerNameText = panel.transform.Find("TowerName").GetComponent<TextMeshProUGUI>();
        if (type != "Plasma Canon"){
            towerNameText.text = type;
        }
        else towerNameText.text = "Plasma Cannon";

        upgradeImageUI = panel.transform.Find("TowerImage").GetComponent<Image>();

        upgradeCostText = panel.transform.Find("UpgradeCostText").GetComponent<TextMeshProUGUI>();
        //upgradeCostText.text = upgradeCosts[totalUpgrades].ToString();

        sellValueText = panel.transform.Find("SellValueText").GetComponent<TextMeshProUGUI>();
        //sellValueText.text = sellValue.ToString();

        targetText = panel.transform.Find("TargetingButton").GetComponentInChildren<TextMeshProUGUI>();

        //Kommenterte ut .text greiene fordi de blir gjort i updateUIText()
        
        updateUIText();
    }

    void OnMouseDown(){
        createUpgradePanelUI();
    }

    void upgradeType(string towerType, int upgradeNumber){
        if (towerType == "Laser Shooter"){
            if (upgradeNumber == 0){
                damage = 1.5f;

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){
                range = 4.5f;
                CreatePoints(100);

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];
            }
            else if (upgradeNumber == 2){
                laserShooterUpgrade3 = true;

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];
            }
            else if (upgradeNumber == 3){
                laserShooterUpgrade3 = false;
                laserShooterUpgrade4 = true;

                attackSpeed = 0.2f;

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

                upgradeCostText.enabled = false;
                panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
            }
        }
        else if (towerType == "Lightsabre Arm"){
            if (upgradeNumber == 0){

                lightsabreArmsToRotate.Clear();
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[1]);

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];

                partToRotate.transform.Rotate(0, 90 * currentTargetingIndex, 0);
            }
            else if (upgradeNumber == 1){
                damage = 0.4f;

                lightsabreArmsToRotate.Clear();
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[2]);

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];

                partToRotate.transform.Rotate(0, 90 * currentTargetingIndex, 0);
            }
            else if (upgradeNumber == 2){
                lightsabreArmUpgrade3 = true;

                lightsabreArmsToRotate.Clear();
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[3]);
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[4]);

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];

                partToRotate.transform.Rotate(0, 90 * currentTargetingIndex, 0);
            }
            else if (upgradeNumber == 3){
                lightsabreArmUpgrade4 = true;

                lightsabreArmsToRotate.Clear();
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[5]);
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[6]);

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

                partToRotate.transform.Rotate(0, 90 * currentTargetingIndex, 0);

                upgradeCostText.enabled = false;
                panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
            }
        }
        else if (towerType == "Plasma Tower"){
            if (upgradeNumber == 0){

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){
                damage = 0.9f;

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];
            }
            else if (upgradeNumber == 2){

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];
            }
            else if (upgradeNumber == 3){

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

                upgradeCostText.enabled = false;
                panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
            }
        }
        else if (towerType == "Plasma Canon"){
            if (upgradeNumber == 0){
                range = 3.5f;
                CreatePoints(100);

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){
                explosionRadius = 2.5f;

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];
            }
            else if (upgradeNumber == 2){
                plasmaCanonUpgrade3 = true;

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];
            }
            else if (upgradeNumber == 3){
                explosionRadius = 3f;

                plasmaCanonUpgrade4 = true;

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

                upgradeCostText.enabled = false;
                panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
            }
        }
        else if (towerType == "Cryo Canon"){
            if (upgradeNumber == 0){
                attackSpeed = 1.8f;

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){
                cryoCanonUpgrade0 = false;
                cryoCanonUpgrade2 = true;

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];
            }
            else if (upgradeNumber == 2){
                explosionRadius = 1.5f;

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];
            }
            else if (upgradeNumber == 3){
                damage = 1f;
                explosionRadius = 2f;

                cryoCanonUpgrade2 = false;
                cryoCanonUpgrade4 = true;

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

                upgradeCostText.enabled = false;
                panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
            }
        }
        else if (towerType == "Beacon"){
            if (upgradeNumber == 0){
                range = 2.5f;
                CreatePoints(100);
                damage = 0.4f;

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){
                attackSpeed = 2f;
                damage = 0.5f;

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];
            }
            else if (upgradeNumber == 2){
                beaconUpgrade3 = true;
                beaconUpgrade0 = false;
                damage = 0.8f;
                range = 2.8f;
                CreatePoints(100);

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];
            }
            else if (upgradeNumber == 3){
                beaconUpgrade4 = true;
                beaconUpgrade3 = false;
                damage = 1f;
                range = 3f;
                CreatePoints(100);

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

                upgradeCostText.enabled = false;
                panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
            }
        }
        else if (towerType == "Energy Generator"){
            if (upgradeNumber == 0){
                energyGeneratorUpgrade1 = true;

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){
                tokensPerPump = 8;

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];
            }
            else if (upgradeNumber == 2){
                attackSpeed = 2.5f;

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];
            }
            else if (upgradeNumber == 3){
                energyGeneratorUpgrade4 = true;

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

                upgradeCostText.enabled = false;
                panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
            }
        }
        else if (towerType == "Hacking Tower"){
            if (upgradeNumber == 0){
                hackingUpgrade1 = true;
                hackingUpgrade0 = false;

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){
                hackingUpgrade2 = true;
                hackingUpgrade1 = false;

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];
            }
            else if (upgradeNumber == 2){
                hackingUpgrade3 = true;
                hackingUpgrade2 = false;

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];
            }
            else if (upgradeNumber == 3){
                range = 3f;
                CreatePoints(100);
                
                hackingUpgrade4 = true;
                hackingUpgrade3 = false;

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

                upgradeCostText.enabled = false;
                panel.transform.Find("UpgradeCostImg").GetComponent<Image>().enabled = false;
            }
        }
    }
}
