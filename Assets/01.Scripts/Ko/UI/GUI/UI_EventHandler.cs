using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    public Action<PointerEventData, Transform> OnClickHandler;
    public Action<PointerEventData, Transform> OnDownHandler;
    public Action<PointerEventData, Transform> OnMoveHandler;
    public Action<PointerEventData, Transform> OnUpHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke(eventData, transform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDownHandler?.Invoke(eventData, transform);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        OnMoveHandler?.Invoke(eventData, transform);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUpHandler?.Invoke(eventData, transform);
    }
}
