using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour
{
    private Button l1, l2, l3;
    private TextMeshProUGUI t1, t2, t3;
    public Color dColor;

    void Start()
    {
        l1 = GameObject.Find("Level 1").GetComponent<Button>();
        l2 = GameObject.Find("Level 2").GetComponent<Button>();
        l3 = GameObject.Find("Level 3").GetComponent<Button>();

        t1 = GameObject.Find("T1").GetComponent<TextMeshProUGUI>();
        t2 = GameObject.Find("T2").GetComponent<TextMeshProUGUI>();
        t3 = GameObject.Find("T3").GetComponent<TextMeshProUGUI>();

        if(GameManager.Instance.LevelCompleted == 0){
            l2.interactable = false;
            l3.interactable = false;
            t2.color = dColor;
            t3.color = dColor;
        }else if(GameManager.Instance.LevelCompleted == 1){
            l3.interactable = false;
            t3.color = dColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLv1(){
        AudioManager.playSound("ui_select");
        Loader.Load(Loader.Scene.Level1_Sewer);
    }
    public void LoadLv2(){
        AudioManager.playSound("ui_select");
        Loader.Load(Loader.Scene.Level2_Slums);
    }
    public void LoadLv3(){
        AudioManager.playSound("ui_select");
        Loader.Load(Loader.Scene.Level3_Suburbs);
    }
}
