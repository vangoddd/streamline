using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private static GameManager _instance;

   public static GameManager Instance{
       get{
           if(_instance == null){
               GameObject go = new GameObject("GameManager");
               go.AddComponent<GameManager>();
           }

           return _instance;
       }
   }

   public int LevelCompleted {get; set;}

   public float sfxVolume {get; set;}
   public float musicVolume {get; set;}
   public float masterVolume {get; set;}

   void Awake(){
       DontDestroyOnLoad(gameObject);
       _instance = this;
   }

   void Start(){
       LevelCompleted = 0;

       sfxVolume = 0f;
       musicVolume = 0f;
       masterVolume = 0f;
   }
}
