using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingScript : MonoBehaviour
{
    private GameObject arrow1, arrow2;
    void Start()
    {
        arrow1 = GameObject.Find("SelectionArrow1");
        arrow2 = GameObject.Find("SelectionArrow2");
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.FloorToInt(Time.time) % 2 == 0){
            arrow1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120f, 0);
            arrow2.GetComponent<RectTransform>().anchoredPosition = new Vector2(120f, 0);
        }else{
            arrow1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-110f, 0);
            arrow2.GetComponent<RectTransform>().anchoredPosition = new Vector2(110f, 0);
        }
    }
}
