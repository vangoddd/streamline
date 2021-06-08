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

    private bool attack = false;
    private bool jump = false;
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        Reset();
    }

    void HandleInput(){
        //getting the movement keys
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed; 

        //handling attack
        if(Input.GetKeyDown(KeyCode.F)){
            Debug.Log("attacking");
            attack = true;
            animator.SetTrigger("Attack");
        }
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

    void Reset(){
        attack = false;
    }
}

