using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject hitFx;
    public GameObject dieFx;

    private Rigidbody2D rb;

    private bool isAggroed = false;
    private bool canMove = false;
    public bool finishedAggro = false;

    private Animator animator;

    private float stunTimer = 0;
    private bool stunned = false;

    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(finishedAggro){

            if(stunTimer > 0f){
                stunTimer -= Time.deltaTime;
                if(stunTimer < 0f) stunned = false;
            }

            if(stunned){
                canMove = false;
            }else{
                canMove = true;
            }
        }
        
    }

    public void hurt(int amt){
        this.health -= amt;
        Debug.Log("Health : " + this.health);
        if(health <= 0){
            die();
        }else{
            Instantiate(hitFx, transform.position, Quaternion.identity);
        }
    }

    private void die(){
        Debug.Log("Die");
        Instantiate(dieFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void aggro(){
        if(!isAggroed){
            isAggroed = true;
            animator.SetTrigger("aggro");
            Debug.Log("Aggro!");
        }
    }

    public void stun(){

        //play stunned animation
        
        stunned = true;
        stunTimer = 1f;
    }
    

    //getter setter

    public bool isAggro(){
        //Play Alert Animation
        return isAggroed;
    }

    public void setCanMove(bool move){
        canMove = move;
    }

    public bool isCanMove(){
        return canMove;
    }
}
