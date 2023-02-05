using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour
{

    [SerializeField] LevelManager levelmanager;
    [SerializeField] GameObject _insidePan;
    [SerializeField] GameObject _upperPan;
    [SerializeField] List<FlagOrder> flagOrder = new List<FlagOrder>();

    private int _mealCount = 0;



    private void OnEnable()
    {
        GameManager.DoubleClick += GameManager_DoubleClick;
        GameManager.LevelStart += GameManager_LevelStart;
        GameManager.NextMeal += GameManager_NextMeal;
        GameManager.MealFinish += GameManager_MealFinish;
    }

    private void GameManager_MealFinish()
    {

    }

    private void GameManager_NextMeal(int obj)
    {
        MealController(obj);
        _mealCount = obj;
    }

    private void GameManager_LevelStart()
    {
        GameManager.instance.OnNextMeal(0);
        GameManager.instance.OnTimerUpdate(levelmanager.levels[Data.instance.GetCurrentLevel()].leveltime);
        GameManager.instance.OnTimerStart();
    }

    private void GameManager_DoubleClick(GameObject obj)
    {
        bool flag = false;
        int level = Data.instance.GetCurrentLevel();
        for (int i = 0; i < levelmanager.levels[level].meals[_mealCount].orders.Count; i++)
        {
            if (obj.GetComponent<Vegitable>().ID == levelmanager.levels[level].meals[_mealCount].orders[i].vegitable.ID)
            {
                flag = true;
            }
        }
        if (flag)
        {
            MoveToPanInside(obj);
            flag = false;
        }
        else
        {
            MoveToPanUpper(obj);
        }
        AudioManager.instance.Play("FlyObject");
        MealFinishController();

    }
    private void OnDisable()
    {
        GameManager.DoubleClick -= GameManager_DoubleClick;
        GameManager.LevelStart -= GameManager_LevelStart;
        GameManager.NextMeal -= GameManager_NextMeal;
        GameManager.MealFinish -= GameManager_MealFinish;

    }

    void MoveToPanInside(GameObject go)
    {
        if (OrderController(go.GetComponent<Vegitable>()))
        {
            go.TryGetComponent<IDoubleClick>(out var iDoubleClickComponent);
            iDoubleClickComponent?.onDoubleClick();
            go.transform.DOMove(_upperPan.transform.position, .5f).SetEase(Ease.InSine);
            go.transform.SetParent(_insidePan.transform);
        }
        else
        {
            MoveToPanUpper(go);
        }

    }
    void MoveToPanUpper(GameObject go)
    {
        go.transform.DOMove(_upperPan.transform.position, .5f).SetEase(Ease.InSine);
    }

    bool OrderController(Vegitable flag)
    {
        bool flagBool = false;

        for (int i = 0; i < flagOrder.Count; i++)
        {
            if (flagOrder[i].flagVegitable.ID == flag.ID)
            {
                if (flagOrder[i].flagCount > 0)
                {
                    flagOrder[i].flagCount--;
                    flagBool = true;
                }
                else
                {
                    flagBool = false;
                }
            }

        }
        return flagBool;
    }


    void MealFinishController()
    {

        int flag = 0;
        for (int i = 0; i < flagOrder.Count; i++)
        {
            if (flagOrder[i].flagCount == 0)
            {
                flag++;
            }
        }
        if (flagOrder.Count == flag)
        {
            int meal = _mealCount + 1;
            if (meal == 2)
            {
                GameManager.instance.OnLevelWin();
                AudioManager.instance.Play("Win");
                ClearPanInside(_insidePan.transform);
            }
            else
            {
                GameManager.instance.OnNextMeal(meal);
                GameManager.instance.OnMealFinish();
                Data.instance.OnSetCurrency(10);
                ClearPanInside(_insidePan.transform);

            }
        }
    }

    void MealController(int meal)
    {
        flagOrder.Clear();
        int level = Data.instance.GetCurrentLevel();

        if (levelmanager.levels.Count <= level)
        {
            return;

        }


        for (int i = 0; i < levelmanager.levels[level].meals[meal].orders.Count; i++)
        {
            FlagOrder fo = new FlagOrder();
            fo.flagVegitable = levelmanager.levels[level].meals[meal].orders[i].vegitable;
            fo.flagCount = levelmanager.levels[level].meals[meal].orders[i].count;

            flagOrder.Add(fo);
        }
    }

    void ClearPanInside(Transform pan)
    {
        foreach (Transform go in pan)
        {
            if (go != null)
            {
                Destroy(go.gameObject);

            }
        }
    }
}

[Serializable]
public class FlagOrder
{
    public Vegitable flagVegitable;
    public int flagCount;
}