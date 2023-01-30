using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] LevelManager levelmanager;
    [SerializeField] GameObject _insidePan;
    [SerializeField] GameObject _upperPan;
    [SerializeField] List<FlagOrder> flagOrder = new List<FlagOrder>();

    private void OnEnable()
    {
        GameManager.DoubleClick += GameManager_DoubleClick;
        GameManager.LevelStart += GameManager_LevelStart;
    }

    private void GameManager_LevelStart()
    {
        flagOrder.Clear();

        for (int i = 0; i < levelmanager.levels[Data.instance.GetCurrentLevel()].orders.Count; i++)
        {
            FlagOrder fo = new FlagOrder();
            fo.flagVegitable = levelmanager.levels[Data.instance.GetCurrentLevel()].orders[i].vegitable;
            fo.flagCount = levelmanager.levels[Data.instance.GetCurrentLevel()].orders[i].count;

            flagOrder.Add(fo);
        }

        GameManager.instance.OnTimerUpdate(levelmanager.levels[Data.instance.GetCurrentLevel()].leveltime);
        GameManager.instance.OnTimerStart();
    }

    private void GameManager_DoubleClick(GameObject obj)
    {
        bool flag = false;
        for (int i = 0; i < levelmanager.levels[Data.instance.GetCurrentLevel()].orders.Count; i++)
        {
            if (obj.GetComponent<Vegitable>().ID == levelmanager.levels[Data.instance.GetCurrentLevel()].orders[i].vegitable.ID)
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

        LevelWinControl();

    }
    private void OnDisable()
    {
        GameManager.DoubleClick -= GameManager_DoubleClick;
        GameManager.LevelStart -= GameManager_LevelStart;

    }

    void MoveToPanInside(GameObject go)
    {
        if (OrderController(go.GetComponent<Vegitable>()))
        {
            go.TryGetComponent<IDoubleClick>(out var iDoubleClickComponent);
            iDoubleClickComponent?.onDoubleClick();
            go.transform.position = new Vector3(_insidePan.transform.position.x, _insidePan.transform.position.y, _insidePan.transform.position.z);
        }
        else
        {
            MoveToPanUpper(go);
        }

    }
    void MoveToPanUpper(GameObject go)
    {
        go.transform.position = new Vector3(_upperPan.transform.position.x, _upperPan.transform.position.y, _upperPan.transform.position.z);
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

    void LevelWinControl()
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
            GameManager.instance.OnLevelWin();
        }
    }

}
[Serializable]
public class FlagOrder
{
    public Vegitable flagVegitable;
    public int flagCount;
}