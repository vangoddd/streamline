using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Parallax : MonoBehaviour
{
    private float startpos;
    public float parallaxFactor;
    public GameObject cam;
 
    void Start()
    {
        startpos = transform.position.x;
    }
 
    void Update()
    {
        float distance = cam.transform.position.x * parallaxFactor;
        Vector3 newPosition = new Vector3(startpos + distance, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}