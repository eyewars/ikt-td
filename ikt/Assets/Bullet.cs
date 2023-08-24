using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(new Vector3(15 * Time.deltaTime, 0, 0), Space.World);

        if (transform.position.x >= 30){
            Destroy(gameObject);
        }
    }
}
