using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject hitFx;
    public GameObject dieFx;

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
        }else{
            Instantiate(hitFx, transform.position, Quaternion.identity);
        }
    }

    private void die(){
        Debug.Log("Die");
        Instantiate(dieFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
