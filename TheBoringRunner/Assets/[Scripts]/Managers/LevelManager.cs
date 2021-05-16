using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private Transform parentLevels;
    private Level _currentLevel;
    private Level _nextLevel;
    private Level _loadedLevel;

    public Level CurrentLevel => _currentLevel;
    public int CurrentLevelVal => levels.IndexOf(_currentLevel) + 1;

    #region Singleton

    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        else
        {
            _instance = this;
        }
    }

    #endregion


    private void Start()
    {
        if (_currentLevel == null)
        {
            _currentLevel = levels[0];
            _loadedLevel = LoadLevel(_currentLevel);
            print(levels.IndexOf(_currentLevel));
        }
    }

    private Level LoadLevel(Level level)
    {
        Level loadedLevel;
        if (level != null)
        {
            loadedLevel = Instantiate(level, parent: parentLevels);
            return loadedLevel;
        }

        return null;
    }

    private void DestroyLevel(Level level)
    {
        Destroy(level.gameObject);
    }

    public void LoadNextLevel()
    {
        var currentLevelIndex = levels.IndexOf(_currentLevel);
        print(currentLevelIndex);
        if (_currentLevel != null && currentLevelIndex + 1 != levels.Count)
        {
            DestroyLevel(_loadedLevel);
            _currentLevel = levels[currentLevelIndex + 1];
            _loadedLevel = LoadLevel(levels[currentLevelIndex + 1]);
        }
    }

    public void ReloadLevel()
    {
        if (_currentLevel != null && _loadedLevel != null)
        {
            DestroyLevel(_loadedLevel);
            _loadedLevel = LoadLevel(_currentLevel);
        }
    }
}