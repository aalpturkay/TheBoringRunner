using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    #region Singleton

    private static Deneme _instance;
    public static Deneme Instance => _instance;

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

    public void Za()
    {
        print("zaaaaaaaaaaaaaaaaaaaaaaaaa");
    }
}