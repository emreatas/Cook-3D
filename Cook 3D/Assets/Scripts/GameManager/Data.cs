using JetBrains.Annotations;
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

        DontDestroyOnLoad(gameObject);
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
            SetCurrency(GetCurrency());
        }
    }
    public void SetCurrencyMenu(int value)
    {

        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency", 0) + value);

    }
    public int GetCurrency()
    {
        return PlayerPrefs.GetInt("Currency", 0);

    }
    #endregion

    #region Sound

    public void OnSetMusic(bool value)
    {

        if (value)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
        }

    }
    public bool GetMusic()
    {
        return (PlayerPrefs.GetInt("Music", 1) == 1) ? true : false;
    }


    public void OnSetSound(bool value)
    {

        if (value)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
        }

    }
    public bool GetSound()
    {
        return PlayerPrefs.GetInt("Sound", 1) == 1 ? true : false;
    }
    #endregion

    #region Haptic Feedback
    public void OnSetHapticFeedback(bool value)
    {

        if (value)
        {
            PlayerPrefs.SetInt("HapticFeedback", 1);
        }
        else
        {
            PlayerPrefs.SetInt("HapticFeedback", 0);
        }

    }
    public bool GetHapticFeedback()
    {
        return (PlayerPrefs.GetInt("HapticFeedback", 1) == 1) ? true : false;
    }

    #endregion

    #region Bonus

    public void SetBonusRewarded()
    {
        PlayerPrefs.SetInt("Bonus" + (GetCurrentLevel() + 1), 1);
    }
    public bool GetBonusRewarded()
    {
        return (PlayerPrefs.GetInt("Bonus" + (GetCurrentLevel() + 1), 0) == 1) ? true : false;
    }

    #endregion

}

