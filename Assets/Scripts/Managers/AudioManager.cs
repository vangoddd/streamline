using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    private static GameObject oneShotGameObject;
    private static AudioSource audioSource;

    private static GameObject BGM;
    private static AudioSource bgmSource;

    public static void playSound(string playSound){
        if(oneShotGameObject == null){
            oneShotGameObject = new GameObject("OneshotSound");
            audioSource = oneShotGameObject.AddComponent<AudioSource>();

            audioSource.outputAudioMixerGroup = ResourceManager.Instance.SFX;
            oneShotGameObject.AddComponent<DontDestroy>();
        }
        audioSource.PlayOneShot(ResourceManager.Instance.sound[playSound]);
    }

    public static void playMusic(int bgMusic){
        //If trying to play the same song (Song alrd playing)
        if(BGM != null && (BGM.GetComponent<AudioSource>().clip == ResourceManager.Instance.bgm[bgMusic] )){
            return;
        }

        //if songs already playing
        if(BGM != null){
            Object.Destroy(BGM); 
        }
        BGM = new GameObject("BGM");
        bgmSource = BGM.AddComponent<AudioSource>();
        bgmSource.outputAudioMixerGroup = ResourceManager.Instance.music;
        BGM.AddComponent<DontDestroy>();
        bgmSource.clip = (AudioClip) ResourceManager.Instance.bgm[bgMusic];
        bgmSource.Play();
    }

    // **********************************************************************************************//

    public static void changeSFXVol(){
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("sfx", GameManager.Instance.sfxVolume);
    }
    public static void changeMusicVol(){
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("music", GameManager.Instance.musicVolume);
    }
    public static void changeMasterVol(){
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("master", GameManager.Instance.masterVolume);
    }

    
}
