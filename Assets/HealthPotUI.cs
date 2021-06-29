using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotUI : MonoBehaviour
{
    private Image image;
    public Sprite one, two, three;
    public int potCount = 3;

    public PlayerHealthScript playerHealthScript;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        potCount = playerHealthScript.potCount;
        if(potCount == 0){
            image.color = new Color(1f, 1f, 1f, 0f);
        }else{
            image.color = new Color(1f, 1f, 1f, 1f);
        }
        if(potCount == 3){
            image.sprite = three;
        }else if(potCount == 2){
            image.sprite = two;
        }else if(potCount == 1){
            image.sprite = one;
        }
    }
}
