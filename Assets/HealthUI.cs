using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private PlayerHealthScript playerHealthScript;
    private Image uiImage;
    private float lastHealth, lastSmoothHealth;
    private float duration = 0.7f;
   //private float maxHealth;

    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealthScript>();
        lastHealth = playerHealthScript.health;
        uiImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealthScript.health != lastHealth){
            lastSmoothHealth = lastHealth;
            timer = duration;
        }

        if(timer > 0){
            timer -= Time.deltaTime;
            //uiImage.fillAmount = Mathf.Lerp(lastSmoothHealth / (float) playerHealthScript.getMaxHealth(), playerHealthScript.health / (float) playerHealthScript.getMaxHealth(), 1 - (timer / duration));
            uiImage.fillAmount = EaseOutQuad(lastSmoothHealth / (float) playerHealthScript.getMaxHealth(), playerHealthScript.health / (float) playerHealthScript.getMaxHealth(), 1 - (timer / duration));
        }else{
            uiImage.fillAmount = ((float)playerHealthScript.health / (float) playerHealthScript.getMaxHealth());
        }   

        lastHealth = playerHealthScript.health;
    }

    public  float EaseOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

}
