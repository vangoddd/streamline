using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public int bgmNumber;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.playMusic(bgmNumber);
    }

    
}
