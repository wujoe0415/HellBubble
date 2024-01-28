using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    public Fade FadeCanvas;

    private void Awake()
    {
        Color color = FadeCanvas.FadeImage.color;
        color.a = 1;
        FadeCanvas.FadeImage.color = color;
        FadeCanvas.FadeOut();
    }
}
