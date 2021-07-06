using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HPRestore : MonoBehaviour
{
    private float originalY;
    public UnityEvent shakeEvent;
    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(new Vector3(
            transform.position.x,
            originalY + (0.1f * Mathf.Sin(Time.fixedTime * 3f)),
            transform.position.z
        ), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 9){
            col.GetComponentInParent<PlayerHealthScript>().addPotion();
            Destroy(gameObject, 0f);
            shakeEvent.Invoke();
        }
        
    }
}
