using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeOptionScript : MonoBehaviour
{
    private Slider SFXSlider, musicSlider, masterSlider;
    private TextMeshProUGUI master, music, sfx;

    void Start()
    {
        SFXSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        masterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();

        master = GameObject.Find("MasterText").GetComponent<TextMeshProUGUI>();
        music = GameObject.Find("MusicText").GetComponent<TextMeshProUGUI>();
        sfx = GameObject.Find("SFXText").GetComponent<TextMeshProUGUI>();

        SFXSlider.value = GameManager.Instance.sfxVolume;
        musicSlider.value = GameManager.Instance.musicVolume;
        masterSlider.value = GameManager.Instance.masterVolume;

        sfx.text = convertToNormal(GameManager.Instance.sfxVolume) + "";
        music.text = convertToNormal(GameManager.Instance.musicVolume) + "";
        master.text = convertToNormal(GameManager.Instance.masterVolume) + "";

    }

    public void onSFXVolumeChange(float vol){
        GameManager.Instance.sfxVolume = vol;
        sfx.text = convertToNormal(vol) + "";
        AudioManager.changeSFXVol();
    }

    public void onMusicVolumeChange(float vol){
        GameManager.Instance.musicVolume = vol;
        music.text = convertToNormal(vol) + "";
        AudioManager.changeMusicVol();
    }

    public void onMasterVolumeChange(float vol){
        GameManager.Instance.masterVolume = vol;
        master.text = convertToNormal(vol) + "";
        AudioManager.changeMasterVol();
    }

    private int convertToNormal(float f){
        return Mathf.RoundToInt(Mathf.InverseLerp(-80f, 0f, f) * 100);
    }
}
