using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public int bgmNumber;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.stopBossMusic();
        AudioManager.playMusic(bgmNumber);
    }

    public void bossMusic(){
        AudioManager.playBossMusic(bgmNumber+1);
    }
    
}
