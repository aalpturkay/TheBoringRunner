using System;
using System.Collections;
using System.Collections.Generic;
using Collectables;
using UnityEngine;

public class ManCollectable : MonoBehaviour
{
    [SerializeField] private CollectableType _collectableType;

    public CollectableType CollectableType => _collectableType;
    private void Start()
    {
        Debug.Log(_collectableType.colorTypes);
    }
}
