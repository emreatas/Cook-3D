using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TimerType _timerType;

    private float timeToDisplay;
    private bool _isRunning;


    private void OnEnable()
    {
        GameManager.TimerStart += GameManager_TimerStart;
        GameManager.TimerStop += GameManager_TimerStop;
        GameManager.TimerUpdate += GameManager_TimerUpdate;
    }
    private void OnDisable()
    {
        GameManager.TimerStart -= GameManager_TimerStart;
        GameManager.TimerStop -= GameManager_TimerStop;
        GameManager.TimerUpdate -= GameManager_TimerUpdate;
    }


    private void GameManager_TimerStart()
    {
        _isRunning = true;
    }
    private void GameManager_TimerStop()
    {
        _isRunning = false;
    }

    private void GameManager_TimerUpdate(float value)
    {
        timeToDisplay = 0;
        timeToDisplay += value;
    }
    private void Update()
    {
        if (!_isRunning) { return; }

        if (_timerType == TimerType.Countdown && timeToDisplay < 0.0f)
        {
            GameManager.instance.OnTimerStop();
            GameManager.instance.OnLevelLose();
            return;
        }

        timeToDisplay += _timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timespan = TimeSpan.FromSeconds(timeToDisplay);
        _timerText.text = timespan.ToString(@"mm\:ss");
    }




}

public enum TimerType
{
    Countdown,
    Stopwatch
}