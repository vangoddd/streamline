using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAi : MonoBehaviour
{
    private Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 0.5f;
    public float atkRange = 0.5f;
    public int atkDmg = 30;

    public bool drawGizmo = false;

    public float attackCooldown = 3.5f;
    private float attackTimer = 3f;

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

    public float projectileSpeed = 200f;

    public GameObject specialAtkFx;
    public GameObject projectilePrefab;

    public Transform spawnPoint;

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

        target = GameObject.Find("Player").GetComponent<Transform>();

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
        animator.SetBool("isAggroed", enemyScript.isAggro());

        if(stopTimer > 0){
            enemyScript.StopEnemyMovement();
            enemyScript.setCanMove(false);
            stopTimer -= Time.deltaTime;
        }else if(stopping){
            stopping = false;
            enemyScript.setCanMove(true); 
        }
        
        if(enemyScript.isAggro()){
            if(attackTimer > 0){
                attackTimer -= Time.deltaTime;
            }else{
                if(attackCounter < specialAtkCounter){ //normal attack
                    attackCounter++;
                    attackTimer = attackCooldown;
                    enemyScript.setIsAttacking(1);
                    if(Vector2.Distance(transform.position, target.position) <= (atkRange) && !enemyScript.isStunned()){
                        animator.SetTrigger("melee");
                        if(attackCounter % 2 == 0){
                            AudioManager.playSound("brute_punch");
                        }else{
                            AudioManager.playSound("brute_punch2");
                        }
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
    }

    // HANDLING ATK DMG

    //******** Melee **********
    public void meleeAttack(){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, atkRange, playerMask);
        if(hitPlayer.Length > 0){      
            hitPlayer[0].gameObject.GetComponentInParent<PlayerHealthScript>().hurt(atkDmg);

            AudioManager.playSound("brute_punchhit");
        }
    }

    //******** Ranged *********
    public void rangedAttack(){
        Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;

        GameObject projectile = (GameObject)Instantiate(projectilePrefab, rb.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(dir * projectileSpeed);
    }

    //******** Special ********
    private int spcAtkCount = 0;

    public void specialAttack(){
        spcAtkCount = 10;
        InvokeRepeating("spawnAttack", 0, 0.1f);

        AudioManager.playSound("brute_slam");

    }

    private void spawnAttack(){
        Debug.Log("attacking!" + spcAtkCount);
        float offset = 0f;
        if(transform.localScale.x < 0){ //facing left
            offset = 0.5f * (10 - spcAtkCount);
        }else{ //facing right
            offset = -0.5f * (10 - spcAtkCount);
        }
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x + offset, spawnPoint.position.y,  spawnPoint.position.z);
        Instantiate(specialAtkFx, spawnPosition, Quaternion.identity);

        AudioManager.playSound("brute_shockwave");

        if (--spcAtkCount == 0) CancelInvoke("spawnAttack");
    }

    // Should also handle animation
    void FixedUpdate()
    {
        //-----------------------
        if(path == null){
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count){
            currentWaypoint = path.vectorPath.Count-1;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;
        if(stopping){
            rb.velocity = Vector2.zero;
            force = Vector2.zero;
        }

        if(enemyScript.isAggro()){
            animator.SetBool("stopping", stopping);
            Move(force);
        }
        

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

    

    public void shakeEvent(){
        CameraShake.Instance.shakeCamera(3f, 0.2f);
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

    public void setAttackTimer(float time){
        attackTimer = time;
    }

}
