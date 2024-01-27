using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public float GameDuration = 300f;
    private float _currentDuration = 0f;
    private bool _isPlaying = false;

    public JokeTeller Comedian;
    // public Choose Choice;

    public void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        _isPlaying = true;
        Comedian.StartJoke();
    }
    private void Update()
    {
        if (!_isPlaying)
            return;
        _currentDuration += Time.deltaTime;

        if (_currentDuration > GameDuration)
        {
            _isPlaying = false;
            Debug.LogWarning("Exceed!!!!!");
        }
    }
    public void MakeChoice()
    {
        // Response
        Comedian.StartJoke();
    }
}
