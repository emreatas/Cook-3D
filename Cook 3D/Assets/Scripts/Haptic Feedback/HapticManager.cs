using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticManager : MonoBehaviour
{
    bool _hapticActive;

    private void Start()
    {
        _hapticActive = Data.instance.GetHapticFeedback();
    }
    private void OnEnable()
    {
        GameManager.MealFinish += GameManager_MealFinish;
        GameManager.LevelLose += GameManager_LevelLose;
        GameManager.LevelWin += GameManager_LevelWin;
    }

    private void GameManager_LevelWin()
    {
        Feedback();
    }

    private void GameManager_LevelLose()
    {
        Feedback();
    }

    private void GameManager_MealFinish()
    {
        Feedback();
    }

    void Feedback()
    {
        if (_hapticActive)
        {
            Vibrator.Vibrate();
        }
    }

    private void OnDisable()
    {
        GameManager.MealFinish -= GameManager_MealFinish;
        GameManager.LevelLose -= GameManager_LevelLose;
        GameManager.LevelWin -= GameManager_LevelWin;


    }
}
