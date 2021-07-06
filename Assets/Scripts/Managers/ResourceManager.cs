using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;

    public static ResourceManager Instance{
        get{
            if(_instance == null){
                // GameObject go = new GameObject("ResourceManager");
                GameObject go = Instantiate(Resources.Load("ResourceManager")) as GameObject;

            }

            return _instance;
        }
    }

    private Object[] sfx;

    public Dictionary<string, AudioClip> sound = new Dictionary<string, AudioClip>();
    public AudioMixerGroup SFX, music, master;

    void Awake(){
        DontDestroyOnLoad(gameObject);
        _instance = this;
        sfx = Resources.LoadAll("Audio/SFX", typeof(AudioClip));

        foreach(AudioClip ac in sfx){
            sound.Add(ac.name, ac);
        }

        Debug.Log(SFX);
    }

}
