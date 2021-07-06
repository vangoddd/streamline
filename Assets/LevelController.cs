using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private Button l1, l2, l3;
    void Start()
    {
        l1 = GameObject.Find("Level 1").GetComponent<Button>();
        l2 = GameObject.Find("Level 2").GetComponent<Button>();
        l3 = GameObject.Find("Level 3").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLv1(){
        Loader.Load(Loader.Scene.Level1_Sewer);
    }
    public void LoadLv2(){
        Loader.Load(Loader.Scene.Level2_Slums);
    }
    public void LoadLv3(){
        Loader.Load(Loader.Scene.Level3_Suburbs);
    }
}
