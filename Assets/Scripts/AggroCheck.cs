using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroCheck : MonoBehaviour
{
    public Transform aggroCheck;
    public float aggroRange = 15f;
    public LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Scans for the enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(aggroCheck.position, aggroRange, enemyMask);

            //Do Aggro on enemy
            foreach(Collider2D e in hitEnemies){
                //
                e.GetComponent<EnemyScript>().aggro();
            }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(aggroCheck.position, aggroRange);
    }
}
