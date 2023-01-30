using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Data : MonoBehaviour
{
    #region Singleton
    public static Data instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region Level
    public void SetCurrentLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 0) + 1);
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel", 0);
    }
    #endregion

    #region Currency
    public static event Action<int> SetCurrency;
    public void OnSetCurrency(int value)
    {
        if (SetCurrency != null)
        {
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency", 0) + value);
            SetCurrency(value);
        }
    }
    public int GetCurrency()
    {
        return PlayerPrefs.GetInt("Currency", 0);

    }
    #endregion
}

