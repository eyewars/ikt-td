using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    
    GameObject myTower;

    public int towerIndex;
    
    public Vector3 myDir;

    void Start(){
        myTower = Towers.towers[0];
        myDir = myTower.GetComponent<Tower>().enemyTarget.position - myTower.GetComponent<Tower>().bullet.transform.position;
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(myDir.normalized * 20 * Time.deltaTime, Space.World);

        if (transform.position.x >= 30){
            Destroy(gameObject);
        }
    }
}
