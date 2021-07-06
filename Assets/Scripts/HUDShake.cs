using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDShake : MonoBehaviour
{
    private RectTransform rectTransform;
    float offset = 0f;
    private float shakeTimer = 0f;
    private float shakeDuration = 0f;
    private float amp = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //shakeHUD(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            shakeHUD(0.5f);
        }
        shakeTimer -= Time.deltaTime;
        if(shakeTimer >= 0f){
            float amplitude = Mathf.Lerp(amp, 0f, 1 - (shakeTimer / shakeDuration));
            float shakeX = amplitude * Mathf.Sin(Time.time * 80f);
            float shakeY = amplitude * Mathf.Cos(Time.time * 60f);
            rectTransform.anchoredPosition = new Vector2(shakeX, shakeY);
        }
        
    }

    public void shakeHUD(float duration){
        shakeDuration = duration;
        shakeTimer = duration;
    }
}
