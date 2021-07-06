using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GuardAI : MonoBehaviour
{
     //ref : https://www.youtube.com/watch?v=jvtFUfJ6CP8&t=162s
    private Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 0.5f;
    public float atkRange = 0.5f;
    public int atkDmg = 50;

    public bool drawGizmo = false;

    public float attackCooldown = 3.5f;
    [SerializeField] float shootCooldown = 3.5f;
    private float attackTimer = 0f;
    private float shootTimer = 0f;

    public GameObject projectilePrefab;
    public float projectileSpeed = 200;
    public int projectileDmg = 20;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    public LayerMask playerMask;

    private EnemyScript enemyScript;
    private Animator animator;

    public Transform atkPoint;

    private float stopTimer = 0f;
    private bool stopping = false;

    private float atkDist;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        target = GameObject.Find("Player").GetComponent<Transform>();

        atkDist = Vector2.Distance(transform.position, atkPoint.position) + atkRange;

        enemyScript = GetComponent<EnemyScript>();
        
    }

    public void startPathfinding(){
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
        animator.SetBool("isStunned", enemyScript.isStunned());

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
            if(!enemyScript.isStunned()){
                if(Vector2.Distance(transform.position, target.position) <= (atkDist)){
                    //play attack animation
                    attackTimer = attackCooldown;
                    enemyScript.setIsAttacking(1);
                    animator.SetTrigger("melee");
                    AudioManager.playSound("guard_whip");

                }
            }
        }

        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            if (!enemyScript.isStunned())
            {
                shootTimer = shootCooldown;
                enemyScript.setIsAttacking(1);
                animator.SetTrigger("ranged");
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

        if(enemyScript.isAggro()){
            animator.SetBool("stopping", stopping);
            Move(force);
        }

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
        if(enemyScript.isCanMove()){
            if(enemyScript.isAggro()){  
                rb.AddForce(force);
            }else{
                return;
            }
        }
        
    }

    public void melee(){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(atkPoint.position, atkRange, playerMask);
        
        if(hitPlayer.Length > 0){
            
            hitPlayer[0].gameObject.GetComponentInParent<PlayerHealthScript>().hurt(atkDmg);
        }
    }

    public void ranged(){
        Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;

        GameObject projectile = (GameObject)Instantiate(projectilePrefab, rb.position, transform.rotation);
        projectile.GetComponent<EnemyProjectile>().setDamage(projectileDmg);
        projectile.GetComponent<Rigidbody2D>().AddForce(dir * projectileSpeed);
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
            Gizmos.DrawWireSphere(atkPoint.position, atkRange);
        }
        
    }
   
}
