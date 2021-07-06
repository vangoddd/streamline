using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUi;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    void Resume(){
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause(){
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void onResume(){
        Resume();
    }

    public void onRestart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void onExit(){
        Time.timeScale = 1f;
        Loader.Load(Loader.Scene.Menu);
    }
}
