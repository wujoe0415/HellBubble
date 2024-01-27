using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public float GameDuration = 300f;
    private float _currentDuration = 0f;
    private bool _isPlaying = false;

    public JokeTeller Comedian;
    public ResponseSerializer Response;
    // public Choose Choice;
    private IEnumerator _coroutine;

    public void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        _isPlaying = true;
        Comedian.StartJoke();
        _coroutine = GameFlow();
        StartCoroutine(_coroutine);
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
        
    }
    private IEnumerator GameFlow()
    {
        Comedian.StartJoke();
        //yield return new WaitForSeconds(2f);
        // GenChoice
        // MakeChoice
        Response.StartResponse(10, 5);
        yield return null;
    }
}
