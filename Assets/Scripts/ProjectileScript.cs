using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage = 20;
    public GameObject hitFx;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player") || col.gameObject.layer == 10 || col.gameObject.layer == 11){
            return;
        }
        if (col.gameObject.CompareTag("enemy")){
            EnemyScript e = col.gameObject.GetComponent<EnemyScript>();
            e.hurt(damage, true, 0.5f);
            AudioManager.playSound("player_shoothit");
        }else{
            AudioManager.playSound("player_shootmiss");
            Instantiate(hitFx, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
