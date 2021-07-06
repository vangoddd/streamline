using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAtkScript : MonoBehaviour
{
    public LayerMask playerMask;
    public int dmg = 30;
    private PlayerHealthScript playerHealthScript;

    private bool hit = false;

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.layer == 9){
            playerHealthScript = col.GetComponentInParent<PlayerHealthScript>();
            hit = true;
            //Destroy(gameObject, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.layer == 9){
            hit = false;
            //Destroy(gameObject, 0f);
        }
    }

    public void applyDmg(){
        if(hit){
            playerHealthScript.hurt(dmg);
            Destroy(gameObject, 0f);
        }
        
    }
}
