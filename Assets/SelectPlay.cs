using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Selectable>().Select();
    }

    public void LoadGame(){
        Loader.Load(Loader.Scene.LevelSelect);
    }

}
