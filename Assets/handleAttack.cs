using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleAttack : MonoBehaviour
{
    private Animator animator;
    private CharacterController2D controller;
    private Rigidbody2D rb;
    public int attackDamage = 20;

    public Transform attackCheck;
    public float attackRange;

    private BasicMovement basicMovement;
    
    public LayerMask enemyMask;

    private int combo = 0;
    private bool canCombo = false;

    public Collider2D attackHitBox0;
    public Collider2D attackHitBox1;
    public Collider2D attackHitBox2;
    public Collider2D attackHitBox3;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>(); 
        basicMovement = GetComponent<BasicMovement>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !basicMovement.jump){
            

            //play the correct attack animation
            if(combo == 0){
                basicMovement.attacking = true;
                animator.SetTrigger("Attack");
                rb.velocity = new Vector2(0f, rb.velocity.y);
                applyDamage(combo);
                
            }else{
                if(canCombo){
                    if((Input.GetAxisRaw("Horizontal") > 0.01f && !controller.isFacingRight()) || 
                    (Input.GetAxisRaw("Horizontal") < -0.01f && controller.isFacingRight())){
                        controller.Flip();
                    }
                    animator.SetTrigger("Combo");
                    if(controller.isFacingRight()){
                        rb.AddForce(new Vector2(120f, 170f));
                    }else{
                        rb.AddForce(new Vector2(-120f, 170f));
                    }
                    applyDamage(combo);
                }
            }
            
            if(combo > 4){
                combo = 0;
            }
            
        }
    }

    void applyDamage(int comboCount){
        basicMovement.canMove = false;
        this.combo++;

        
    }

    public void animationHit(int comboCount){
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(enemyMask);
        filter.useLayerMask = true;
        Collider2D[] hitEnemies = new Collider2D[9999];
        Collider2D check = null;

        switch(comboCount) 
        {
        case 0:
            check = attackHitBox0;
            break;
        case 1:
            check = attackHitBox1;
            break;
        case 2:
            check = attackHitBox2;
            break;
        case 3:
            check = attackHitBox3;
            break;
        default:
            check = attackHitBox0;
            break;
        }

        Physics2D.OverlapCollider(check, filter, hitEnemies); 

        foreach(Collider2D e in hitEnemies){
            //apply dmg
            if(e != null){
                EnemyScript enemy = e.GetComponent<EnemyScript>();
                Rigidbody2D enemyRb = e.GetComponent<Rigidbody2D>();
                enemy.stun();
                enemy.hurt(attackDamage);

                if(controller.isFacingRight()){
                    enemyRb.AddForce(new Vector2(80f, 50f));
                }else{
                    enemyRb.AddForce(new Vector2(-80f, 50f));
                }
            }
        }
    }

    public void setCanCombo(int canCombo){
        if(canCombo == 1){
            this.canCombo = true;
        }else{
            this.canCombo = false;
        }
    }

    public void resetCombo(){
        combo = 0;
        basicMovement.attacking = false;
    }

    public void StopMovement(){
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
}
