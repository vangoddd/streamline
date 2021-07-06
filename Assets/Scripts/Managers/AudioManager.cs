using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    private static GameObject oneShotGameObject;
    private static AudioSource audioSource;

    public static void playSound(string playSound){
        if(oneShotGameObject == null){
            oneShotGameObject = new GameObject("OneshotSound");
            audioSource = oneShotGameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.1f;
            oneShotGameObject.AddComponent<DontDestroy>();
        }
        audioSource.PlayOneShot(ResourceManager.Instance.sound[playSound]);
    }
}
