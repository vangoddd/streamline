using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeOptionScript : MonoBehaviour
{
    private Slider slider;
    private float curVol;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.Instance.sfxVolume;
        curVol = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onVolumeChange(float vol){
        curVol = vol;
        GameManager.Instance.sfxVolume = curVol;
        AudioManager.changeSFXVol();
    }
}
