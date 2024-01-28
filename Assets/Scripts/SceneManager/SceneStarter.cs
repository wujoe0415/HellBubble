using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    public Fade FadeCanvas;

    private void Awake()
    {
        FadeCanvas.FadeOut();
    }
}
