using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public void loadOption(){
        AudioManager.playSound("ui_select");
        Loader.LoadNoLoading(Loader.Scene.Option);
    }
}
