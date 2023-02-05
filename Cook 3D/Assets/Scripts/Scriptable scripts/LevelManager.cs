using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelManager")]
public class LevelManager : ScriptableObject
{
    public List<LevelOrder> levels = new List<LevelOrder>();


}


[Serializable]
public class Order
{
    public Vegitable vegitable;
    public int count;
}

[Serializable]
public class Meal
{
    public List<Order> orders = new List<Order>();

}

[Serializable]
public class LevelOrder
{
    [Tooltip("The time must be entered in seconds.\r\n")]
    public int leveltime;

    public LevelDifficulty levelDifficulty;
    public List<Meal> meals = new List<Meal>();

}

public enum LevelDifficulty
{
    Easy,
    Medium,
    Hard
}