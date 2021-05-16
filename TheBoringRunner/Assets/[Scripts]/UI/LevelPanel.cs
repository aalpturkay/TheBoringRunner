using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private void Update()
    {
        levelText.SetText($"Level {LevelManager.Instance.CurrentLevelVal}");
    }
}