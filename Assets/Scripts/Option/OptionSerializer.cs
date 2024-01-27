using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class OptionSerializer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text Content;
    private Option _option = null;
    public Action OnClick;
    public void SerializeOption(Option o)
    {
        _option = o;
        Content.text = _option.Content;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(name + " Game Object Entered!");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(name + " Game Object Exited!");
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        OnClick?.Invoke();
        Debug.Log(name + " Game Object Clicked!");
    }
}
