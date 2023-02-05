using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    LevelManager levelManager;

    [SerializeField]
    OrderUI orderUiPrefab;
    [SerializeField]
    Transform orderUiParent;


    [Header("Level referances")]
    public TextMeshProUGUI levelText;
    public GameObject levelWinPanel;
    public GameObject levelLosePanel;
    public Button levelContinueButton;

    [Header("Coin Referances")]
    public TextMeshProUGUI currencyText;
    public Transform panpos;
    public Transform coinpos;
    public Image coinImage;
    public GameObject canvas;
    public AnimationCurve coinCurve;

    [Header("Button Referances")]
    public Button NextLevelButton;
    public TextMeshProUGUI winTextReferances;

    private void OnEnable()
    {
        GameManager.LevelStart += GameManager_LevelStart;
        GameManager.LevelLose += GameManager_LevelLose;
        GameManager.LevelWin += GameManager_LevelWin;
        GameManager.NextLevel += GameManager_NextLevel;
        GameManager.RetryLevel += GameManager_RetryLevel;
        GameManager.NextMeal += GameManager_NextMeal;
        GameManager.MealFinish += GameManager_MealFinish;
        GameManager.ContinueLevel += GameManager_ContinueLevel;

        Data.SetCurrency += Data_SetCurrency;
    }

    private void GameManager_ContinueLevel()
    {
        StartCoroutine(ContinueLevel());

        GameManager.instance.OnTimerUpdate(30);
        GameManager.instance.OnTimerStart();
    }

    private void Start()
    {
        if (levelManager.levels.Count <= Data.instance.GetCurrentLevel())
        {
            levelWinPanel.SetActive(true);
            winTextReferances.text = "You have completed all the chapters.";
            return;
        }
        currencyText.text = "x" + Data.instance.GetCurrency();
    }

    private void Data_SetCurrency(int obj)
    {
        currencyText.text = "x" + obj;
    }

    private void GameManager_RetryLevel()
    {

        GameManager.instance.OnLevelStart();


    }

    private void GameManager_NextLevel()
    {
        GameManager.instance.OnLevelStart();
    }
    private void GameManager_NextMeal(int obj)
    {
        levelText.text = "Level" + (Data.instance.GetCurrentLevel() + 1).ToString();

        foreach (Transform go in orderUiParent)
        {
            if (go != null)
            {
                Destroy(go.gameObject);

            }
        }
        if (levelManager.levels[Data.instance.GetCurrentLevel()].meals.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < levelManager.levels[Data.instance.GetCurrentLevel()].meals[obj].orders.Count; i++)
        {
            OrderUI o = Instantiate(orderUiPrefab, orderUiParent);
            o.orderSprite.sprite = levelManager.levels[Data.instance.GetCurrentLevel()].meals[obj].orders[i].vegitable.vegitableImage;
            o.orderText.text = "x" + levelManager.levels[Data.instance.GetCurrentLevel()].meals[obj].orders[i].count.ToString();
        }
    }

    private void GameManager_MealFinish()
    {
        StartCoroutine(MealFinish());
        currencyText.text = "x" + Data.instance.GetCurrency();
    }

    private void GameManager_LevelStart()
    {
        levelWinPanel.SetActive(false);
        levelLosePanel.SetActive(false);
    }
    private void GameManager_LevelWin()
    {
        StartCoroutine(MealFinish());
        StartCoroutine(WinAnim());
    }

    private void GameManager_LevelLose()
    {
        levelLosePanel.SetActive(true);
        AudioManager.instance.Play("Lose");

        if (Data.instance.GetCurrency() >= 5)
        {
            levelContinueButton.interactable = true;
        }
        else
        {
            levelContinueButton.interactable = false;
        }
    }

    public void NextLevel()
    {
        if (levelManager.levels.Count <= Data.instance.GetCurrentLevel())
        {
            NextLevelButton.interactable = false;
            winTextReferances.text = "You have completed all the chapters.";
            return;
        }
        GameManager.instance.OnNextLevel();
    }
    public void RestartLevel()
    {
        GameManager.instance.OnRetryLevel();
    }
    public void LevelContinue()
    {
        GameManager.instance.OnContinueLevel();
    }
    private void OnDisable()
    {
        GameManager.LevelStart -= GameManager_LevelStart;
        GameManager.LevelLose -= GameManager_LevelLose;
        GameManager.LevelWin -= GameManager_LevelWin;
        GameManager.NextLevel -= GameManager_NextLevel;
        GameManager.RetryLevel -= GameManager_RetryLevel;
        GameManager.NextMeal -= GameManager_NextMeal;
        GameManager.MealFinish -= GameManager_MealFinish;
        GameManager.ContinueLevel -= GameManager_ContinueLevel;



        Data.SetCurrency -= Data_SetCurrency;

    }

    public IEnumerator WinAnim()
    {
        yield return new WaitForSeconds(.5f);
        levelWinPanel.SetActive(true);
        levelWinPanel.transform.localScale = Vector3.zero;
        levelWinPanel.transform.DOScale(Vector3.one, .3f).SetEase(Ease.InCirc);
    }
    public IEnumerator MealFinish()
    {
        for (int j = 0; j < 10; j++)
        {
            Image i = Instantiate(coinImage, canvas.transform);
            i.transform.position = panpos.transform.position;
            i.transform.DOMove(coinpos.transform.position, .5f)
                .SetEase(coinCurve)
                .OnComplete(() =>
                {
                    Destroy(i.gameObject);
                });
            AudioManager.instance.Play("Coin");
            yield return new WaitForSeconds(.05f);
        }
    }
    public IEnumerator ContinueLevel()
    {
        for (int j = 0; j < 5; j++)
        {
            Image i = Instantiate(coinImage, canvas.transform);
            i.transform.position = coinImage.transform.position;
            i.transform.DOMove(levelContinueButton.transform.position, .5f)
                .SetEase(coinCurve)
                .OnComplete(() =>
                {
                    Destroy(i.gameObject);
                });
            yield return new WaitForSeconds(.05f);
        }
        AudioManager.instance.Play("Coin");
        yield return new WaitForSeconds(.5f);
        levelLosePanel.SetActive(false);
    }

    public void ButtonClicKSound()
    {
        AudioManager.instance.Play("ButtonClick");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }



}
