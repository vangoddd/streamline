using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField]
    private Selectable selectable = null;

    public GameObject arrow;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectable.Select();
    }

    public void OnSelect(BaseEventData e){
        arrow.transform.position = transform.position;
    }
}
