using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = GameManager.Instance;
        Destroy(gameObject, 0f);
    }


}
