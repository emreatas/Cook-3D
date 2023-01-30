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
        bool flag = false;
        int flag2 = 0;

        for (int i = 0; i < vegitables.Count; i++)
        {
            for (int j = 0; j < levelManager.levels[currentLevel].orders.Count; j++)
            {
                if (vegitables[i].ID == levelManager.levels[currentLevel].orders[j].vegitable.ID)
                {
                    flag = true;
                    flag2 = j;
                }
            }

            if (flag)
            {
                SpawnObject(vegitables[i].gameObject, levelManager.levels[currentLevel].orders[flag2].count);
                flag = false;


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