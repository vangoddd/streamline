using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenuController : MonoBehaviour
{
    public static bool isDead = false;

    public GameObject deadMenuUi;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void Resume(){
        deadMenuUi.SetActive(false);
        Time.timeScale = 1f;
        isDead = false;
    }

    public void Die(){
        deadMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isDead = true;
    }

    public void onRestart(){
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void onExit(){
        Resume();
        Loader.Load(Loader.Scene.Menu);
    }
}
