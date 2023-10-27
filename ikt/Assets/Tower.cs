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
    public GameObject bullet;
    public GameObject mine;

    public float shootTimer;
    public float mineTimer;

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
    private GameObject[] partToRotateArr;
    private GameObject partToRotate;

    private Vector3 partToRotatePositionTemp;

    public List<GameObject> lightsabreEnemies = new List<GameObject>();
    GameObject[] lightsabreArmsToRotateAll;
    List<GameObject> lightsabreArmsToRotate = new List<GameObject>();
    float lightsabreArmRotateTimer = 0f;

    public string type = "Laser Shooter";

    public string damageType = "Laser";

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

    public int tokensPerPump = 5;

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

    // upgradePanel er en UI Prefab vi dro inn i Unity editoren
    // panel er instancen (som blir lagd senere)
    private GameObject upgradePanel;
    public GameObject panel;
    private GameObject descriptionPanel;
    private TextMeshProUGUI descriptionPanelText;
    private TextMeshProUGUI upgradeCostText;
    private TextMeshProUGUI sellValueText;

    private string descriptionText;

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

            cryoCanonUpgrade0 = true;
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
        }
         else if (type == "Support Tower"){
            damage = StatTracker.instance.getDamage(7);
            range = StatTracker.instance.getRange(7);
            attackSpeed = StatTracker.instance.getAttackSpeed(7);
            projectileSpeed = StatTracker.instance.getProjectileSpeed(7);
            explosionRadius = StatTracker.instance.getExplosionRadius(7);
            cost = StatTracker.instance.getCost(7);

            upgradeCosts = StatTracker.instance.getUpgradeCost(7);
            upgradeDescriptions = StatTracker.instance.getUpgradeDescription(7);

            descriptionText = StatTracker.instance.getDescription(7);
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
            lightsabreArmsToRotateAll = GameObject.FindGameObjectsWithTag("LightsabreRotate");
            lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[0]);

            for (int i = 0; i < lightsabreArmsToRotateAll.Length; i++){
                lightsabreArmsToRotateAll[i].tag = "Untagged";
            }
        }

        partToRotateArr = GameObject.FindGameObjectsWithTag("Rotate");
        partToRotate = partToRotateArr[0];
        upgrade1Model.SetActive(false);
        upgrade2Model.SetActive(false);
        upgrade3Model.SetActive(false);
        upgrade4Model.SetActive(false);

        // Fjerner "Rotate" taggen sånn at ikke de blir funnet på nytt av neste tårn
        for (int i = 0; i < partToRotateArr.Length; i++){
            partToRotateArr[i].tag = "Untagged";
        }

        // KANSKJE IDK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // FJERN DENNE LINJA NÅR DU FÅR MODELL SHITTET TIL Å FUNGERE ELLER NOE SÅNN !!!!!!!!!!!!!!!!!!!!!!!!!! 
        // KANSKJE IDK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        partToRotatePositionTemp = partToRotate.transform.position;

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
        if ((type != "Energy Generator") && (type != "Support Tower") && (type != "Lightsabre Arm")){
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
                    if (StatTracker.instance.getTokens() > 5000){
                        StatTracker.instance.changeTokens(60);
                    }
                    else StatTracker.instance.changeTokens((int)(tokensPerPump + (StatTracker.instance.getTokens() * 0.01)));
                }
                else {
                    StatTracker.instance.changeTokens(tokensPerPump);
                }
                StatTracker.instance.updateText();
                shootTimer -= attackSpeed;
            }
            
            shootTimer += 1 * Time.deltaTime;

            partToRotate.transform.rotation = Quaternion.Euler(0f, 360 * shootTimer / attackSpeed, 0f);
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
                    int upgrade3StatusIndex = theEnemy.GetComponent<Enemy>().lightsabreArmUpgrade3StatusTower.IndexOf(this);
                    //Debug.Log(upgrade3StatusIndex);

                    float bonusDamage = 0f;
                    if (lightsabreArmUpgrade3){
                        bonusDamage = theEnemy.GetComponent<Enemy>().lightsabreArmUpgrade3StatusTimer[upgrade3StatusIndex] / 10;
                    }
                    theEnemy.GetComponent<Enemy>().health -= dealDamage(Spawner.enemies[enemyIndex].GetComponent<Enemy>().damageResistance, bonusDamage);
                    // KAN HENDE AT DETTE SYSTEMET BARE STRAIGHT UP IKKE FUNKER, HUSK Å TEST DET NÅR DU KAN ROTERE OG SJEKKE ORDENTLIG HVILKEN ENEMY SOM ENTERA FØRST

                    if (theEnemy.GetComponent<Enemy>().health <= 0){
                        StatTracker.instance.changeTokens(theEnemy.GetComponent<Enemy>().tokenIncrease);
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

            if (lightsabreArmRotateTimer >= 1){
                lightsabreArmRotateTimer -= 1;
            }

            lightsabreArmRotateTimer += 1 * Time.deltaTime;

            for (int i = 0; i < lightsabreArmsToRotate.Count; i++){
                lightsabreArmsToRotate[i].transform.rotation = Quaternion.Euler(0f, 0f, 360 * lightsabreArmRotateTimer / 1);
            }   
        }
    }

    public float dealDamage(string enemyResistance, float bonusDamage){
        if (damageType == enemyResistance){
            return (damage + bonusDamage) / 2;
        }
        else return damage + bonusDamage;
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
        sellValueText.text = sellValue.ToString();
        if (totalUpgrades < 4){
            upgradeCostText.text = upgradeCosts[totalUpgrades].ToString();
        }
    }

    public void upgradeTower(){
        if (totalUpgrades == 4){
            return;
        }

        if (StatTracker.instance.getTokens() < upgradeCosts[totalUpgrades]){
            return;
        }

        upgradeType(type, totalUpgrades);

        StatTracker.instance.changeTokens(-upgradeCosts[totalUpgrades]);
        StatTracker.instance.updateText();

        totalMoneySpent += upgradeCosts[totalUpgrades];
        updateSellValue();

        totalUpgrades++;

        updateUIText();
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

        upgradeCostText = panel.transform.Find("UpgradeCostText").GetComponent<TextMeshProUGUI>();
        //upgradeCostText.text = upgradeCosts[totalUpgrades].ToString();

        sellValueText = panel.transform.Find("SellValueText").GetComponent<TextMeshProUGUI>();
        //sellValueText.text = sellValue.ToString();

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
            }
            else if (upgradeNumber == 1){
                damage = 1f;

                lightsabreArmsToRotate.Clear();
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[2]);

                upgrade1Model.SetActive(false);
                upgrade2Model.SetActive(true);
                partToRotate = partToRotateArr[2];
            }
            else if (upgradeNumber == 2){
                lightsabreArmUpgrade3 = true;

                lightsabreArmsToRotate.Clear();
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[3]);
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[4]);

                upgrade2Model.SetActive(false);
                upgrade3Model.SetActive(true);
                partToRotate = partToRotateArr[3];
            }
            else if (upgradeNumber == 3){


                lightsabreArmsToRotate.Clear();
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[5]);
                lightsabreArmsToRotate.Add(lightsabreArmsToRotateAll[6]);

                upgrade3Model.SetActive(false);
                upgrade4Model.SetActive(true);
                partToRotate = partToRotateArr[4];

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
                attackSpeed = 1.2f;

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

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){

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
        else if (towerType == "Energy Generator"){
            if (upgradeNumber == 0){
                energyGeneratorUpgrade1 = true;

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){
                tokensPerPump = 10;

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
        else if (towerType == "Support Tower"){
            if (upgradeNumber == 0){

                upgrade0Model.SetActive(false);
                upgrade1Model.SetActive(true);
                partToRotate = partToRotateArr[1];
            }
            else if (upgradeNumber == 1){

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
    }
}
