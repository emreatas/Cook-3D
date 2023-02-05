using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Vegitable> vegitables = new List<Vegitable>();
    public GameObject spawnPoint;
    public LevelManager levelManager;

    private void OnEnable()
    {
        GameManager.LevelStart += GameManager_LevelStart;
        GameManager.NextLevel += GameManager_NextLevel;
        GameManager.RetryLevel += GameManager_RetryLevel;
    }

    private void GameManager_RetryLevel()
    {
        LevelSpawn();
    }

    private void GameManager_NextLevel()
    {
        LevelSpawn();
    }

    private void GameManager_LevelStart()
    {
        LevelSpawn();
    }

    private void OnDisable()
    {
        GameManager.LevelStart -= GameManager_LevelStart;
        GameManager.NextLevel -= GameManager_NextLevel;
        GameManager.RetryLevel -= GameManager_RetryLevel;


    }

    void LevelSpawn()
    {
        foreach (Transform spawn in spawnPoint.transform)
        {
            if (spawn != null)
            {

                Destroy(spawn.gameObject);
            }
        }

        int currentLevel = Data.instance.GetCurrentLevel();
        Debug.Log(currentLevel);

        List<Order> flagorders = new List<Order>();
        for (int i = 0; i < levelManager.levels[currentLevel].meals.Count; i++)
        {
            for (int j = 0; j < levelManager.levels[currentLevel].meals[i].orders.Count; j++)
            {
                Order flag = new Order();
                flag.vegitable = levelManager.levels[currentLevel].meals[i].orders[j].vegitable;
                flag.count = levelManager.levels[currentLevel].meals[i].orders[j].count;
                flagorders.Add(flag);
            }
        }
        bool isSameVegi = false;
        int sameVegiID = 0;

        for (int i = 0; i < vegitables.Count; i++)
        {
            for (int j = 0; j < flagorders.Count; j++)
            {
                if (vegitables[i].ID == flagorders[j].vegitable.ID)
                {
                    sameVegiID = j;
                    isSameVegi = true;
                }
            }
            if (isSameVegi)
            {
                SpawnObject(vegitables[i].gameObject, flagorders[sameVegiID].count);
                isSameVegi = false;
            }
            else
            {
                SpawnObject(vegitables[i].gameObject, 1 * ((int)levelManager.levels[currentLevel].levelDifficulty + 1));

            }
        }

    }

    void SpawnObject(GameObject go, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject flagobject = Instantiate(go, spawnPoint.transform);
            flagobject.transform.position = spawnPoint.transform.position;
        }
    }

}