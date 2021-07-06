using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;

    public static ResourceManager Instance{
        get{
            if(_instance == null){
                GameObject go = new GameObject("ResourceManager");
                go.AddComponent<ResourceManager>();
            }

            return _instance;
        }
    }

    private Object[] sfx;

    public Dictionary<string, AudioClip> sound = new Dictionary<string, AudioClip>();

    void Awake(){
        DontDestroyOnLoad(gameObject);
        _instance = this;
        sfx = Resources.LoadAll("Audio/SFX", typeof(AudioClip));

        foreach(AudioClip ac in sfx){
            sound.Add(ac.name, ac);
        }
    }

}
