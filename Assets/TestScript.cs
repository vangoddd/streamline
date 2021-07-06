using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.playMusic(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)){
        }

        if(Input.GetKeyDown(KeyCode.B)){
           AudioManager.playMusic(1);
        }
        if(Input.GetKeyDown(KeyCode.V)){
            AudioManager.playMusic(0);
        }
    }
}
