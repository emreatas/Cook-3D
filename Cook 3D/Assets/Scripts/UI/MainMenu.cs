using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI leveltext;

    void Start()
    {
        leveltext.text = "Level " + (Data.instance.GetCurrentLevel() + 1).ToString();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

}
