using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public CharacterController2D controller;
    float horizontalMove = 0f;

    Rigidbody2D rb;
    public float speed;
    public Animator animator;
    
    public LayerMask enemyMask;

    public bool canMove = true;
    public bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate(){
        if(canMove){
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        }else{
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    void HandleInput(){
        //getting the movement keys
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed; 

        //handling jump
        if(Input.GetKeyDown(KeyCode.Space)){
            jump = true;
            animator.SetBool("isJumping", true);
        }
        
    }

    public void onLanding(){
        Debug.Log("Landing event called");
        animator.SetBool("isJumping", false);
        jump = false;
    }

    public void setCanMove(int canMove){
        if(canMove == 1){
            this.canMove = true;
        }else{
            this.canMove = false;
        }
    }

    
}

