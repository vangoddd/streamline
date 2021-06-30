using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public enum EnemyType{
        ground,
        air
    }

    public GameObject hitFx;
    public GameObject dieFx;

    public bool isBoss = false;

    private Rigidbody2D rb;

    private bool isAggroed = false;
    private bool canMove = false;
    public bool finishedAggro = false;

    private Animator animator;

    private float stunTimer = 0;
    private bool stunned = false;
    public int stunResist = 0;
    private int consecutiveStun = 0;
    public bool cannotBeStunned = false;

    private bool attacking = false;

    public bool dropHeal = false;
    public GameObject healPot;

    public int health = 100;
    public EnemyType enemyType;

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

            if(stunned || attacking){
                canMove = false;
            }else{
                canMove = true;
            }
        }
        
    }

    public void hurt(int amt, bool ranged, float hurtStun){
        if(!isAggroed){
            aggro();
        }

        if(!(enemyType == EnemyType.ground && ranged)){
            stun(hurtStun);
        }

        if(ranged && enemyType == EnemyType.ground){
            this.health -= (int) ((float) amt * 0.5f);
        }else if(!ranged && enemyType == EnemyType.air){
            this.health -= (int) ((float) amt * 1.2f);
        }
        else{
            this.health -= amt;
        }
        Debug.Log("Health : " + this.health);
        if(health <= 0){
            die();
        }else{
            Instantiate(hitFx, transform.position, Quaternion.identity);
        }
    }

    private void die(){
        CameraShake.Instance.shakeCamera(2f, 0.2f);

        if(dropHeal){
            Instantiate(healPot, transform.position, Quaternion.identity);
        }

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

    public void stun(float duration){
        if(cannotBeStunned){
            return;
        }

        consecutiveStun++;
        
        if((consecutiveStun >= stunResist) && (stunResist > 0)){
            Debug.Log("breaking from stun");
            stunned = false;
            stunTimer = 0;
            consecutiveStun = 0;
            return;
        }
        
        stunned = true;
        stunTimer = duration;
        rb.velocity = new Vector2(0f, 0f);
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(8, 8);
        // if (collision.gameObject.tag == "enemy")
        // {
        //     Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
        //     Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        // }
    }
    

    //getter setter

    public bool isAggro(){
        //Play Alert Animation
        return isAggroed;
    }

    public void setCanMove(bool move){
        canMove = move;
    }

    public void setCanMoveInt(int move){
        if(move == 0){
            canMove = false;
        }else{
            canMove = true;
        }
    }

    public bool isCanMove(){
        return canMove;
    }

    public void StopEnemyMovement(){
        rb.velocity = new Vector2(0f, 0f);
    }

    public bool isStunned(){
        return stunned;
    }

    public void setIsAttacking(int a){
        if(a == 0){
            attacking = false;
        }else{
            attacking = true;
        }
    }

    public bool isAttacking(){
        return attacking;
    }
}
