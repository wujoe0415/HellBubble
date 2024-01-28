using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    public static bool IsEndDialog = false;
    public static bool IsEndChoice = false;
    public static bool IsEndGame = false;


    public float GameDuration = 300f;
    private float _currentDuration = 0f;
    private bool _isPlaying = false;

    public JokeTeller Comedian;
    public ReactionSerializer Response;
    public Alarm AlarmManager;

    public UnityEvent OnStartGame;
    public UnityEvent OnHaltGame;
    public UnityEvent OnEndGame;

    // public Choose Choice;
    private IEnumerator _coroutine;
    public void OnEnable()
    {
        IsEndGame = false;
        IsEndDialog = false;
    }
    public void Start()
    {
        StartGame();
        OnEndGame.AddListener(() => { IsEndGame = true; });
    }
    public void StartGame()
    {
        _isPlaying = true;
        AlarmManager.SetMaxValue(GameDuration);
        OnStartGame.Invoke();
        _coroutine = GameFlow();
        StartCoroutine(_coroutine);
    }
    private void Update()
    {
        if (!_isPlaying)
            return;
        _currentDuration += Time.deltaTime;
        AlarmManager.SetAlarmValue(_currentDuration);
        if (_currentDuration > GameDuration)
        {
            _isPlaying = false;
            Debug.LogWarning("Exceed!!!!!");
            OnHaltGame.Invoke();
        }
    }
    
    private IEnumerator GameFlow()
    {
        WaitForSeconds blank = new WaitForSeconds(1.5f);
        while (!IsEndGame)
        {
            Comedian.StartJoke();

            while (!IsEndDialog)
                yield return null;
            IsEndDialog= false;

            // MakeChoice
            while (!IsEndChoice)
                yield return null;
            IsEndChoice= false;
            Response.StartReaction(10, 5);
            yield return blank;
        }
    }
}
