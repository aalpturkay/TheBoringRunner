using System;
using System.Collections;
using System.Collections.Generic;
using Collectables;
using DG.Tweening;
using Events;
using Other;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton and EventListeners

    private void Awake()
    {
        _playerColorType = _playerType.colorTypes;
        GameEvents.PlayerCollisionEvent.AddListener(PlayerCollisionWithMan);
    }

    #endregion

    [SerializeField] private PlayerType _playerType;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;
    private Vector3 _startPos, _endPos, _mousePosDelta;
    private bool _isDrag;
    private bool _isFinalLine = false;
    private ColorType.ColorTypes _playerColorType;
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

    public Rigidbody PlayerRb => _playerRb;
    public float PlayerHealth => _playerType.health;
    public float PlayerSpeed => _playerType.speed;
    public bool IsFinalLine => _isFinalLine;

    private void PlayerCollisionWithMan(GameObject manGameObject, ColorType.ColorTypes playerColorType)
    {
        if (manGameObject.CompareTag("Collectable"))
        {
            var playerLocalScale = _playerTransform.localScale;
            var manColorType = manGameObject.GetComponent<ManCollectable>().CollectableType.colorTypes;
            if (manColorType == playerColorType)
            {
                _playerTransform.DOScale(
                    playerLocalScale.x >= 2 ? playerLocalScale : playerLocalScale + Vector3.one * .1f, .3f);
            }
            else
            {
                _playerTransform.DOScale(
                    playerLocalScale.x <= 1
                        ? playerLocalScale
                        : playerLocalScale - Vector3.one * .1f, .3f);
            }


            Destroy(manGameObject);
        }
    }

    private void ShiftPlayerColor(GameObject colorShifter)
    {
        if (colorShifter.CompareTag("ColorShifter"))
        {
            var colorShifterColor = colorShifter.GetComponent<ColorShifter>().ShifterColorType;
            switch (colorShifterColor)
            {
                case ColorType.ColorTypes.Red:
                    _playerColorType = ColorType.ColorTypes.Red;
                    SetPlayerColor(redMaterial);
                    break;
                case ColorType.ColorTypes.Green:
                    _playerColorType = ColorType.ColorTypes.Green;
                    SetPlayerColor(greenMaterial);
                    break;
                case ColorType.ColorTypes.Blue:
                    _playerColorType = ColorType.ColorTypes.Blue;
                    SetPlayerColor(blueMaterial);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void SetPlayerColor(Material material)
    {
        playerRenderer.material = material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
            _isFinalLine = true;
        ShiftPlayerColor(other.gameObject);
        GameEvents.PlayerCollisionEvent?.Invoke(other.gameObject, _playerColorType);
    }

    public void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _mousePosDelta = Input.mousePosition - _startPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _startPos = Vector3.zero;
            _mousePosDelta = Vector3.zero;
        }
        //print(_mousePosDelta.x * Time.deltaTime);

        var playerPos = _playerTransform.position;
        var targetPos = playerPos.x + _mousePosDelta.x * Time.deltaTime;
        //print(targetPos);
        var speed = _playerType.speed;
        playerPos.z = playerPos.z + speed * Time.deltaTime;
        playerPos.x = Mathf.MoveTowards(playerPos.x, targetPos, 6 * Time.deltaTime);
        playerPos.x = Mathf.Clamp(playerPos.x, -4f, 4f);
        _playerTransform.position = playerPos;


        var rotEndVal = new Vector3(0, _mousePosDelta.x == 0 ? 0 : Mathf.Sign(_mousePosDelta.x) * 45, 0);
        _playerTransform.DORotate(rotEndVal, .8f);
    }
}