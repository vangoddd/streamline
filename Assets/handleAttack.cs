using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleAttack : MonoBehaviour
{
    private Animator animator;
    private CharacterController2D controller;
    public int attackDamage = 20;

    public Transform attackCheck;
    public float attackRange;

    private BasicMovement basicMovement;
    
    public LayerMask enemyMask;

    private int combo = 0;
    private bool canCombo = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>(); 
        basicMovement = GetComponent<BasicMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !basicMovement.jump){
            

            //play the correct attack animation
            if(combo == 0){
                animator.SetTrigger("Attack");
                applyDamage();
                
            }else{
                if(canCombo){
                    animator.SetTrigger("Combo");
                    applyDamage();
                }
            }
            
            if(combo > 4){
                combo = 0;
            }
            
        }
    }

    void applyDamage(){
        basicMovement.canMove = false;
        combo++;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCheck.position, attackRange, enemyMask);

        foreach(Collider2D e in hitEnemies){
            //Should stun the enemy
            // Rigidbody2D enemyRb = e.gameObject.GetComponent<Rigidbody2D>();
            // if(controller.isFacingRight()){
            //     enemyRb.AddForce(new Vector2(350f, 100f));
            // }else{
            //     enemyRb.AddForce(new Vector2(-350f, 100f));
            // }

            //apply dmg
            e.GetComponent<EnemyScript>().hurt(attackDamage);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
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
    }
}
