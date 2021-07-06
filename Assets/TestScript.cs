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
            Debug.Log("changing scene");
            GameManager.Instance.LevelCompleted += 1;
            Loader.Load(Loader.Scene.Level2_Slums);
        }

        if(Input.GetKeyDown(KeyCode.B)){
            Debug.Log(GameManager.Instance.LevelCompleted);
        }
        if(Input.GetKeyDown(KeyCode.V)){
            GameManager.Instance.LevelCompleted += 1;
        }
    }
}
