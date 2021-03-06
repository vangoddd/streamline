using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandleDash : MonoBehaviour
{
    private Animator animator;
    private CharacterController2D controller;
    private Rigidbody2D rb;
    private BasicMovement basicMovement;

    public GameObject dashFx;

    public int skillCooldown = 7;
    private float skillTimer = 0f;

    public UnityEvent shakeEvent;
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
        if(skillTimer > 0f) skillTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift) && !basicMovement.attacking){
            if(skillTimer <= 0f){
                Debug.Log("Activating skill");
                
                skillTimer = skillCooldown;
                Dash();
            }else{
                shakeEvent.Invoke();
                Debug.Log("Skill on CD for" + skillTimer);
            }
            
        }
    }

    void Dash(){
        CameraShake.Instance.shakeCamera(1f, 0.2f);
        basicMovement.StopMovementBasic(true);
        if(basicMovement.horizontalMove >= 0.01f){
            Instantiate(dashFx, transform.position, Quaternion.Euler(0f, 0f, 0f)); 
            rb.velocity = (new Vector2(25f, rb.velocity.y));
        }else if(basicMovement.horizontalMove >= 0.01f){
            Instantiate(dashFx, transform.position, Quaternion.Euler(0f, 180f, 0f)); 
            rb.velocity = (new Vector2(-25f, rb.velocity.y));
        }else{
            if(controller.isFacingRight()){
                Instantiate(dashFx, transform.position, Quaternion.Euler(0f, 0f, 0f)); 
                rb.velocity = (new Vector2(25f, rb.velocity.y));
            }else{
                Instantiate(dashFx, transform.position, Quaternion.Euler(0f, 180f, 0f)); 
                rb.velocity = (new Vector2(-25f, rb.velocity.y));
            }
        }

        AudioManager.playSound("player_dash");
    }

    public bool isCooldown(){
        if(skillTimer > 0){
            return true;
        }else{
            return false;
        }
    }

    public float getSkillCooldown(){
        return skillTimer;
    }
}
