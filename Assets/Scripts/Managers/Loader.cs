using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        Level1_Sewer,
        Level2_Slums,
        Level3_Suburbs,
        Option,
        Menu,
        LevelSelect,
    }

    public static void Load(Scene scene){
        SceneManager.LoadScene(scene.ToString());
    }
}
