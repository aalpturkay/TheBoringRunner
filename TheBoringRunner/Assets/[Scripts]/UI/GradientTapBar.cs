using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientTapBar : MonoBehaviour
{
    [SerializeField] private Image tapBarImage;
    [SerializeField] private HealthBar tapBar;

    void Update()
    {
        SetGradientColor();
    }

    private void SetGradientColor()
    {
        Color tapBarColor = Color.Lerp(Color.red, Color.yellow, (tapBar.GetHealth / 100f));
        tapBarImage.color = tapBarColor;
    }
}