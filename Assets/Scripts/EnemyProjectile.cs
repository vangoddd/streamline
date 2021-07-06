using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 30;
    public GameObject hitFx;

    public void Start(){
        AudioManager.playSound("enemy_shoot");
    }

    public void setDamage(int dmg){
        damage = dmg;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.layer == 10 || col.gameObject.CompareTag("enemy")){
            return;
        }
        if (col.gameObject.layer == 6){
            PlayerHealthScript p = col.gameObject.GetComponentInParent<PlayerHealthScript>();
            p.hurt(damage);
            Instantiate(hitFx, transform.position, Quaternion.identity);
            AudioManager.playSound("enemy_shoothit");

        }else if(col.gameObject.layer == 7){
            Instantiate(hitFx, transform.position, Quaternion.identity);
        }else{
            return;
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
