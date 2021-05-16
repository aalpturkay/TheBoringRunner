using System;
using System.Collections;
using System.Collections.Generic;
using Collectables;
using DG.Tweening;
using Events;
using Other;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Singleton and EventListeners

    private void Awake()
    {
        _playerColorType = _playerType.colorTypes;
        GameEvents.PlayerCollisionEvent.AddListener(PlayerCollisionWithMan);
    }

    #endregion

    #region Swerve

    [SerializeField] private PlayerSwipe playerSwipe;
    private float swerveSpeed = 0.5f;
    private float maxSwerveAmount = .3f;
    private float _startPosX, _endPosX, _mousePosDeltaX;

    #endregion

    [SerializeField] private PlayerType _playerType;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private TextMeshProUGUI manCountText;
    [SerializeField] private Image manCountImage;
    [SerializeField] private Sprite manCountRedSprite;
    [SerializeField] private Sprite manCountBlueSprite;
    [SerializeField] private Sprite manCountGreenSprite;
    private int _collectedManCount = 0;
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

    public Image ManCountImage => manCountImage;

    public TextMeshProUGUI ManCountText => manCountText;
    public int CollectedManCount => _collectedManCount;
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
                IncreaseManCount();
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
                    SetManCountImage(manCountRedSprite);
                    break;
                case ColorType.ColorTypes.Green:
                    _playerColorType = ColorType.ColorTypes.Green;
                    SetPlayerColor(greenMaterial);
                    SetManCountImage(manCountGreenSprite);
                    break;
                case ColorType.ColorTypes.Blue:
                    _playerColorType = ColorType.ColorTypes.Blue;
                    SetPlayerColor(blueMaterial);
                    SetManCountImage(manCountBlueSprite);
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

    private void IncreaseManCount()
    {
        _collectedManCount += 1;
        manCountText.SetText(_collectedManCount.ToString());
    }

    private void SetManCountImage(Sprite manCountSprite)
    {
        manCountImage.sprite = manCountSprite;
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
            _mousePosDelta = Vector3.zero;
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

        print(_mousePosDelta * Time.deltaTime);

        var playerPos = _playerTransform.position;
        var targetPos = playerPos.x + _mousePosDelta.x * Time.deltaTime;

        var speed = _playerType.speed;
        playerPos.z = playerPos.z + speed * Time.deltaTime;


        playerPos.x = Mathf.MoveTowards(playerPos.x, Mathf.Abs(_mousePosDelta.x) <= .2 ? playerPos.x : targetPos,
            12 * Time.deltaTime);


        var rot = _mousePosDelta.x;
        rot = Mathf.Clamp(rot, -30, 30);
        var rotEndVal = new Vector3(0,
            _mousePosDelta.x == 0 ? 0 : Mathf.Sign(_mousePosDelta.x) * 30, 0);
        _playerTransform.DORotate(Mathf.Abs(_mousePosDelta.x) <= .2 ? Vector3.zero : new Vector3(0, rot, 0), .4f);


        playerPos.x = Mathf.Clamp(playerPos.x, -4f, 4f);
        _playerTransform.position = playerPos;
    }

    public void SwervePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosX = Input.mousePosition.x;
            _mousePosDeltaX = 0;
        }

        else if (Input.GetMouseButton(0))
        {
            _mousePosDeltaX = Input.mousePosition.x - _startPosX;
            _startPosX = Mathf.Lerp(_startPosX, Input.mousePosition.x, 0.05f);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _startPosX = 0;
            _mousePosDeltaX = 0;
        }

        var playerPos = _playerTransform.position;
        var speed = _playerType.speed;
        playerPos.z = playerPos.z + speed * Time.deltaTime;
        playerPos.x += _mousePosDeltaX * Time.deltaTime * .04f;
        playerPos.x = Mathf.Clamp(playerPos.x, -4f, 4f);
        _playerTransform.position = playerPos;

        var rot = _mousePosDeltaX;
        rot = Mathf.Clamp(rot, -30, 30);
        _playerTransform.DORotate(Mathf.Abs(_mousePosDeltaX) <= .2 ? Vector3.zero : new Vector3(0, rot, 0), .4f);
    }

    public void SwerveMovement()
    {
        var playerPos = _playerTransform.position;
        var speed = _playerType.speed;
        playerPos.z = playerPos.z + speed * Time.deltaTime;

        float swerveAmount = Time.deltaTime * swerveSpeed * playerSwipe.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount, 0, speed * Time.deltaTime);

        playerPos.x = Mathf.Clamp(playerPos.x, -4f, 4f);
    }
}