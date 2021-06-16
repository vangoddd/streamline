using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GroundAI : MonoBehaviour
{
    //ref : https://www.youtube.com/watch?v=jvtFUfJ6CP8&t=162s
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 0.5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        enemyScript = GetComponent<EnemyScript>();
        //will be called when player is close enough
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath(){
        if(seeker.IsDone()){
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    // Should also handle animation
    void FixedUpdate()
    {
        if(path == null){
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            return;
        }else{
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        Move(force);

        float dist = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(dist < nextWaypointDistance){
            currentWaypoint++;
        }

        if(force.x >= 0.01f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if(force.x <= -0.01f){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    private void Move(Vector2 force){
        if(enemyScript.isAggro() && enemyScript.isCanMove()){
            rb.AddForce(force);
        }else{
            return;
        }
    }
}
