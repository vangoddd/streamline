using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealthScript playerHealthScript;
    private Image uiImage;
    // Start is called before the first frame update
    void Start()
    {
        uiImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        uiImage.fillAmount = ((float)playerHealthScript.health / (float)playerHealthScript.getMaxHealth());
    }
}
