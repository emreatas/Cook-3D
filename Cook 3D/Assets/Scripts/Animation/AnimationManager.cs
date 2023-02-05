using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("True Meal")]
    [SerializeField] ParticleSystem _mealParticle;

    private void OnEnable()
    {
        GameManager.MealFinish += GameManager_MealFinish;
    }

    private void GameManager_MealFinish()
    {
        _mealParticle.Play();
    }

    private void OnDisable()
    {
        GameManager.MealFinish -= GameManager_MealFinish;

    }
}
