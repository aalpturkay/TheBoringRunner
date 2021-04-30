using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton

    private static PlayerController _instance;
    public static PlayerController Instance => _instance;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    [SerializeField] private PlayerType _playerType;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _playerTransform;

    public Animator PlayerAnimator
    {
        get => _playerAnimator;
        set => _playerAnimator = value;
    }

    public Transform PlayerTransform
    {
        get => _playerTransform;
        set => _playerTransform = value;
    }

    public float PlayerHealth => _playerType.health;
    public float PlayerSpeed => _playerType.speed;
}