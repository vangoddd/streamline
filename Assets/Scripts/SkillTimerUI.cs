using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTimerUI : MonoBehaviour
{
    private Text cooldownText;
    public HandleDash handleDash;
    // Start is called before the first frame update
    void Start()
    {
        cooldownText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(handleDash.isCooldown()){
            cooldownText.text = (Mathf.Ceil(handleDash.getSkillCooldown())) + "";
        }else{
            cooldownText.text = "";
        }
    }
}
