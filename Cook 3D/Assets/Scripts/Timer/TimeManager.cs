using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.OnLevelStart();
    }


}
