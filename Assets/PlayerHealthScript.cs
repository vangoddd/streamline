using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
     

public class PlayerHealthScript : MonoBehaviour
{       
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
