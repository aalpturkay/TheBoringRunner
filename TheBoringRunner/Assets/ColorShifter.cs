using System;
using System.Collections;
using System.Collections.Generic;
using Other;
using UnityEngine;

public class ColorShifter : MonoBehaviour
{
    [SerializeField] private ColorType.ColorTypes colorShifterColorType;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    private ColorType.ColorTypes _colorType;

    public ColorType.ColorTypes ShifterColorType => _colorType;
    private void Awake()
    {
        _colorType = colorShifterColorType;
    }


    private void Start()
    {
        switch (colorShifterColorType)
        {
            case ColorType.ColorTypes.Red:
                SetColor(Color.red);
                break;
            case ColorType.ColorTypes.Green:
                SetColor(Color.green);
                break;
            case ColorType.ColorTypes.Blue:
                SetColor(Color.blue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private void SetColor(Color color)
    {
        skinnedMeshRenderer.material.SetColor("_TintColor", color);
    }
}