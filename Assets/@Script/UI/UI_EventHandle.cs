using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandle : MonoBehaviour, IPointerClickHandler
{
    public event Action OnClickHandler = null;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke();
    }
}
