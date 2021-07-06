using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiController : MonoBehaviour
{
    private Image image;
    public Sprite cooldown, ready;
    private HandleDash handleDash;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        handleDash = GameObject.Find("Player").GetComponent<HandleDash>();
    }

    // Update is called once per frame
    void Update()
    {
        if(handleDash.isCooldown()){
            image.sprite = cooldown;
        }else{
            image.sprite = ready;
        }
    }
}
