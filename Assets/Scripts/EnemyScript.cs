using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hurt(int amt){
        this.health -= amt;
        Debug.Log("Health : " + this.health);
        if(health <= 0){
            die();
        }
    }

    private void die(){
        Debug.Log("Die");
        Destroy(gameObject);
    }
}
