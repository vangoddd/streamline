using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAi : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 0.5f;
    public float atkRange = 0.5f;
    public int atkDmg = 50;

    public bool drawGizmo = false;

    public float attackCooldown = 3.5f;
    private float attackTimer = 0f;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    public LayerMask playerMask;

    private EnemyScript enemyScript;
    private Animator animator;

    private int attackCounter = 0;
    public int specialAtkCounter = 4;
    private float stopTimer = 0f;
    private bool stopping = false;
    //###################
    // - Melee (move then attack)
    // - Ranged (stop moving then attack)
    // - Ground slam (when attack counter is 4)
    //#####################

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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

    void Update(){

        if(stopTimer > 0){
            enemyScript.StopEnemyMovement();
            enemyScript.setCanMove(false);
            stopTimer -= Time.deltaTime;
        }else if(stopping){
            stopping = false;
            enemyScript.setCanMove(true); 
        }
        

        if(attackTimer > 0){
            attackTimer -= Time.deltaTime;
        }else{
            if(attackCounter < specialAtkCounter){ //normal attack
                attackCounter++;
                attackTimer = attackCooldown;
                enemyScript.setIsAttacking(1);
                if(Vector2.Distance(transform.position, target.position) <= (atkRange) && !enemyScript.isStunned()){
                    animator.SetTrigger("melee");
                }else{
                    animator.SetTrigger("shoot");
                }
            }else{
                attackCounter = 0;
                enemyScript.setIsAttacking(1);
                attackTimer = attackCooldown;
                animator.SetTrigger("slam");
            }
            
        }
    }

    // Should also handle animation
    void FixedUpdate()
    {
        //-----------------------
        if(path == null){
            return;
        }

        //Debug.Log(path.vectorPath.Count + " " + currentWaypoint);

        if(currentWaypoint >= path.vectorPath.Count){
            currentWaypoint = path.vectorPath.Count-1;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;
        if(stopping){
            rb.velocity = Vector2.zero;
            force = Vector2.zero;
        }
        animator.SetBool("stopping", stopping);
        Move(force);

        float dist = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(dist < nextWaypointDistance){
            currentWaypoint++;
        }

        if(!enemyScript.isAttacking()){
            if(force.x >= 0.01f){
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }else if(force.x <= -0.01f){
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

    }

    private void Move(Vector2 force){
        if(enemyScript.isCanMove()){
            if(enemyScript.isAggro()){  
                rb.AddForce(force);
            }else{
                return;
            }
        }
        
    }
    
    void stop(float duration){
        stopping = true;
        stopTimer = duration;
    }

    void OnDrawGizmos()
    {
        if(drawGizmo){
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, atkRange);
        }
        
    }

}
