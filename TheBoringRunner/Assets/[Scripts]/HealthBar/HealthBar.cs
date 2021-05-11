using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private int maxHealth;
    [SerializeField] private int initialHealth;
    public int GetHealth => (int) slider.value;

    private void Start()
    {
        SetInitialHealth(initialHealth);
        SetMaxHealth(maxHealth);
    }

    private void SetInitialHealth(int val)
    {
        slider.value = val;
    }

    private void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(int health)
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        slider.DOValue(health, .2f);
    }
}