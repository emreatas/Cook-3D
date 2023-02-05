using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region Level Management
    public static event Action LevelWin;
    public void OnLevelWin()
    {
        if (LevelWin != null)
        {
            OnTimerStop();
            Data.instance.SetCurrentLevel();
            LevelWin();
        }
    }

    public static event Action LevelLose;
    public void OnLevelLose()
    {
        if (LevelLose != null)
        {
            LevelLose();
        }
    }
    public static event Action LevelStart;
    public void OnLevelStart()
    {
        if (LevelStart != null)
        {
            LevelStart();
        }
    }
    public static event Action NextLevel;
    public void OnNextLevel()
    {
        if (NextLevel != null)
        {
            NextLevel();
        }
    }
    public static event Action RetryLevel;
    public void OnRetryLevel()
    {
        if (RetryLevel != null)
        {
            RetryLevel();
        }
    }
    public static event Action<int> NextMeal;
    public void OnNextMeal(int meal)
    {
        if (NextMeal != null)
        {
            NextMeal(meal);
        }
    }
    public static event Action MealFinish;
    public void OnMealFinish()
    {
        if (MealFinish != null)
        {
            MealFinish();
        }
    }
    public static event Action ContinueLevel;
    public void OnContinueLevel()
    {
        if (ContinueLevel != null)
        {
            ContinueLevel();
        }
    }
    #endregion

    #region Timer
    public static event Action TimerStart;
    public void OnTimerStart()
    {
        if (TimerStart != null)
        {
            TimerStart();
        }
    }

    public static event Action TimerStop;
    public void OnTimerStop()
    {
        if (TimerStop != null)
        {
            TimerStop();
        }
    }

    public static event Action<float> TimerUpdate;
    public void OnTimerUpdate(float time)
    {
        if (TimerUpdate != null)
        {
            TimerUpdate(time);
        }
    }
    #endregion

    #region Game

    public static event Action<GameObject> DoubleClick;
    public void OnDoubleClick(GameObject go)
    {
        if (DoubleClick != null)
        {
            DoubleClick(go);
        }
    }


    #endregion
}
