using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRoom : MonoBehaviour
{
    public GameObject barricade;

    void Update(){
        if(gameObject.transform.childCount == 0){
            barricade.SetActive(false);
        }
    }

    public void setBarricade(bool b){
        if(b){
            barricade.SetActive(true);
        }else{
            barricade.SetActive(false);
        }
    }
}
