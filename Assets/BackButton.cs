using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void backToMenu(){
        AudioManager.playSound("ui_select");
        Loader.LoadNoLoading(Loader.Scene.Menu);
    }
}
