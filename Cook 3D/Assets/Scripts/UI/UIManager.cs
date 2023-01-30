using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    LevelManager levelManager;

    [SerializeField]
    OrderUI orderUiPrefab;
    [SerializeField]
    Transform orderUiParent;

    public TextMeshProUGUI levelText;
    public GameObject levelWinPanel;
    public GameObject levelLosePanel;
    private void OnEnable()
    {
        GameManager.LevelStart += GameManager_LevelStart;
        GameManager.LevelLose += GameManager_LevelLose;
        GameManager.LevelWin += GameManager_LevelWin;
        GameManager.NextLevel += GameManager_NextLevel;
        GameManager.RetryLevel += GameManager_RetryLevel;
    }

    private void GameManager_RetryLevel()
    {
        GameManager.instance.OnLevelStart();
    }

    private void GameManager_NextLevel()
    {
        GameManager.instance.OnLevelStart();
    }
    private void GameManager_LevelStart()
    {
        levelText.text = "Level" + (Data.instance.GetCurrentLevel() + 1).ToString();

        foreach (Transform go in orderUiParent)
        {
            if (go != null)
            {
                Destroy(go.gameObject);

            }
        }

        for (int i = 0; i < levelManager.levels[Data.instance.GetCurrentLevel()].orders.Count; i++)
        {
            OrderUI o = Instantiate(orderUiPrefab, orderUiParent);
            o.orderSprite.sprite = levelManager.levels[Data.instance.GetCurrentLevel()].orders[i].vegitable.vegitableImage;
            o.orderText.text = "x" + levelManager.levels[Data.instance.GetCurrentLevel()].orders[i].count.ToString();
        }
    }
    private void GameManager_LevelWin()
    {
        levelWinPanel.SetActive(true);
    }

    private void GameManager_LevelLose()
    {
        levelLosePanel.SetActive(true);
    }

    public void NextLevel()
    {
        GameManager.instance.OnNextLevel();
    }
    public void RestartLevel()
    {
        GameManager.instance.OnRetryLevel();
    }
    private void OnDisable()
    {
        GameManager.LevelStart -= GameManager_LevelStart;
        GameManager.LevelLose -= GameManager_LevelLose;
        GameManager.LevelWin -= GameManager_LevelWin;
        GameManager.NextLevel -= GameManager_NextLevel;
        GameManager.RetryLevel -= GameManager_RetryLevel;


    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }



}
