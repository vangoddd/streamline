using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    public Loader.Scene nextScene;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 9){
            GameManager.Instance.LevelCompleted++;
            Loader.Load(nextScene);
        }
        
    }
}
