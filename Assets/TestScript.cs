using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)){
            AudioManager.playSound("enemy_deathbig2");

        }

        if(Input.GetKeyDown(KeyCode.B)){
           AudioManager.playSound("enemy_deathbig1");
        }
        if(Input.GetKeyDown(KeyCode.V)){
            
        }
    }
}
