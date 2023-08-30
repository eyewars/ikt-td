using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    
    public float speed = 1f;
    public float distanceToWaypoint = 0f;

    public float health = 2f;

    public Transform target;
    private int waypointIndex = 0;

    void Start(){
        target = Waypoints.points[waypointIndex];
    }

    void Update(){
        Vector3 movement = target.position - transform.position;
        transform.Translate(movement.normalized * speed * Time.deltaTime, Space.World);

        distanceToWaypoint = Vector3.Distance(transform.position, target.position);

        if (Vector3.Distance(transform.position, target.position) < 0.01){
            if (waypointIndex >= Waypoints.points.Length - 1){
                Destroy(gameObject);
                StatTracker.instance.takeDamage(1);
                StatTracker.instance.updateText();
                return;
            }

            waypointIndex++;
            target = Waypoints.points[waypointIndex];
        }
    }
}
