using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
     

public class PlayerHealthScript : MonoBehaviour
{       
    public int health;
    private int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            die();
        }
        if(Input.GetKeyDown(KeyCode.O)){
            hurt(20);
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
}
