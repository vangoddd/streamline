using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
     

public class PlayerHealthScript : MonoBehaviour
{       
    public int health;
    private int maxHealth;

    public int potCount = 0;
    public int healAmt = 50;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
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
        //##########################################

        if(Input.GetKeyDown(KeyCode.E)){
            if(potCount > 0){
                potCount--;
                health = (int) Mathf.Min((float)health + (float) healAmt, (float) maxHealth);
            }else{
                //play screen shake
            }

        }
    }

    public void hurt(int amt){
        health -= amt;
        if(health <= 0){
            die();
        }
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
