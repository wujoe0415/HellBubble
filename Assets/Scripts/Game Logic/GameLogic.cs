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


    public float GameDuration = 180f;
    private float _currentDuration = 0f;
    private bool _isPlaying = false;

    public JokeTeller Comedian;
    public Alarm AlarmManager;
    public BarSystem Bar;

    public UnityEvent OnStartGame;
    public UnityEvent OnHaltGame;
    public UnityEvent OnEndGame;

    private int _currentRound = 0;
    private int _maxRound = 7;

    // public Choose Choice;
    private IEnumerator _coroutine;
    public void OnEnable()
    {
        IsEndGame = false;
        IsEndDialog = false;
}
    public void Start()
    {
        Invoke("StartGame", 2.2f);
        OnEndGame.AddListener(() => { IsEndGame = true; });
    }
    public void StartGame()
    {
        _currentRound = 0;
        _maxRound = 7;
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
        bool isHell = false;
        while (!IsEndGame && _maxRound > _currentRound )
        {
            if (!(Bar.GoodCurrentValue > 0) || Bar.GoodCurrentValue < Bar.BadCurrentValue)
            {
                isHell = true;
                break;
            }
            Comedian.StartJoke();
            while (!IsEndDialog)
                yield return null;
            IsEndDialog= false;

            // MakeChoice
            while (!IsEndChoice)
                yield return null;
            IsEndChoice= false;
            yield return blank;
            _currentRound++;
        }
        
        if(!isHell)
            OnEndGame.Invoke();
        else
            OnHaltGame.Invoke();
    }
}
