using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAtkScript : MonoBehaviour
{
    public LayerMask playerMask;
    public int dmg = 30;

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.layer == 9){
            col.GetComponentInParent<PlayerHealthScript>().hurt(dmg);
            Destroy(gameObject, 0f);
        }
    }
}
