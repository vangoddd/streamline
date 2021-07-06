using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private Image loadingImage;
    void Start()
    {
        loadingImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        loadingImage.fillAmount = Loader.getLoadingProgress();
    }
}
