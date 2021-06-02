using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Animator animator;

    private bool attack = false;
    private bool jump = false;
    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        Move();
        HandleActions();

        Reset();
    }

    void HandleInput(){
        if(Input.GetKeyDown(KeyCode.F)){
            Debug.Log("attacking");
            attack = true;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("jumping");
            jump = true;
        }
    }

    void Move() { 
        float x = Input.GetAxisRaw("Horizontal"); 
        float moveBy = x * speed; 
        
        if (moveBy > 0 && !facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (moveBy < 0 && facingRight)
			{
				// ... flip the player.
				Flip();
			}

        rb.velocity = new Vector2(moveBy, rb.velocity.y); 
    }

    void HandleActions(){
        if(attack){
            Debug.Log("Setting the trigger attack");
            animator.SetTrigger("Attack");
        }

        if(jump){
            Debug.Log("Handling jump");
            rb.AddForce(new Vector2(0f, 200f));
        }
    }

    void Reset(){
        attack = false;
        jump = false;
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

