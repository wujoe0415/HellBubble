using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource EnterAudio;
    public AudioSource ExitAudio;
    public void OnPointerEnter(PointerEventData eventData)
    {
        EnterAudio.Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ExitAudio.Play();
    }
}