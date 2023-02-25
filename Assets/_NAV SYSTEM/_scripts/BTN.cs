using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BTN : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onButtonClick;

    public void OnPointerClick(PointerEventData eventData)
    {
    onButtonClick.Invoke();
    }
}
