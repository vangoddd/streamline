using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
     

public class PlayerHealthScript : MonoBehaviour
{       
    public int health;
    private int maxHealth;

    public UnityEvent shakeEvent;

    public int potCount = 0;
    public int healAmt = 50;

    public float hitImmunityTime = 2f;
    public float immuneTimer = 0f;

    public GameObject healFX;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging code ###########################
        if(Input.GetKeyDown(KeyCode.P)){
            die();
        }
        if(Input.GetKeyDown(KeyCode.O)){
            hurt(20);
        }
        if(Input.GetKeyDown(KeyCode.U)){
            health += 20;
        }
        //##########################################

        //Handle healing
        if(Input.GetKeyDown(KeyCode.E)){
            if(potCount > 0){
                potCount--;
                health = (int) Mathf.Min((float)health + (float) healAmt, (float) maxHealth);

                Instantiate(healFX, transform.position, Quaternion.identity, gameObject.transform);
            }else{
                shakeEvent.Invoke();
            }
        }
        
        //handle immunity
        if(immuneTimer > 0){
            immuneTimer -= Time.deltaTime;
            if(Mathf.FloorToInt(immuneTimer * 5) % 2 == 0){
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }else{
                spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            }
        }else{
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void hurt(int amt){
        if(immuneTimer > 0){
            return;
        }
        CameraShake.Instance.shakeCamera(4f, 0.2f);
        immuneTimer = hitImmunityTime;
        health -= amt;
        if(health <= 0){
            die();
        }

        shakeEvent.Invoke();
    }

    public void die(){
        Debug.Log("player died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int getMaxHealth(){
        return maxHealth;
    }

    public void addPotion(){
        if(potCount<3){
            potCount++;
        }
        
    }
}
